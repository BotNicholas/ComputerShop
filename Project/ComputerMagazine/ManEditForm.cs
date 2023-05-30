using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_magazine
{
    public partial class ManEditForm : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True");
        private ErrorProvider error;
        private Form caller;
        private string mode;
        private int max_id;
        private int old_id;

        public ManEditForm(Form caller, string mode)
        {
            InitializeComponent();

            error = new ErrorProvider();
            this.caller = caller;
            this.mode = mode;

            if (((Form1)caller).getStyle().Equals("white"))
            {
                this.BackColor = Color.WhiteSmoke;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                checkBox1.ForeColor = Color.Black;
                dataGridView1.BackgroundColor = Color.WhiteSmoke;
            }
            else
            {
                this.BackColor = Color.FromArgb(35, 35, 35);
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                checkBox1.ForeColor = Color.White;
                dataGridView1.BackgroundColor = Color.FromArgb(35, 35, 35);
            }

            if(mode == "add")
            {
                this.Width = 365;
                checkBox1.Visible = false;
            } else if(mode == "edit")
            {
                button1.BackgroundImage = Image.FromFile("../../resources/pencil.png");
                checkBox1.Visible = true;
                updateDG();

                //fill the fields with the first line
            }
                
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(((int)e.KeyChar)!=8 && !Regex.IsMatch(textBox1.Text + e.KeyChar, "^\\d+$"))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar) != 8 && !Regex.IsMatch(textBox4.Text + e.KeyChar, "^\\-?\\d+\\.?\\d*$"))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                mode = "delete";
                textBox1.Enabled= false;
                textBox2.Enabled= false;
                textBox3.Enabled= false;
                textBox4.Enabled= false;
                button1.Visible = false;
            }
            else
            {
                mode = "edit";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                button1.Visible = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(mode == "edit")
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                old_id = Convert.ToInt32(textBox1.Text);
            } else if( mode == "delete")
            {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
                        using (connection)
                        {
                            connection.Open();
                            string query = $"delete from manufacturer where Man_code = {dataGridView1.Rows[e.RowIndex].Cells[0].Value}";

                            SqlCommand command = new SqlCommand(query, connection);

                            MessageBox.Show($"Success! {command.ExecuteNonQuery()} rows affected!");
                        }

                        updateDG();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            error.Clear();
            bool flag = true;

            if(mode == "add" && Convert.ToInt32(textBox1.Text) <= max_id)
            {
                error.SetError(textBox1, $"Id shoud be more than {max_id}");
                flag = false;
            }

            if(mode!="delete" && Convert.ToInt32(textBox4.Text) < 0)
            {
                error.SetError(textBox4, "Price shoud be more than 0!");
                flag = false;
            }

            if (flag)
            {
                try
                {
                    connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
                    using (connection)
                    {
                        connection.Open();
                        string query;

                        if(mode == "add")
                        {
                            //query = $"insert into manufacturer values({Convert.ToInt32(textBox1.Text)}, '{textBox2.Text}', '{textBox3.Text}', {Convert.ToInt32(textBox4.Text)})";
                            query = "insert into manufacturer values(@Man_code, @Man_name, @Man_Adres, @Man_Production_price)";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@Man_code", textBox1.Text);
                            command.Parameters.AddWithValue("@Man_name", textBox2.Text);
                            command.Parameters.AddWithValue("@Man_Adres", textBox3.Text);
                            command.Parameters.AddWithValue("@Man_Production_price", textBox4.Text);


                            MessageBox.Show($"Success! {command.ExecuteNonQuery()} rows affected!");

                            Close();
                        }
                        else if(mode == "edit")
                        {
                            //query = $"update manufacturer set Man_code = {Convert.ToInt32(textBox1.Text)}, Man_name = '{textBox2.Text}', Man_Adres = '{textBox3.Text}', Man_Production_price = {Convert.ToInt32(textBox4.Text)} where Man_code = {old_id}";
                            query = $"update manufacturer set Man_code = @Man_code, Man_name = @Man_name, Man_Adres = @Man_Adres, Man_Production_price = @Man_Production_price where Man_code = {old_id}";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@Man_code", textBox1.Text);
                            command.Parameters.AddWithValue("@Man_name", textBox2.Text);
                            command.Parameters.AddWithValue("@Man_Adres", textBox3.Text);
                            command.Parameters.AddWithValue("@Man_Production_price", textBox4.Text);

                            MessageBox.Show($"Success! {command.ExecuteNonQuery()} rows affected!");


                            //Close();
                        }
                        else if(mode == "delete")
                        {
                            //Close();
                        }
                    }
                    if(mode != "add")
                        updateDG();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void updateDG()
        {
            try
            {
                dataGridView1.Visible = true;
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();


                connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
                using (connection)
                {
                    connection.Open();

                    string query = "select max(Man_code) from manufacturer";
                    SqlCommand command = new SqlCommand(query, connection);

                    max_id = (int)command.ExecuteScalar();


                    query = "select * from manufacturer";
                    command.CommandText = query;

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.Visible = true;
                    dataGridView1.ColumnCount = 4;
                    int i = 0;

                    while (reader.Read())
                    {
                        dataGridView1.RowCount++;
                        dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                        dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                        dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                        dataGridView1.Rows[i].Cells[3].Value = reader.GetValue(3);
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }

                    dataGridView1.Columns[0].HeaderCell.Value = "Manufacturer id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Manufacturer name";
                    dataGridView1.Columns[2].HeaderCell.Value = "Manufacturer adres";
                    dataGridView1.Columns[3].HeaderCell.Value = "Production price";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
