using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;

namespace Poison.Train
{
    public static class ChartExtensions
    {
        public static void AddLegend(this Chart c)
        {
            Legend legend = new Legend("Default");

            legend.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            legend.Alignment = StringAlignment.Center;
            legend.Docking = Docking.Bottom;
            legend.LegendStyle = LegendStyle.Table;

            c.Legends.Add(new Legend("Default"));
        }

        public static void InitializeChart(this Chart chart, int height, int width, string xAxisName, string yAxisName, 
            double? xAxisInterval, double? yAxisInterval,
            double xMin, double xMax, double yMin, double yMax)
        {
            chart.Width = Unit.Pixel(width);
            chart.Height = Unit.Pixel(height);

            // Set max height of chart legend
            if (chart.Legends[0] != null)
            {
                chart.Legends[0].MaximumAutoSize = 10;
            }

            if (chart.ChartAreas.Count() == 0)
            {
                ChartArea area = new ChartArea("ChartArea0");
                chart.ChartAreas.Add(area);
                area.AxisX.Initialize(xAxisName, xAxisInterval, xMin, xMax, TextOrientation.Horizontal);
                area.AxisY.Initialize(yAxisName, yAxisInterval, yMin, yMax, TextOrientation.Rotated270);
            }
        }

        public static void Initialize(this Axis axis, string name, double? interval, double minimum, double maximum, TextOrientation orientation)
        {
            Font font = new Font("Segoe UI", 9);

            if (interval.HasValue)
            {
                axis.Interval = interval.Value;
                axis.MajorGrid.Interval = interval.Value;
                axis.Minimum = minimum;
                axis.Maximum = maximum;
                axis.LabelStyle.Format = "0.00";
                axis.LabelStyle.Font = font;
                axis.LabelStyle.Interval = interval.Value;
                axis.LabelStyle.IntervalType = DateTimeIntervalType.Number;
                axis.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Number;
                axis.LabelStyle.IntervalOffset = 0.00;
                axis.LabelAutoFitStyle = LabelAutoFitStyles.StaggeredLabels;
            }

            axis.LineColor = Color.Silver;
            axis.MajorGrid.LineColor = Color.Silver;
            axis.Name = name;
            axis.Title = name;
            axis.TextOrientation = orientation;
            axis.TitleFont = font;
        }

        public static Series AddSeries(this Chart chart, string seriesName, IEnumerable xValues, IEnumerable yValues, Color? color, SeriesChartType type)
        {
            Series series = chart.AddSeries(seriesName, color, type);
            series.Points.DataBindXY(xValues, yValues);
            return series;
        }

        public static Series AddSeries(this Chart chart, string seriesName, IEnumerable<DataPoint> points, Color? color, SeriesChartType type)
        {
            Series series = chart.AddSeries(seriesName, color, type);
            foreach (var p in points)
                series.Points.Add(p);
            return series;
        }

        public static Series AddSeries(this Chart chart, string seriesName, Color? color, SeriesChartType type)
        {
            ChartArea area = chart.ChartAreas[0];
            Series series = new Series(seriesName);
            series.ChartArea = area.Name;
            series.ChartType = type;

            if (color.HasValue)
            {
                series.Color = color.Value;
            }

            if (type == SeriesChartType.Column ||
                type == SeriesChartType.StackedColumn ||
                type == SeriesChartType.StackedColumn100 ||
                type == SeriesChartType.Bar ||
                type == SeriesChartType.StackedBar ||
                type == SeriesChartType.StackedBar100)
            {
                series.CustomProperties = "DrawingStyle = Cylinder";
            }
            else if (type == SeriesChartType.Line)
            {
                series.BorderWidth = 2;
                series.MarkerStyle = MarkerStyle.Circle;
                series.MarkerSize = 7;
            }
            else if (type == SeriesChartType.Pie)
            {
                series.CustomProperties = "PieDrawingStyle = SoftEdge";
            }

            area.AxisX.LabelStyle.Angle = 90;
            area.AxisX.LabelStyle.Interval = 1;

            if (series.ChartType == SeriesChartType.Pie)
            {
                for (var i = 0; i < series.Points.Count; ++i)
                {
                    series.Points[i]["PieLabelStyle"] = "Disabled";
                }
            }

            chart.Series.Add(series);
            chart.Legends[0].TextWrapThreshold = 500;
            return series;
        }

        public static System.Drawing.Image ToImage(this Chart c)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                c.SaveImage(stream, ChartImageFormat.Png);
                stream.Flush();

                stream.Seek(0, SeekOrigin.Begin);

                return System.Drawing.Image.FromStream(stream);
            }
        }
    }
}