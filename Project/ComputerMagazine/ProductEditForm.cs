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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Computer_magazine
{
    public partial class ProductEditForm : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True");
        private ErrorProvider error;
        private Form caller;
        private string mode;
        private int old_id;
        private int max_id;

        public ProductEditForm(Form caller, string mode)
        {
            InitializeComponent();

            //openFileDialog1.FileName = "";

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
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
                label8.ForeColor = Color.Black;
                checkBox1.ForeColor = Color.MidnightBlue;
                dataGridView1.BackgroundColor = Color.WhiteSmoke;
            }
            else
            {
                this.BackColor = Color.FromArgb(35, 35, 35);
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                label7.ForeColor = Color.White;
                label8.ForeColor = Color.DeepSkyBlue;
                checkBox1.ForeColor = Color.White;
                dataGridView1.BackgroundColor = Color.FromArgb(35, 35, 35);
            }


            if (mode == "add")
            {
                this.Width = 280;
                checkBox1.Visible = false;
            }
            else if (mode == "edit")
            {
                button1.BackgroundImage = Image.FromFile("../../resources/pencil.png");
                checkBox1.Visible = true;
                updateDG();

                //fill the fields with the first line
            }

            connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";


            using (connection)
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select Cod_type from Product_type", connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetValue(0).ToString());
                }

                comboBox1.Sorted = true;
                comboBox1.SelectedIndex = 0;

                command = new SqlCommand("select Man_code from manufacturer", connection);

                reader.Close();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetValue(0).ToString());
                }

                comboBox2.Sorted = true;
                comboBox2.SelectedIndex = 0;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar) != 8 && !Regex.IsMatch(textBox1.Text + e.KeyChar, "^\\d+$"))
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar) != 8 && !Regex.IsMatch(textBox3.Text + e.KeyChar, "^\\d+$"))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar) != 8 && !Regex.IsMatch(textBox4.Text + e.KeyChar, "^\\d+$"))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                mode = "delete";
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                button1.Visible= false;
                label8.Enabled=false;
            }
            else
            {
                mode = "edit";
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                button1.Visible = true;
                label8.Enabled = true;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mode == "edit")
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();                
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();

                openFileDialog1.FileName = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

                String[] filePath = openFileDialog1.FileName.Replace("/", "\\").Split('\\');
                label8.Text = filePath[filePath.Length - 1];

                old_id = Convert.ToInt32(textBox1.Text);

            }
            else if (mode == "delete")
            {
                if (MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
                        using (connection)
                        {
                            connection.Open();
                            string query = $"delete from Product where cod_prod = {dataGridView1.Rows[e.RowIndex].Cells[0].Value}";

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

            if (mode == "add" && Convert.ToInt32(textBox1.Text) <= max_id)
            {
                error.SetError(textBox1, $"Id shoud be more than {max_id}");
                flag = false;
            }

            //MessageBox.Show(openFileDialog1.FileName);

            if(openFileDialog1.FileName == "")
            {
                error.SetError(label8, "Choose a picture");
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

                        if (mode == "add")
                        {
                            //query = $"insert into Monitor values({Convert.ToInt32(comboBox1.Text)}, '{textBox2.Text}', {Convert.ToInt32(textBox3.Text)}, '{textBox4.Text}', '{textBox5.Text}', '{textBox6.Text}')";
                            query = $"insert into Product values(@cod_prod, @Cod_type, @Man_cod, @Prod_name, @Price, @Waranty, @Picture)";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@cod_prod", textBox1.Text);
                            command.Parameters.AddWithValue("@Cod_type", comboBox1.Text);
                            command.Parameters.AddWithValue("@Man_cod", comboBox2.Text);
                            command.Parameters.AddWithValue("@Prod_name", textBox2.Text);
                            command.Parameters.AddWithValue("@Price", textBox3.Text);
                            command.Parameters.AddWithValue("@Waranty", textBox4.Text);
                            command.Parameters.AddWithValue("@Picture", openFileDialog1.FileName);

                            MessageBox.Show($"Success! {command.ExecuteNonQuery()} rows affected!");

                            Close();
                        }
                        else if (mode == "edit")
                        {
                            //query = $"update Monitor set cod_prod = {Convert.ToInt32(comboBox1.Text)}, Tatrix_type = '{textBox2.Text}', Diagonal = {Convert.ToInt32(textBox3.Text)}, Monitor_type = '{textBox4.Text}', Matrix_size = '{textBox5.Text}', Applic = '{textBox6.Text}' where cod_prod = {old_id}";
                            query = $"update Product set cod_prod = @cod_prod, Cod_type = @Cod_type,  Man_cod = @Man_cod, Prod_name = @Prod_name, Price = @Price, Waranty = @Waranty, Picture = @Picture where cod_prod = {old_id}";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@cod_prod", textBox1.Text);
                            command.Parameters.AddWithValue("@Cod_type", comboBox1.Text);
                            command.Parameters.AddWithValue("@Man_cod", comboBox2.Text);
                            command.Parameters.AddWithValue("@Prod_name", textBox2.Text);
                            command.Parameters.AddWithValue("@Price", textBox3.Text);
                            command.Parameters.AddWithValue("@Waranty", textBox4.Text);
                            //MessageBox.Show(openFileDialog1.FileName);
                            command.Parameters.AddWithValue("@Picture", openFileDialog1.FileName);

                            MessageBox.Show($"Success! {command.ExecuteNonQuery()} rows affected!");


                            //Close();
                        }
                        else if (mode == "delete")
                        {
                            //Close();
                        }
                    }
                    if (mode != "add")
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


                    string query = "select max(cod_prod) from Product";
                    SqlCommand command = new SqlCommand(query, connection);

                    max_id = (int)command.ExecuteScalar();


                    query = "select * from Product";
                    command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.Visible = true;
                    dataGridView1.ColumnCount = 7;
                    int i = 0;

                    while (reader.Read())
                    {
                        //MessageBox.Show($"{reader.GetValue(0)}  {reader.GetValue(1)}    {reader.GetValue(2)}    {reader.GetValue(3)}");
                        dataGridView1.RowCount++;
                        dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                        dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                        dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                        dataGridView1.Rows[i].Cells[3].Value = reader.GetValue(3);
                        dataGridView1.Rows[i].Cells[4].Value = reader.GetValue(4);
                        dataGridView1.Rows[i].Cells[5].Value = reader.GetValue(5);
                        dataGridView1.Rows[i].Cells[6].Value = reader.GetValue(6);
                        dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Production type id";
                    dataGridView1.Columns[2].HeaderCell.Value = "Manufacturer id";
                    dataGridView1.Columns[3].HeaderCell.Value = "Product name";
                    dataGridView1.Columns[4].HeaderCell.Value = "Product price";
                    dataGridView1.Columns[5].HeaderCell.Value = "Waranty";
                    dataGridView1.Columns[6].HeaderCell.Value = "Picture";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String[] filePath = openFileDialog1.FileName.Replace("/", "\\").Split('\\');
                label8.Text = filePath[filePath.Length-1];
            }
        }
    }
}
