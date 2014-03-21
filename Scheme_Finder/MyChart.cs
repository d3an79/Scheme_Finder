/*******************************************************************
 * 
 * 
 * 
 * 
 * 
 * To do
 * 
 * Change DrawChart() to take in a string or enum for chart type which then passes it on to the
 *   correct function
 *   
 * Geting messy now, need to clean up and take out the hardcoded variables
 * 
 * *****************************************************************/


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
        public void DrawChart(float[] vals, string seriesName, string title, string xTitle, string yTitle,
            float lowLimit)
        {
            // clear chart values
            this.chart1.Titles.Clear();
            this.chart1.Series.Clear();
            this.chart1.ChartAreas[0].AxisY.StripLines.Clear();

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

            // set the x axis interval type
            this.chart1.ChartAreas[0].AxisX.Interval = 1;
            // set the max x val to auto
            this.chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;

            // draw low limit line on chart
            this.chart1.ChartAreas[0].AxisY.StripLines.Add(
                new StripLine()
                {
                    BorderColor = Color.Red,
                    IntervalOffset = lowLimit                    
                });

            // populates the points in a series
            populateSeries(vals, seriesName);
        }

        // draw a line chart with 2 series' and titles
        // !!! change to take in a list of arrays for values and names
        // !!! then loop through them to add any number of series
        public void DrawChart(float[] vals1, string seriesName1, float[] vals2, string seriesName2
            , string title, string xTitle, string yTitle, float lowLimit)
        {
            // clear chart values
            this.chart1.Titles.Clear();
            this.chart1.Series.Clear();
            this.chart1.ChartAreas[0].AxisY.StripLines.Clear();

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

            // set the x axis interval type
            this.chart1.ChartAreas[0].AxisX.Interval = 1;
            // set the max x val to auto
            this.chart1.ChartAreas[0].AxisX.Maximum = Double.NaN;

            // set the line width for each series
            chart1.Series[seriesName1].BorderWidth = 2;
            chart1.Series[seriesName2].BorderWidth = 2;

            // draw low limit line on chart
            this.chart1.ChartAreas[0].AxisY.StripLines.Add(
                new StripLine()
                {
                    BorderColor = Color.Red,
                    IntervalOffset = lowLimit
                });
            

            // populates the points in a series
            populateSeries(vals1, seriesName1);
            // populates the points in a series
            populateSeries(vals2, seriesName2);
        }

        // draw a spline chart that takes 2 float[,]
        public void DrawChart(float[,] vals1, string seriesName1, float[,] vals2, string seriesName2
            , string title, string xTitle, string yTitle, float maxSchemeLength, float lowLimit)
        {
            // clear chart values
            this.chart1.Titles.Clear();
            this.chart1.Series.Clear();
            this.chart1.ChartAreas[0].AxisY.StripLines.Clear();

            // add a series1 to the chart
            this.chart1.Series.Add(seriesName1);

            // add a series1 to the chart
            this.chart1.Series.Add(seriesName2);

            // Choose type of chart !!! could set this during runtime
            this.chart1.Series[seriesName1].ChartType = SeriesChartType.Spline;
            // Choose type of chart !!! could set this during runtime
            this.chart1.Series[seriesName2].ChartType = SeriesChartType.Spline;

            // add labels to chart
            this.chart1.Titles.Add(title);
            this.chart1.ChartAreas[0].AxisX.Title = xTitle;
            this.chart1.ChartAreas[0].AxisY.Title = yTitle;

            // set the x axis interval type
            this.chart1.ChartAreas[0].AxisX.Interval = 10;
            // remove the minor interval marks and grinds on x axis
            this.chart1.ChartAreas[0].AxisX.MinorTickMark.Enabled = false;
            this.chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            // set the min x val at 0
            this.chart1.ChartAreas[0].AxisX.Minimum = 0;
            // set the max x val to maxSchemeLength
            this.chart1.ChartAreas[0].AxisX.Maximum = maxSchemeLength;

            // set the line width for each series
            chart1.Series[seriesName1].BorderWidth = 2;
            chart1.Series[seriesName2].BorderWidth = 2;

            // draw low limit line on chart
            this.chart1.ChartAreas[0].AxisY.StripLines.Add(
                new StripLine()
                {
                    BorderColor = Color.Red,
                    IntervalOffset = lowLimit
                });

            // populate the xy points in a series
            populateXYSeries(vals1, seriesName1);
            // populate the xy points in a series
            populateXYSeries(vals2, seriesName2);
        }

        // populate points in a series from an float[]
        private void populateSeries(float[] vals, string seriesname)
        {
            foreach (float val in vals)
            {
                this.chart1.Series[seriesname].Points.Add(val);
            }
        }

        // populates xy points in a series from a float[,]
        private void populateXYSeries(float[,] vals, string seriesName)
        {
            // nested loop through vals[,]
            for (int i = 0; i < vals.GetLength(0); i++)
            {
                float xval = 0;
                float yval = 0;

                // assigns x and y vals
                for (int j = 0; j < 2; j++)
                {
                    if (j == 0)
                    {
                        xval = vals[i, j];
                    }
                    else
                    {
                        yval = vals[i, j];
                    }
                }
                // adds xy points
                this.chart1.Series[seriesName].Points.AddXY(xval, yval);
            }
        }
    }
}
