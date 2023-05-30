using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Computer_magazine
{
    public partial class PasswordRestoreFrorm : Form
    {

        private int code;
        private string oldPassword;
        private string email;
        private string login;
        Form caller;
        public PasswordRestoreFrorm(String login, String style, Form caller)
        {
            InitializeComponent();

            this.caller = caller;

            label3.Text += "\"" + login + "\"";

            if (style.Equals("white"))
            {
                this.BackColor = Color.WhiteSmoke;

                label1.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
            }
            else
            {
                this.BackColor = Color.FromArgb(35, 35, 35);

                label1.ForeColor = Color.WhiteSmoke;
                label3.ForeColor = Color.WhiteSmoke;
                label4.ForeColor = Color.WhiteSmoke;
                label5.ForeColor = Color.WhiteSmoke;
            }

            this.login = login;

            try
            {
                using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                {
                    connection.Open();

                    String query = "select * from Users where user_login = @user_login collate Latin1_General_CS_AI";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add("@user_login", login);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        oldPassword = reader.GetValue(1).ToString();
                        email = reader.GetValue(3).ToString();

                        reader.Close();
                        command.Dispose();


                        sendCodeMessage();
                    }
                    else
                    {
                        MessageBox.Show($"User not found!", "User not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!oldPassword.Equals(textBox2.Text))
            {
                if (textBox2.Text.Length >= 5)
                {

                    if (textBox2.Text.Equals(textBox3.Text))
                    {
                        try
                        {
                            using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                            {
                                connection.Open();

                                String query = "update Users set user_password = @user_passord where user_login = @user_login";

                                SqlCommand command = new SqlCommand(query, connection);
                                command.Parameters.Add("@user_passord", textBox2.Text);
                                command.Parameters.Add("@user_login", login);

                                //auto regiistration
                                ((RegistrationForm)caller).setLogin(login);
                                ((RegistrationForm)caller).setPassword(textBox2.Text);

                                command.ExecuteNonQuery();

                                MessageBox.Show("Password successfully changed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                sendResultMessage();
                                this.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Passwords are not matching!", "Invalind confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show($"Passwords should be 5 or more symbols!", "Short password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Enter new password!", "New Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(code.ToString())){
                textBox1.Visible= false;
                button1.Visible= false;
                label1.Visible= false;

                label3.Visible= true;
                label4.Visible= true;
                label5.Visible= true;

                textBox2.Visible= true;
                textBox3.Visible= true;

                button2.Visible= true;
            }
            else
            {
                MessageBox.Show($"Verification code is incorrect! Resanding...", "Wrong Verification code", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sendCodeMessage();
                textBox1.Text = "";
            }
        }



        private void sendCodeMessage()
        {
            Random randCode = new Random();
            code = randCode.Next(1000, 9999);

            //Создаем письмо
            MailAddress from = new MailAddress("microbot2033@gmail.com", "MicroBotSupport");
            //MailAddress to = new MailAddress("botannicolai22@gmail.com", login);
            MailAddress to = new MailAddress(email, login);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Restoring your passsword";
            message.Body = $"<h1>We are restoring your password now</h1><br><br><b>Dear {login}!</b><br>To continue restoring the password enter the following code in your App: <b>{code}</b>";
            message.IsBodyHtml = true;


            //Создаем клиента (не пользователя), через который мы будем отправлять письмо
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //smtp.gmail.com - сервис Gmail; 587 - порт для писем

            client.Credentials = new NetworkCredential("microbot2033@gmail.com", "gohxzhvswujhezie"); //тут указываем почту, с которой будем отправлять и указываем пароль. !ACHTUNG! Это пароль - это не простой пароль от почты, а пароль приложения. Его можно создать в настройках своего Гугл аккаунта. Просто в строке поиска пишешь "Пароли приложений" -> из выпадающего списска с приложения ми выбираешь "другое", пишешь имя и копируешь пароль в оранджевом окошке. Вот его и надо использовать...
                                                                                                      //Подробнее о том, как это сделать смотри тут: https://www.youtube.com/watch?v=tu9QS_t5o1k
                                                                                                      //Ранние методы не рабочие, так как поилитики безопасности Google изменились!!!
            client.EnableSsl = true; //Для безопасного подключения
            client.Send(message);

            //MessageBox.Show("Sent!");
        }

        private void sendResultMessage()
        {
            MailAddress from = new MailAddress("microbot2033@gmail.com", "MicroBotSupport");
            MailAddress to = new MailAddress(email, login);
            MailMessage message = new MailMessage(from, to);

            message.Subject = "Success!";
            message.Body = $"<h1>Password Succcessfully changed!</h1><br><br>Dear {login}!<br>Wour password has been successfully changed to <b>{textBox2.Text}</b>...<br><br>Have a good day!";
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl= true;
            client.Credentials = new NetworkCredential("microbot2033@gmail.com", "gohxzhvswujhezie");

            client.Send(message);
        }
    }
}
