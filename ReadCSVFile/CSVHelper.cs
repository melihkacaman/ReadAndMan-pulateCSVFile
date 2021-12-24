using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadCSVFile
{
    public class CSVHelper
    {
        public static DataTable readCSV(string filePath, char seperator, bool columns = false) {
            var dt = new DataTable();
            if (columns == true) {
                // Creating the columns 
                File.ReadLines(filePath).Take(1)
                    .SelectMany(x => x.Split(new[] { seperator }, StringSplitOptions.RemoveEmptyEntries))
                    .ToList()
                    .ForEach(x => dt.Columns.Add(x.Trim()));
            }

            // Adding Rows
            File.ReadLines(filePath).Skip(1)
                .Select(x => x.Split(seperator))
                .ToList()
                .ForEach(line => dt.Rows.Add(line));
            
            return dt; 
        }
    }
}
