﻿using Poison.Modelling;
using Poison.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;
using System.Windows.Forms;

namespace Poison.Train
{
    public partial class MainForm : Form
    {
        private Train train;

        private ModelStat modelStat;

        public MainForm()
        {
            InitializeComponent();

            train = new Train();
            modelStat = new ModelStat(train);
        }

        private async void bt_Run_Click(object sender, EventArgs e)
        {
            double generatingAvgTimeStart;
            double generatingAvgTimeStop;
            double step;

            if (!double.TryParse(tb_GeneratingAvgTimeStart.Text, out generatingAvgTimeStart) || generatingAvgTimeStart < 0.5 || generatingAvgTimeStart > 20.0)
            {
                MessageBox.Show("Начальное значение времени должно быть действительным числом больше 0,5 и меньше 20.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(tb_GeneratingAvgTimeStop.Text, out generatingAvgTimeStop) || generatingAvgTimeStop < 0.5 || generatingAvgTimeStop > 20.0)
            {
                MessageBox.Show("Конечное значение времени должно быть действительным числом больше 0,5 и меньше 20.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (generatingAvgTimeStart > generatingAvgTimeStop)
            {
                double temp = generatingAvgTimeStart;
                generatingAvgTimeStart = generatingAvgTimeStop;
                generatingAvgTimeStop = temp;
            }

            if (!double.TryParse(tb_Step.Text, out step) || step < 0.005)
            {
                MessageBox.Show("Шаг действительным числом больше 0,005.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bt_Run.Enabled = false;

            TrainModelData[] data = await Task.Factory.StartNew<TrainModelData[]>(() => Simulate(generatingAvgTimeStart, generatingAvgTimeStop, step));

            DrawCharts(data);

            bt_Run.Enabled = true;
        }

        private void DrawCharts(TrainModelData[] data)
        {
            int minInputTrainCount = data.Min(t => t.InputTrainCount);
            int maxInputTrainCount = data.Max(t => t.InputTrainCount);

            int minOutputTrainCount = data.Min(t => t.OutputTrainCount);
            int maxOutputTrainCount = data.Max(t => t.OutputTrainCount);

            Chart c = new Chart();

            c.AddLegend();

            c.InitializeChart(pb_Chart1.Height, pb_Chart1.Width, "Входящий поток n, поездов/неделю", "Выходящий поток n, поездов/неделю", 100, 100, minInputTrainCount, maxInputTrainCount, minOutputTrainCount, maxOutputTrainCount);

            List<DataPoint> points = new List<DataPoint>();

            foreach (TrainModelData item in data)
            {
                points.Add(new DataPoint(item.InputTrainCount, item.OutputTrainCount));
            }

            c.AddSeries("Выходной поток", points, null, SeriesChartType.Line);

            Image img = c.ToImage();

            pb_Chart1.Image = img;

            c = new Chart();

            c.AddLegend();

            c.InitializeChart(pb_Chart2.Height, pb_Chart2.Width, "Номер блок-участка", "Коэффициент загрузки блок-участка", 0.2, 0.2, 1.0, Train._IntervalCount, 0.0, 1.0);

            List<DataPoint>[] pointsNew = new List<DataPoint>[data.Length];

            for (int i = 0; i < pointsNew.Length; i++)
            {
                pointsNew[i] = new List<DataPoint>();
            }

            for (int i = 0; i < pointsNew.Length; i++)
            {   
                for (int j = 0; j < data[i].IntervalsUtil.Count; j++)
                {
                    pointsNew[i].Add(new DataPoint(j + 1, data[i].IntervalsUtil[j]));
                }
            }

            for (int i = 0; i < pointsNew.Length; i++)
            {
                c.AddSeries(string.Format("I={0} мин.",data[i].GeneratingAvgTime), pointsNew[i], null, SeriesChartType.Line);
            }

            img = c.ToImage();

            pb_Chart2.Image = img;
        }

        private TrainModelData[] Simulate(double generatingAvgTimeStart, double generatingAvgTimeStop, double step)
        {
            List<TrainModelData> data = new List<TrainModelData>();

            for (double i = generatingAvgTimeStop; i > generatingAvgTimeStart; i -= step)
            {
                TrainModelData item = new TrainModelData();

                train.GeneratingAvgTime = i;

                train.Simulate();

                item.InputTrainCount = train.InputTrainCount;
                item.OutputTrainCount = train.OutputTrainCount;

                for (int j = 0; j < Train._IntervalCount; j++)
                {
                    item.IntervalsUtil.Add(modelStat.FacilityStatCollection[Train.GetIntervalName(j)].Utilization);
                }

                item.GeneratingAvgTime = i;

                data.Add(item);
            }

            return data.ToArray();
        }

        private string FormatStatReport(ModelStat modelStat, Train train)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"START TIME: {0}", 0.0));
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"END TIME: {0}", modelStat.Model.Time));
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"FACILITIES: {0}", modelStat.Model.Facilities.Count));
            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"STORAGES: {0}", 0));
            builder.AppendLine();

            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"FACILITIES"));
            builder.AppendLine();

            foreach (FacilityStat facilityStat in modelStat.FacilityStatCollection.Values)
            {
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"FACILITY NAME: {0}", facilityStat.Facility.Name));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"FACILITY ENTRIES: {0}", facilityStat.Entries));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"FACILITY UTIL: {0}", facilityStat.Utilization));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"FACILITY AVE . TIME: {0}", facilityStat.AverageTime));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"FACILITY AVAIL: {0}", facilityStat.Facility.State == FacilityState.Free ? "Yes" : "No"));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"FACILITY OWNER: {0}", facilityStat.LastOwner));
                builder.AppendLine();
            }

            builder.AppendLine();

            builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"QUEUES"));
            builder.AppendLine();

            foreach (QueueStat queueStat in modelStat.QueueStatCollection.Values)
            {
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"QUEUE NAME: {0}", queueStat.Queue.Name));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"QUEUE MAX: {0}", queueStat.Max));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"QUEUE CONT.: {0}", queueStat.Queue.Count));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"QUEUE ENTRY: {0}", queueStat.EntryCount));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"QUEUE ENTRY (0): {0}", queueStat.EntryCountZero));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"QUEUE AVE. CONT.: {0}", queueStat.AverageCount));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"QUEUE AVE. TIME: {0}", queueStat.AverageTime));
                builder.AppendLine(string.Format(CultureInfo.CurrentCulture,"QUEUE AVE. TIME (-0): {0}", queueStat.AverageTimeNonZero));
                builder.AppendLine();
            }

            builder.AppendLine();

            builder.AppendLine(string.Format("Count of input trains: {0}",train.InputTrainCount));
            builder.AppendLine(string.Format("Count of output trains: {0}", train.OutputTrainCount));

            builder.AppendLine();

            return builder.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            tb_Step.Text = 0.05.ToString();
            tb_GeneratingAvgTimeStart.Text = 2.0.ToString();
            tb_GeneratingAvgTimeStop.Text = 5.0.ToString();
        }
    }
}
