using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadCSVFile
{
    public partial class Form1 : Form
    {
        private string prevValue = String.Empty; 
        public Form1()
        {
            InitializeComponent();           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = "C:";
            file.Filter = "CSV File |*.csv";
            
            string filePathRes = "";
           
            if (file.ShowDialog() == DialogResult.OK)
            {
                filePathRes = file.FileName;               
            }

            var result = CSVHelper.readCSV(filePathRes, ',', true);

            dataGridView1.DataSource = result; 
            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int? rowIdx = e?.RowIndex;
            int? colIdx = e?.ColumnIndex;
            if (rowIdx.HasValue && colIdx.HasValue)
            {
                var dgv = (DataGridView)sender;
                var cell = dgv?.Rows?[rowIdx.Value]?.Cells?[colIdx.Value]?.Value;
                if (string.IsNullOrWhiteSpace(cell?.ToString()))
                {
                    MessageBox.Show("You can't manipulate like this.");
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = prevValue;              
                }
            };
        }

        
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            prevValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null && !string.IsNullOrWhiteSpace(textBox1.Text)) {
                DataTable dt = (DataTable)dataGridView1.DataSource;
                dt.Columns.Add(textBox1.Text);
                dataGridView1.DataSource = dt; 
            }
            else
            {
                MessageBox.Show("You can't");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null) {
                AddRowForm addrow = new AddRowForm(dataGridView1);
                addrow.ShowDialog();
            } 
        }
    }
}
