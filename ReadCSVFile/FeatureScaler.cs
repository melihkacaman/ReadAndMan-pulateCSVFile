using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCSVFile
{
    public class FeatureScaler
    {
        private List<double> notScaledData;
        private int min;
        private int max; 
        public FeatureScaler(List<double> data, int rangeMax)
        {
            this.notScaledData = data;
            min = 0; 
            max = rangeMax; 
        }

        public List<double> ScaleData() {
            double minData = notScaledData.Min();
            double maxData = notScaledData.Max();

            List<double> result = new List<double>(); 
            foreach (double item in notScaledData)
            {
                double scl = max * ((item - minData) / (maxData - minData));
                result.Add(scl);
            }


            return result;
        }


    }
}
