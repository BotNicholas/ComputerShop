﻿using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Computer_magazine
{
    public partial class PrinterEditForm : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True");
        private ErrorProvider error;
        private Form caller;
        private string mode;
        private int old_id;

        public PrinterEditForm(Form caller, string mode)
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
                checkBox1.ForeColor = Color.Black;
                dataGridView1.BackgroundColor = Color.WhiteSmoke;
            }
            else
            {
                this.BackColor = Color.FromArgb(35, 35, 35);
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White; 
                checkBox1.ForeColor = Color.White;
                dataGridView1.BackgroundColor = Color.FromArgb(35, 35, 35);
            }

            if (mode == "add")
            {
                this.Width = 250;
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
                SqlCommand command = new SqlCommand("select cod_prod from Product where Cod_type = 2050", connection); //2050 - Printers id;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetValue(0).ToString());
                }

                comboBox1.Sorted = true;
                comboBox1.SelectedIndex = 0;

            }
        }

        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                mode = "delete";
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                button1.Visible= false;
            }
            else
            {
                mode = "edit";
                comboBox1.Enabled = true;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button1.Visible= true;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mode == "edit")
            {
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                old_id = Convert.ToInt32(comboBox1.Text);
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
                            string query = $"delete from Printer where cod_prod = {dataGridView1.Rows[e.RowIndex].Cells[0].Value}";

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

            if (mode!= "delete" && (!textBox1.Text.Equals("color")) && (!textBox1.Text.Equals("laser")))
            {
                flag = !flag;
                error.SetError(textBox1, "Printer type should be only color or laser");
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
                            query = $"insert into Printer values(@cod_prod, @Printer_type, @Applic)";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@cod_prod", comboBox1.Text);
                            command.Parameters.AddWithValue("@Printer_type", textBox1.Text);
                            command.Parameters.AddWithValue("@Applic", textBox2.Text);

                            MessageBox.Show($"Success! {command.ExecuteNonQuery()} rows affected!");
                            Close();
                        }
                        else if (mode == "edit")
                        {
                            //query = $"update Phone set cod_prod = {Convert.ToInt32(comboBox1.Text)}, CPU_freq = {Convert.ToInt32(textBox1.Text)}, RAM = {Convert.ToInt32(textBox2.Text)}, Intern_mem = {Convert.ToInt32(textBox3.Text)}, Applic = '{textBox4.Text}' where cod_prod = {old_id}";
                            query = $"update Printer set cod_prod = @cod_prod, Printer_type = @Printer_type, Applic = @Applic where cod_prod = {old_id}";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@cod_prod", comboBox1.Text);
                            command.Parameters.AddWithValue("@Printer_type", textBox1.Text);
                            command.Parameters.AddWithValue("@Applic", textBox2.Text);

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

                    string query = "select * from Printer";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.Visible = true;
                    dataGridView1.ColumnCount = 3;
                    int i = 0;

                    while (reader.Read())
                    {
                        //MessageBox.Show($"{reader.GetValue(0)}  {reader.GetValue(1)}    {reader.GetValue(2)}    {reader.GetValue(3)}");
                        dataGridView1.RowCount++;
                        dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                        dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                        dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Printer type";
                    dataGridView1.Columns[2].HeaderCell.Value = "Application";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
