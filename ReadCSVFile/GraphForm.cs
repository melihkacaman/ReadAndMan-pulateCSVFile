using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReadCSVFile
{
    public partial class GraphForm : Form
    {
        List<string> xx, yy;
        string xName, yName;
         
        public GraphForm(List<string> xx, List<string> yy, string xName, string yName, bool normalizeData)
        {
            // scale data points first. 
            if (normalizeData)
            {
                double[] x_scaler = new double[xx.Count];
                double[] y_scaler = new double[xx.Count];

                double x_summation = 0, y_summation = 0;

                for (int i = 0; i < xx.Count; i++)
                {
                    x_summation += double.Parse(xx[i]);
                    x_scaler[i] = double.Parse(xx[i]);

                    y_summation += double.Parse(yy[i]);
                    y_scaler[i] = double.Parse(yy[i]);
                }

                // normalize data points 
                for (int i = 0; i < xx.Count; i++)
                {
                    x_scaler[i] = x_scaler[i] / x_summation;
                    y_scaler[i] = y_scaler[i] / y_summation;
                }

                // prepare data for chart 
                this.xx = new List<string>();
                this.yy = new List<string>();

                for (int i = 0; i < xx.Count; i++)
                {
                    this.xx.Add(x_scaler[i].ToString());
                    this.yy.Add(y_scaler[i].ToString());
                }

            }
            else {
                this.xx = xx;
                this.yy = yy;             
            }

            this.xName = xName;
            this.yName = yName; 
          
            InitializeComponent();                
        }

        private void GraphForm_Load(object sender, EventArgs e)
        {
            CreateChart();       
        }

     

        private void CreateChart()
        {
            var series = new Series(xName + " - " + yName);
            // Frist parameter is X-Axis and Second is Collection of Y- Axis
            series.Points.DataBindXY(xx, yy);
            series.ChartType = SeriesChartType.Line;
            chart1.Series.Add(series);
        }
    }
}
