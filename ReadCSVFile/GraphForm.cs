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
        public GraphForm()
        {
            InitializeComponent();
        }

        private void GraphForm_Load(object sender, EventArgs e)
        {
            

            chart1.Series["q1"].Points.AddXY("1", "2");
            chart1.Series["q1"].Points.AddXY("2", "3");
            chart1.Series["q1"].Points.AddXY("4", "5");
            chart1.Series["q1"].Points.AddXY("6", "7");
            chart1.Series["q1"].Points.AddXY("8", "2");
        }

        private void firstSeries() 
        {
            

        }
    }
}
