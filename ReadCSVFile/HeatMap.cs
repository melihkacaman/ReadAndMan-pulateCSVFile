using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCSVFile
{
    public partial class HeatMap : Form
    {
        private List<HeatPoint> heatPoints;
        private List<string> Xx;
        private List<string> Yy;
        private List<string> Zz;

        private List<double> Xx_double = new List<double>();
        private List<double> Yy_double = new List<double>();
        private List<double> Zz_double = new List<double>();

        Random rRand = new Random();

        public HeatMap(List<string> Xx, List<string> Yy, List<string> Zz)
        {
            this.heatPoints = new List<HeatPoint>();
            this.Xx = Xx;
            this.Yy = Yy;
            this.Zz = Zz;

            for (int i = 0; i < Xx.Count; i++)
            {
                Xx_double.Add(double.Parse(Xx[i]));
                Yy_double.Add(double.Parse(Yy[i]));
                Zz_double.Add(double.Parse(Zz[i]));
            }

            FeatureScaler featureScaler_x = new FeatureScaler(Xx_double, 200);
            Xx_double = featureScaler_x.ScaleData();
            FeatureScaler featureScaler_y = new FeatureScaler(Xx_double, 200);
            Yy_double = featureScaler_y.ScaleData();
            FeatureScaler featureScaler_z = new FeatureScaler(Xx_double, 120);
            Zz_double = featureScaler_z.ScaleData();
            

            InitializeComponent();                      
        }

        private Bitmap CreateIntensityMask(Bitmap bSurface, List<HeatPoint> aHeatPoints)
        {
            Graphics DrawSurface = Graphics.FromImage(bSurface);

            DrawSurface.Clear(Color.White);

            foreach (HeatPoint DataPoint in aHeatPoints)
            {
                DrawHeatPoint(DrawSurface, DataPoint, 25);
            }
            return bSurface;

        }

        private void DrawHeatPoint(Graphics Canvas, HeatPoint HeatPoint, int Radius)
        {
            List<Point> CircumferencePointsList = new List<Point>();
            Point CircumferencePoint;
            Point[] CircumferencePointsArray;
            float fRatio = 1F / Byte.MaxValue;
            byte bHalf = Byte.MaxValue / 2;
            int iIntensity = (byte)(HeatPoint.Intensity - ((HeatPoint.Intensity - bHalf) * 2));
            float fIntensity = iIntensity * fRatio;
            for (double i = 0; i <= 360; i += 10)
            {
                CircumferencePoint = new Point();
                CircumferencePoint.X = Convert.ToInt32(HeatPoint.X + Radius * Math.Cos(ConvertDegreesToRadians(i)));
                CircumferencePoint.Y = Convert.ToInt32(HeatPoint.Y + Radius * Math.Sin(ConvertDegreesToRadians(i)));
                CircumferencePointsList.Add(CircumferencePoint);
            }
            CircumferencePointsArray = CircumferencePointsList.ToArray();
            PathGradientBrush GradientShaper = new PathGradientBrush(CircumferencePointsArray);
            ColorBlend GradientSpecifications = new ColorBlend(3);
            GradientSpecifications.Positions = new float[3] { 0, fIntensity, 1 };
            GradientSpecifications.Colors = new Color[3]
            {
                Color.FromArgb(0, Color.White),
                Color.FromArgb(HeatPoint.Intensity, Color.Black),
                Color.FromArgb(HeatPoint.Intensity, Color.Black)
            };
            GradientShaper.InterpolationColors = GradientSpecifications;
            Canvas.FillPolygon(GradientShaper, CircumferencePointsArray);
        }

        private double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        public static Bitmap Colorize(Bitmap Mask, byte Alpha)
        {
            Bitmap Output = new Bitmap(Mask.Width, Mask.Height, PixelFormat.Format32bppArgb);
            Graphics Surface = Graphics.FromImage(Output);
            Surface.Clear(Color.Transparent);
            ColorMap[] Colors = CreatePaletteIndex(Alpha);
            ImageAttributes Remapper = new ImageAttributes();
            Remapper.SetRemapTable(Colors);
            Surface.DrawImage(Mask, new Rectangle(0, 0, Mask.Width, Mask.Height), 0, 0, Mask.Width, Mask.Height, GraphicsUnit.Pixel, Remapper);
            return Output;
        }
        private static ColorMap[] CreatePaletteIndex(byte Alpha)
        {
            ColorMap[] OutputMap = new ColorMap[256];
            Bitmap Palette = (Bitmap)Bitmap.FromFile(@"C:\Users\melih\OneDrive\Desktop\palette2.1.jpeg");
            for (int X = 0; X <= 255; X++)
            {
                OutputMap[X] = new ColorMap();
                OutputMap[X].OldColor = Color.FromArgb(X, X, X);
                OutputMap[X].NewColor = Color.FromArgb(Alpha, Palette.GetPixel(X, 0));
            }
            return OutputMap;
        }

        private void HeatMap_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.heatPoints = new List<HeatPoint>();
            Bitmap bMap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            int iX;   // 0 - 200 
            int iY;   // 0 - 200 
            byte iIntense; // 0 - 120 
      
             
            for (int i = 0; i < 500; i++)
            {
                iX = rRand.Next(0, 200);                
                iY = rRand.Next(0, 200);
                iIntense = (byte)rRand.Next(0, 120);
                heatPoints.Add(new HeatPoint(iX, iY, iIntense));
            }
           
            // createHeatLabels(X, Y);
            bMap = CreateIntensityMask(bMap, heatPoints);
            //pictureBox1.Image = Colorize(bMap, 255);
            pictureBox1.Image = bMap;
        }

        private void createHeatLabels(List<int> X, List<int> Y)
        {
            int biggestX = X.Max();
            int smallestX = X.Min();

            int biggestY = Y.Max();
            int smallestY = Y.Min();

            Label bigX = new Label();
            bigX.Text = biggestX.ToString();
            bigX.SetBounds(pictureBox1.Bounds.X - 5, pictureBox1.Bounds.Y - 5, 50, 50);
            this.Controls.Add(bigX);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.heatPoints = new List<HeatPoint>(); 
            Bitmap bMap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            int iX;   // 0 - 200 
            int iY;   // 0 - 200 
            byte iIntense; // 0 - 120 

            for (int i = 0; i < Xx_double.Count; i++)
            {
                iX = Convert.ToInt32(Xx_double[i]);
                iY = Convert.ToInt32(Yy_double[i]);
                iIntense = Convert.ToByte(Zz_double[i]);

                this.heatPoints.Add(new HeatPoint(iX, iY, iIntense));
            }

            bMap = CreateIntensityMask(bMap, heatPoints);
            pictureBox1.Image = bMap;
        }
    }
}
