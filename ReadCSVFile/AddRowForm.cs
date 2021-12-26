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
    public partial class AddRowForm : Form
    {
        DataGridView gridViewFrm1;
        List<TextBox> texts; 
        public AddRowForm(DataGridView dataGrid)
        {
            InitializeComponent();
            gridViewFrm1 = dataGrid;
            texts = new List<TextBox>();
        }

        
        private void AddRowForm_Load(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gridViewFrm1.DataSource;
            int field = 20; 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Label lbl = new Label();
                lbl.Text = dt.Columns[i].ColumnName.Trim();
                lbl.SetBounds(50, field, 40, 20);
                this.Controls.Add(lbl);

                TextBox txt = new TextBox();
                txt.SetBounds(100, field, 70, 20);
                this.Controls.Add(txt);
                this.texts.Add(txt); 
                field = field + 25;
            }

            Button button = new Button();
            button.Text = "Add Row";
            button.SetBounds(50, field + 10, 80, 20);
            button.Click += new EventHandler(button_click);
            this.Controls.Add(button);  
        }

        private void button_click(object sender, EventArgs e)
        {
            bool control = true; 
            List<string> textString = new List<string>(); 
            foreach (TextBox item in texts)
            {
                if (string.IsNullOrWhiteSpace(item.Text))
                {
                    MessageBox.Show("You can't add empty information to the table!");
                    control = false; 
                    Close(); 
                    Dispose(); 
                   
                    break;
                }

                textString.Add(item.Text); 
            }

            if (control == true) {

                string[] arguments = new string[textString.Count];
                for (int i = 0; i < textString.Count; i++)
                {
                    arguments[i] = textString[i];
                }

                try
                {
                    DataTable dt = (DataTable)gridViewFrm1.DataSource;                                       
                    dt.Rows.Add(arguments);
                    gridViewFrm1.DataSource = dt;
                    this.Dispose();
                }
                catch (Exception)
                {
                    MessageBox.Show("Error, check what you typed");
                }
            }
        }
    }
}
 