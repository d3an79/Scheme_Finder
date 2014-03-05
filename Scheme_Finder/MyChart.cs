using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace ChartClass
{
    public class MyChart
    {
        Chart chart1 = new Chart();

        // Constructors for initialising the class
        // Initialise with just the chart
        public MyChart(Chart chart)
        {
            chart1 = chart;
        }

        // draw a column chart with given values, and titles
        public void DrawChart(float[] vals, string seriesName, string title, string xTitle, string yTitle)
        {
            // clear chart values
            this.chart1.Titles.Clear();
            this.chart1.Series.Clear();

            // add a series to the chart
            this.chart1.Series.Add(seriesName);
            // Hide legend in chart view
            this.chart1.Series[seriesName].IsVisibleInLegend = false;

            // Choose type of chart !!! could set this during runtime
            this.chart1.Series[seriesName].ChartType = SeriesChartType.Column;

            // add labels to chart
            this.chart1.Titles.Add(title);
            this.chart1.ChartAreas[0].AxisX.Title = xTitle;
            this.chart1.ChartAreas[0].AxisY.Title = yTitle;

            // populates the points in a series
            populateSeries(vals, seriesName);
        }

        // draw a line chart with 2 series' and titles
        // !!! change to take in a list of arrays for values and names
        // !!! then loop through them to add any number of series
        public void DrawChart(float[] vals1, string seriesName1, float[] vals2, string seriesName2
            , string title, string xTitle, string yTitle)
        {
            // clear chart values
            this.chart1.Titles.Clear();
            this.chart1.Series.Clear();

            // add a series1 to the chart
            this.chart1.Series.Add(seriesName1);

            // add a series1 to the chart
            this.chart1.Series.Add(seriesName2);

            // Choose type of chart !!! could set this during runtime
            this.chart1.Series[seriesName1].ChartType = SeriesChartType.Line;
            // Choose type of chart !!! could set this during runtime
            this.chart1.Series[seriesName2].ChartType = SeriesChartType.Line;


            // add labels to chart
            this.chart1.Titles.Add(title);
            this.chart1.ChartAreas[0].AxisX.Title = xTitle;
            this.chart1.ChartAreas[0].AxisY.Title = yTitle;

            // populates the points in a series
            populateSeries(vals1, seriesName1);
            // populates the points in a series
            populateSeries(vals2, seriesName2);
        }

        // populate points in a series from an int[]
        private void populateSeries(float[] vals, string seriesname)
        {
            foreach (int val in vals)
            {
                this.chart1.Series[seriesname].Points.Add(val);
            }
        }
    }
}
