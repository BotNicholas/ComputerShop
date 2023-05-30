﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Computer_magazine
{
    public partial class ConsignmentEditForm : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True");
        private ErrorProvider error;
        private Form caller;
        private string mode;
        private int old_id;
        private int old_prod_id;

        public ConsignmentEditForm(Form caller, string mode)
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
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
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
                label5.ForeColor = Color.White;
                label6.ForeColor = Color.White;
                label7.ForeColor = Color.White;
                checkBox1.ForeColor = Color.White;
                dataGridView1.BackgroundColor = Color.FromArgb(35, 35, 35);
            }

            if (mode == "add")
            {
                this.Width = 350;
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
                SqlCommand command = new SqlCommand("select Sup_code from supplier", connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetValue(0).ToString());
                }

                comboBox1.Sorted = true;
                comboBox1.SelectedIndex = 0;

                command = new SqlCommand("select cod_prod from Product", connection);

                reader.Close();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetValue(0).ToString());
                }

                comboBox2.Sorted = true;
                comboBox2.SelectedIndex = 0;



                command = new SqlCommand("select Man_code from manufacturer", connection);

                reader.Close();

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox3.Items.Add(reader.GetValue(0).ToString());
                }

                comboBox3.Sorted = true;
                comboBox3.SelectedIndex = 0;
            }


        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar) != 8 && !Regex.IsMatch(textBox1.Text + e.KeyChar, "^\\d+$"))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar) != 8 && !Regex.IsMatch(textBox2.Text + e.KeyChar, "^\\d+$"))
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                mode = "delete";
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                dateTimePicker1.Enabled = false;
                button1.Visible= false;
            }
            else
            {
                mode = "edit";
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                dateTimePicker1.Enabled = true;
                button1.Visible = true;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mode == "edit")
            {
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                old_id = Convert.ToInt32(textBox2.Text);
                old_prod_id = Convert.ToInt32(comboBox2.Text);

                //MessageBox.Show($"{old_id} and {old_prod_id}");
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
                            string query = $"delete from consignment where consignment_number = {dataGridView1.Rows[e.RowIndex].Cells[4].Value} and cod_prod = {dataGridView1.Rows[e.RowIndex].Cells[1].Value}";

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
                            query = $"insert into consignment values(@Sup_code, @cod_prod, @prod_number, @Man_code, @consignment_number, @Consignment_date, @Price)";

                            SqlCommand command = new SqlCommand(query, connection);

                            //MessageBox.Show($"{dateTimePicker1.Value.Year}-{dateTimePicker1.Value.Month}-{dateTimePicker1.Value.Day}");

                            command.Parameters.AddWithValue("@Sup_code", comboBox1.Text);
                            command.Parameters.AddWithValue("@cod_prod", comboBox2.Text);
                            command.Parameters.AddWithValue("@prod_number", textBox1.Text);
                            command.Parameters.AddWithValue("@Man_code", comboBox3.Text);
                            command.Parameters.AddWithValue("@consignment_number", textBox2.Text);
                            command.Parameters.AddWithValue("@Consignment_date", dateTimePicker1.Value);
                            command.Parameters.AddWithValue("@Price", textBox3.Text);

                            MessageBox.Show($"Success! {command.ExecuteNonQuery()} rows affected!");

                            Close();
                        }
                        else if (mode == "edit")
                        {
                            //query = $"update Monitor set cod_prod = {Convert.ToInt32(comboBox1.Text)}, Tatrix_type = '{textBox2.Text}', Diagonal = {Convert.ToInt32(textBox3.Text)}, Monitor_type = '{textBox4.Text}', Matrix_size = '{textBox5.Text}', Applic = '{textBox6.Text}' where cod_prod = {old_id}";
                            query = $"update consignment set Sup_code = @Sup_code, cod_prod = @cod_prod,  prod_number = @prod_number, Man_code = @Man_code, consignment_number = @consignment_number, Consignment_date = @Consignment_date, Price = @Price where consignment_number = {old_id} and cod_prod = {old_prod_id}";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@Sup_code", comboBox1.Text);
                            command.Parameters.AddWithValue("@cod_prod", comboBox2.Text);
                            command.Parameters.AddWithValue("@prod_number", textBox1.Text);
                            command.Parameters.AddWithValue("@Man_code", comboBox3.Text);
                            command.Parameters.AddWithValue("@consignment_number", textBox2.Text);
                            command.Parameters.AddWithValue("@Consignment_date", dateTimePicker1.Value);
                            command.Parameters.AddWithValue("@Price", textBox3.Text);

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

                    string query = "select * from consignment";
                    SqlCommand command = new SqlCommand(query, connection);

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
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Supplier id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Product id";
                    dataGridView1.Columns[2].HeaderCell.Value = "Product amount";
                    dataGridView1.Columns[3].HeaderCell.Value = "Manufacturer id";
                    dataGridView1.Columns[4].HeaderCell.Value = "Consignment number";
                    dataGridView1.Columns[5].HeaderCell.Value = "Consignment date";
                    dataGridView1.Columns[6].HeaderCell.Value = "Price";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
