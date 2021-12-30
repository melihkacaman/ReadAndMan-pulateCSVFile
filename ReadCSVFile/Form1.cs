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
            if (dataGridView1.DataSource != null) {
                dataGridView1.DataSource = null;
                comboBox1.DataSource = null;
                comboBox2.DataSource = null;
                comboBox3.DataSource = null;

                comboBox4.DataSource = null;
                comboBox5.DataSource = null;
                comboBox6.DataSource = null;
            }

            OpenFileDialog file = new OpenFileDialog();
            file.InitialDirectory = "C:";
            file.Filter = "CSV File |*.csv";
            
            string filePathRes = "";
           
            if (file.ShowDialog() == DialogResult.OK)
            {
                filePathRes = file.FileName;

                var result = CSVHelper.readCSV(filePathRes, ',', true);

                result.Rows.RemoveAt(result.Rows.Count - 1);
                dataGridView1.DataSource = result;

                comboBox1.DataSource = CSVHelper.columnsName;
                comboBox2.DataSource = CSVHelper.columnsName.ToArray();
                comboBox3.DataSource = CSVHelper.columnsName.ToArray();
                comboBox4.DataSource = CSVHelper.columnsName.ToArray(); ;
                comboBox5.DataSource = CSVHelper.columnsName.ToArray(); ;
                comboBox6.DataSource = CSVHelper.columnsName.ToArray(); ;
            }            
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
            if (dataGridView1.DataSource != null)
            {
                AddRowForm addrow = new AddRowForm(dataGridView1);
                addrow.ShowDialog();
            }
            else {
                MessageBox.Show("Firstly, you have to choose a csv file!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null) {
                try
                {
                    DataTable data = (DataTable)dataGridView1.DataSource;
                    CSVHelper.exportDataTableAsCSV(data);

                    MessageBox.Show("Successfully saved the file on C disk.");
                }
                catch (Exception)
                {
                    MessageBox.Show("Error !");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null) {
                if (comboBox1.SelectedIndex != comboBox2.SelectedIndex)
                {
                    // do your job 
                    List<string> Xx = new List<string>();
                    List<string> Yy = new List<string>();

                    for(int i = dataGridView1.SelectedRows.Count - 1; i>= 0; i--)
                    {
                        Xx.Add(dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[comboBox1.SelectedIndex].Value.ToString());
                        Yy.Add(dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[comboBox2.SelectedIndex].Value.ToString());                                                
                    }
                    
                    GraphForm graphForm = new GraphForm(Xx, Yy, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), checkBox1.Checked);
                    graphForm.ShowDialog();
                }
                else {
                    MessageBox.Show("Please, choose different columns for charting!");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                if (comboBox3.SelectedItem != null)
                {
                    // do your job 
                    List<string> Xx = new List<string>();                    

                    for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                    {
                        Xx.Add(dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[comboBox3.SelectedIndex].Value.ToString());                        
                    }                    

                    PieChart pieChart = new PieChart(Xx, comboBox3.SelectedItem.ToString());
                    pieChart.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please, choose different columns for charting!");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                if (comboBox4.SelectedItem != null && comboBox5.SelectedItem != null && comboBox6.SelectedItem != null)
                {
                    if (comboBox4.SelectedItem != comboBox5.SelectedItem && comboBox4.SelectedItem != comboBox6.SelectedItem
                        && comboBox5.SelectedItem != comboBox6.SelectedItem)
                    {
                        List<string> Xx = new List<string>();
                        List<string> Yy = new List<string>();
                        List<string> Zz = new List<string>();

                        for (int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
                        {
                            Xx.Add(dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[comboBox4.SelectedIndex].Value.ToString());
                            Yy.Add(dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[comboBox5.SelectedIndex].Value.ToString());
                            Zz.Add(dataGridView1.Rows[dataGridView1.SelectedRows[i].Index].Cells[comboBox6.SelectedIndex].Value.ToString());
                        }

                        HeatMap heatMap = new HeatMap(Xx, Yy, Zz);
                        heatMap.ShowDialog(); 
                    }
                    else {
                        MessageBox.Show("Dimensions can't be same columns ! ");
                    }
                }
                else
                {
                    MessageBox.Show("Error !");
                }
            }
        }
    }
}
