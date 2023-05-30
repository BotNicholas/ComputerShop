using System;
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

namespace Computer_magazine
{
    public partial class RegistrationForm : Form
    {
        Form caller;
        string mode = "signIn";
        public RegistrationForm(Form caller)
        {
            InitializeComponent();
            this.caller = caller;

            if(((Form1)caller).getStyle() == "white")
            {
                this.BackColor = Color.WhiteSmoke;

                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
                label9.ForeColor = Color.Black;
                button1.ForeColor = Color.Black;
            }
            else
            {
                this.BackColor = Color.FromArgb(35, 35, 35);

                label1.ForeColor = Color.WhiteSmoke;
                label2.ForeColor = Color.WhiteSmoke;
                label3.ForeColor = Color.WhiteSmoke;
                label5.ForeColor = Color.WhiteSmoke;
                label6.ForeColor = Color.WhiteSmoke;
                label7.ForeColor = Color.WhiteSmoke;
                label9.ForeColor = Color.WhiteSmoke;
                button1.ForeColor = Color.WhiteSmoke;
            }
        }

        private void RegistrationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            caller.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (mode == "signIn")
            {
                mode = "signUp";
                label5.Text = "Sign Up";
                label3.Text = "Already have an acount?";
                label4.Text = "Sign In";
                button1.Text = "Sign Up";
                label6.Visible = true;
                textBox2.Visible = true;
                label7.Visible= true;
                textBox4.Visible = true;
                label8.Visible = false;
                label9.Visible = false;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";

            }
            else {
                mode = "signIn";
                label5.Text = "Sign In";
                label3.Text = "Don't have an acount?";
                label4.Text = "Sign Up";
                button1.Text = "Sign In";
                label6.Visible = false;
                textBox2.Visible = false;
                label7.Visible = false;
                textBox4.Visible = false;
                label8.Visible = false;
                label9.Visible = false;

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (mode == "signIn")
                {
                    using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand("select * from Users where user_login =  @user_login collate Latin1_General_CS_AI", connection);
                        command.Parameters.Add("@user_login", textBox3.Text);

                        SqlDataReader reader = command.ExecuteReader();

                        //MessageBox.Show(reader.HasRows.ToString());

                        if (reader.HasRows)
                        {
                            reader.Read();
                            //MessageBox.Show(reader.GetValue(0).ToString() + " " + reader.GetValue(1).ToString() + " " + reader.GetValue(2).ToString());
                            if(textBox1.Text == reader.GetValue(1).ToString())
                            {
                                //MessageBox.Show(reader.GetValue(0).ToString() + " " + reader.GetValue(1).ToString() + " " + reader.GetValue(2).ToString());
                                ((Form1)caller).setUser(reader.GetValue(2).ToString());
                                ((Form1)caller).setUserLogin(reader.GetValue(0).ToString());
                                caller.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Wrong password!", "Wrong password!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                label8.Visible = true;
                                label9.Visible = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Such user has not been found!", "User no found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        reader.Close();
                    }
                }
                else
                {
                    if(textBox3.Text != "")
                    {
                        if (textBox1.Text.Length >= 5)
                        {
                            if (textBox1.Text == textBox2.Text)
                            {
                                if (Regex.IsMatch(textBox4.Text, "^.+\\@.+\\..+$"))
                                {
                                    using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                                    {
                                        connection.Open();

                                        SqlCommand command = new SqlCommand("select * from Users where user_login = @user_login collate Latin1_General_CS_AI", connection);
                                        command.Parameters.Add("@user_login", textBox3.Text);

                                        SqlDataReader reader = command.ExecuteReader();

                                        //MessageBox.Show(reader.HasRows.ToString());

                                        if (!reader.HasRows)
                                        {
                                            reader.Close();

                                            command.CommandText = "select * from Users where user_email = @user_email";
                                            command.Parameters.Clear();
                                            command.Parameters.Add("user_email", textBox4.Text);

                                            reader = command.ExecuteReader();

                                            if (!reader.HasRows)
                                            {
                                                reader.Close();

                                                command.CommandText = "insert into Users values (@user_login, @user_password, 'user', @user_email)";
                                                command.Parameters.Clear();
                                                command.Parameters.Add("@user_login", textBox3.Text);
                                                command.Parameters.Add("@user_password", textBox1.Text);
                                                command.Parameters.Add("@user_email", textBox4.Text);

                                                if (command.ExecuteNonQuery() > 0)
                                                {
                                                    ((Form1)caller).setUser("user");
                                                    ((Form1)caller).setUserLogin(textBox3.Text);
                                                    this.Hide();
                                                    caller.Show();
                                                }
                                                else
                                                {
                                                    MessageBox.Show("An error occured!", "User adding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("This email is already used!", "Unavailable email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Such user already exists!", "User exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }

                                        reader.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please enter the email in the folowing format: example@gmail.com!", "Wrong email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Passwords don't match!", "Passwords don't match", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password should contain at least 5 chars!", "Password's to short", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter User name!", "Empty user name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            PasswordRestoreFrorm passwordRestoreFrorm = new PasswordRestoreFrorm(textBox3.Text, ((Form1)caller).getStyle(), this);
            passwordRestoreFrorm.ShowDialog();
        }



        public void setLogin(String login)
        {
            textBox3.Text = login;
        }

        public void setPassword(String password)
        {
            textBox1.Text = password;
        }
    }
}
