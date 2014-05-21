using Poison.Modelling;
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
using System.Windows.Forms;

namespace Poison.Train
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void bt_Run_Click(object sender, EventArgs e)
        {
            Train train = new Train();

            ModelStat modelStat = new ModelStat(train);

            train.Simulate();

            tb_Stat.Text = FormatStatReport(modelStat, train);
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
    }
}
