using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Computer_magazine
{
    public partial class RolesEditForm : Form
    {
        private Form caller;
        private string mode = "edit";
        private int your_row = -1;
        private int admins_count = 0;
        private string userToEdit;
        public RolesEditForm(Form caller)
        {
            InitializeComponent();
            this.caller = caller;

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

            comboBox1.Items.Add("user");
            comboBox1.Items.Add("seller");
            comboBox1.Items.Add("manager");
            comboBox1.Items.Add("admin");

            updateDG();
        }






        private void updateDG() {
            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("select * from Users", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 3;
                    int i = 0;
                    your_row = -1;
                    admins_count = 0;
                    dataGridView1.Rows.Clear();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //MessageBox.Show(reader.GetValue(0) + " == " + ((Form1)caller).getUserLogin() + " - " + (reader.GetValue(0).Equals(((Form1)caller).getUserLogin())));
                            if (reader.GetValue(0).Equals(((Form1)caller).getUserLogin()))
                                your_row = i;

                            if (reader.GetValue(2).Equals("admin"))
                                admins_count++;

                            dataGridView1.RowCount++;
                            dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                            dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                            dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                            dataGridView1.Rows[i].HeaderCell.Value = (i+1).ToString();
                            i++;
                        }

                        dataGridView1.Columns[0].HeaderCell.Value = "User login";
                        dataGridView1.Columns[1].HeaderCell.Value = "User password";
                        dataGridView1.Columns[2].HeaderCell.Value = "User role";
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error has been occured: "+ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(mode == "edit")
            {
                //MessageBox.Show($"e.RowIndex({e.RowIndex}) == your_row({your_row}) - " + (e.RowIndex == your_row).ToString());
                if(e.RowIndex == your_row)
                    comboBox1.Enabled= false;
                else
                    comboBox1.Enabled = true;


                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                userToEdit = textBox3.Text;
            }

            if(mode == "delete")
            {
                if (!dataGridView1.Rows[e.RowIndex].Cells[2].Value.Equals("admin"))
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand("delete from Users where user_login = @user_login", connection);
                            command.Parameters.Add("@user_login", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                            if (MessageBox.Show("Are you sure?", "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                command.ExecuteNonQuery();
                            }

                            updateDG();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error has been occured: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else if (admins_count > 1)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand("delete from Users where user_login = @user_login", connection);
                            command.Parameters.Add("@user_login", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                            if (MessageBox.Show("Are you sure?", "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                command.ExecuteNonQuery();

                                if (e.RowIndex == your_row)
                                {
                                    caller.Close();
                                }
                            }

                            updateDG();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error has been occured: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                        MessageBox.Show("There are only one admin, you can't delete him!", "Denied!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox3.Text!="" && textBox1.Text != "")
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand($"update Users set user_login = @user_login, user_password = @user_password, user_role = @user_role where user_login = '{userToEdit}'", connection);

                        command.Parameters.Add("@user_login", textBox3.Text);
                        command.Parameters.Add("@user_password", textBox1.Text);
                        command.Parameters.Add("@user_role", comboBox1.Text);

                        int result = command.ExecuteNonQuery();

                        if(result > 0)
                        {
                            MessageBox.Show("Success! " + result.ToString() + " rows affected!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            updateDG();
                        }


                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("An error has been occured: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(mode == "edit")
            {
                mode = "delete";
                button1.Visible= false;


                textBox3.Text = "";
                textBox1.Text = "";
                comboBox1.Text = "";

                textBox3.Enabled = false;
                textBox1.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                mode = "edit";
                button1.Visible= true;

                textBox3.Enabled = true;
                textBox1.Enabled = true;
                comboBox1.Enabled = true;
            }
        }
    }
}
