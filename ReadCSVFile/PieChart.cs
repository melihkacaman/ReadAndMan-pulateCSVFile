using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ReadCSVFile
{
    public partial class PieChart : Form
    {
        private string dataTitle;
        private List<string> data;
        private Random rnd;
        bool once = false;
        List<Label> labels = new List<Label>();
        public PieChart(List<string> data, string dataTitle)
        {
            this.data = data;
            this.dataTitle = dataTitle;
            this.rnd = new Random(); 
            InitializeComponent();
        }

        private List<PieData> prepareDataForPie()
        {
            List<string> distincs = data.Distinct().ToList();
            List<PieData> pieDatas = new List<PieData>();

            foreach (string distinct in distincs)
            {
                int countt = 0; 
                foreach (string item in data)
                {
                    if (item.Equals(distinct)) {
                        countt++; 
                    }
                }

                pieDatas.Add(new PieData() { name = distinct, count_percentage = countt });
            }

            double summm = 0; 
            for (int i = 0; i < pieDatas.Count; i++)
            {
                pieDatas[i].count_percentage = (pieDatas[i].count_percentage / data.Count) * 100;
                summm += pieDatas[i].count_percentage; 
            }

            return pieDatas; 
        }

        class PieData
        {
            public string name { get; set; }
            public double count_percentage { get; set; }
        }

        private void DrawPieChartOnForm(List<PieData> pieDatas)
        {
            using (Graphics myPieGraphic = this.CreateGraphics())
            {
                Point myPieLocation = new Point(0, 0);
                Size myPieSize = new Size(this.Size.Width / 2, this.Size.Height / 2);

                int[] percents = new int[pieDatas.Count];
                Color[] colors = new Color[pieDatas.Count];
                int j = 0; 
                for (int i = 0; i < pieDatas.Count; i++)
                {
                    percents[i] = Convert.ToInt32(Math.Floor(pieDatas[i].count_percentage));                     
                    Color randomColor = System.Drawing.ColorTranslator.FromHtml(getRandColor());                    
                    colors[i] = randomColor;

                    Label label = new Label();
                    label.Text = pieDatas[i].name;
                    
                    label.SetBounds(380, 12 + j, 30, 30);
                    j += 30;
                    label.BackColor = randomColor;
                    labels.Add(label);
                }

                
                DrawPieChart(percents, colors, myPieGraphic, myPieLocation, myPieSize);
            }
        }

        private void DrawPieChart(int[] myPiePerecents, Color[] myPieColors, Graphics myPieGraphic, Point myPieLocation, Size myPieSize)
        {
            int sum = 0;
            foreach (int percent_loopVariable in myPiePerecents)
            {
                sum += percent_loopVariable;
            }

            if (sum != 100)
            {
                myPiePerecents[0] += (100 - sum); 
            }


            if (myPiePerecents.Length != myPieColors.Length)
            {
                MessageBox.Show("There Must Be The Same Number Of Percents And Colors.");
            }

            int PiePercentTotal = 0;
            for (int PiePercents = 0; PiePercents < myPiePerecents.Length; PiePercents++)
            {
                using (SolidBrush brush = new SolidBrush(myPieColors[PiePercents]))
                {
                    myPieGraphic.FillPie(brush, new Rectangle(new Point(10, 10), myPieSize),
                        Convert.ToSingle(PiePercentTotal * 360 / 100),
                        Convert.ToSingle(myPiePerecents[PiePercents] * 360 / 100));
                }

                PiePercentTotal += myPiePerecents[PiePercents];
            }
            return;
        }

        private void PieChart_Load(object sender, EventArgs e)
        {
        }

        private string getRandColor()
        {            
            string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            while (hexOutput.Length < 6)
                hexOutput = "0" + hexOutput;
            return "#" + hexOutput;
        }

        private void PieChart_Paint(object sender, PaintEventArgs e)
        {
            
            DrawPieChartOnForm(prepareDataForPie());
            foreach (var item in labels)
            {
                this.Controls.Add(item);
            }
            once = true; 
            
        }
    }
}
