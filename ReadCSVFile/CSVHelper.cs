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

        public static void exportDataTableAsCSV(DataTable dtDataTable, string strFilePath = @"C:\exportingFile.csv") {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}
