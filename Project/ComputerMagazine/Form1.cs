using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_magazine
{
    public partial class Form1 : Form
    {
        private string cfg = "../../config/config.cfg";
        Dictionary<string, string> config;
        private string table;
        private ErrorProvider error = new ErrorProvider();
        private string user;
        private string user_login;

        Computer_magazineDataSet ds;

        private string style;

        public Form1()
        {
            InitializeComponent();


            config = new Dictionary<string, string>();


            //reading saved config
            using (StreamReader file = new StreamReader(cfg))
            {
                string[] lines = file.ReadToEnd().Split('\n');
                
                foreach(string line in lines)
                {
                    //MessageBox.Show(line);
                    if(line != "")
                        config.Add((line.Split(':'))[0], (line.Split(':'))[1]);
                }
            }

            if (config.ContainsKey("style"))
            {
                //style = config["style"];
                config.TryGetValue("style", out style);
                style = style.Replace("\r", ""); //\r - perenos karetki

                //MessageBox.Show(style);

                if (style.Equals("white"))
                {
                    this.BackColor = Color.WhiteSmoke;
                    dataGridView1.BackgroundColor= Color.WhiteSmoke;
                    menuStrip1.BackColor= Color.Gainsboro;
                    panel1.BackColor= Color.LightGray;
                    panel2.BackColor= Color.LightGray;
                    panel3.BackColor= Color.LightGray;
                    tablesToolStripMenuItem.BackColor= Color.LightGray;
                    settingsToolStripMenuItem.BackColor= Color.LightGray;
                    reportsToolStripMenuItem.BackColor= Color.LightGray;
                    rolesToolStripMenuItem.BackColor = Color.LightGray;

                    label1.ForeColor= Color.Black;
                    label2.ForeColor= Color.Black;
                    label3.ForeColor= Color.Black;
                    label4.ForeColor= Color.Black;
                    label5.ForeColor= Color.Black;
                    label6.ForeColor= Color.Black;
                    label7.ForeColor= Color.Black;
                    checkBox1.ForeColor= Color.Black;
                    label8.ForeColor= Color.Black;
                    label9.ForeColor= Color.Black;
                    label10.ForeColor= Color.Black;
                    label11.ForeColor= Color.Black;
                    label12.ForeColor= Color.Black;
                    label13.ForeColor= Color.Black;
                }
                else
                {
                    this.BackColor = Color.FromArgb(35, 35, 35);
                    dataGridView1.BackgroundColor = Color.FromArgb(35, 35, 35);
                    menuStrip1.BackColor = Color.DimGray;
                    panel1.BackColor = Color.Gray;
                    panel2.BackColor = Color.Gray;
                    panel3.BackColor = Color.Gray;
                    tablesToolStripMenuItem.BackColor = Color.Gray;
                    settingsToolStripMenuItem.BackColor = Color.Gray;
                    reportsToolStripMenuItem.BackColor = Color.Gray;
                    rolesToolStripMenuItem.BackColor = Color.Gray;

                    label1.ForeColor = Color.WhiteSmoke;
                    label2.ForeColor = Color.WhiteSmoke;
                    label3.ForeColor = Color.WhiteSmoke;
                    label4.ForeColor = Color.WhiteSmoke;
                    label5.ForeColor = Color.WhiteSmoke;
                    label6.ForeColor = Color.WhiteSmoke;
                    label7.ForeColor = Color.WhiteSmoke;
                    checkBox1.ForeColor = Color.WhiteSmoke;
                    label8.ForeColor = Color.WhiteSmoke;
                    label9.ForeColor = Color.WhiteSmoke;
                    label10.ForeColor = Color.WhiteSmoke;
                    label11.ForeColor = Color.WhiteSmoke;
                    label12.ForeColor = Color.WhiteSmoke;
                    label13.ForeColor = Color.WhiteSmoke;
                }
            }


            comboBox1.Items.Add("Consign.№");
            comboBox1.Items.Add("Date");

            //ds = new Computer_magazineDataSet();

            //string connection_string = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";

            //using (SqlConnection connection = new SqlConnection(connection_string))
            //{
            //    connection.Open();

            //    string query = "select * from consignment_view";

            //    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

            //    dataAdapter.Fill(ds.Tables[0]);


            //    query = "select * from Chek_view";

            //    dataAdapter.SelectCommand = new SqlCommand(query, connection);

            //    dataAdapter.Fill(ds.Tables[1]);

            //    query = "select * from Product_view";

            //    dataAdapter.SelectCommand = new SqlCommand(query, connection);

            //    dataAdapter.Fill(ds.Tables[2]);



            //    //// перебор всех таблиц
            //    //foreach (DataTable dt in ds.Tables)
            //    //{
            //    //    Console.WriteLine(dt.TableName); // название таблицы
            //    //                                     // перебор всех столбцов
            //    //    foreach (DataColumn column in dt.Columns)
            //    //        Console.Write("\t{0}", column.ColumnName);
            //    //    Console.WriteLine();
            //    //    // перебор всех строк таблицы
            //    //    foreach (DataRow row in dt.Rows)
            //    //    {
            //    //        // получаем все ячейки строки
            //    //        var cells = row.ItemArray;
            //    //        foreach (object cell in cells)
            //    //            Console.Write("\t{0}", cell);
            //    //        Console.WriteLine();
            //    //    }
            //    //}

            //}

            helpProvider1.HelpNamespace = "../../help/help.html";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistrationForm rf = new RegistrationForm(this);
            this.Hide();
            rf.ShowDialog();


            if (user == "user")
            {
                editToolStripMenuItem.Visible = false;
                editToolStripMenuItem1.Visible = false;
                editToolStripMenuItem2.Visible = false;
                editToolStripMenuItem3.Visible = false;
                editToolStripMenuItem4.Visible = false;
                editToolStripMenuItem5.Visible = false;
                editToolStripMenuItem6.Visible = false;
                editToolStripMenuItem7.Visible = false;
                editToolStripMenuItem8.Visible = false;
                editToolStripMenuItem9.Visible = false;
                editToolStripMenuItem10.Visible = false;
                editToolStripMenuItem11.Visible = false;
                editToolStripMenuItem12.Visible = false;

                chekToolStripMenuItem.Visible = false;
                consignmentToolStripMenuItem.Visible = false;
                chekInfoToolStripMenuItem.Visible = false;
                pasportDataToolStripMenuItem.Visible = false;


                addToolStripMenuItem.Visible = false;
                toolStripMenuItem1.Visible = false;
                toolStripMenuItem2.Visible = false;
                toolStripMenuItem3.Visible = false;
                toolStripMenuItem4.Visible = false;
                toolStripMenuItem5.Visible = false;
                toolStripMenuItem6.Visible = false;
                toolStripMenuItem7.Visible = false;
                toolStripMenuItem8.Visible = false;
                toolStripMenuItem9.Visible = false;
                toolStripMenuItem10.Visible = false;
                toolStripMenuItem11.Visible = false;
                addToolStripMenuItem1.Visible = false;

                reportsToolStripMenuItem.Visible = false;

                rolesToolStripMenuItem.Visible = false;
            }

            if (user == "admin")
            {
                editToolStripMenuItem.Visible = true;
                editToolStripMenuItem1.Visible = true;
                editToolStripMenuItem2.Visible = true;
                editToolStripMenuItem3.Visible = true;
                editToolStripMenuItem4.Visible = true;
                editToolStripMenuItem5.Visible = true;
                editToolStripMenuItem6.Visible = true;
                editToolStripMenuItem7.Visible = true;
                editToolStripMenuItem8.Visible = true;
                editToolStripMenuItem9.Visible = true;
                editToolStripMenuItem10.Visible = true;
                editToolStripMenuItem11.Visible = true;
                editToolStripMenuItem12.Visible = true;

                addToolStripMenuItem.Visible = true;
                toolStripMenuItem1.Visible = true;
                toolStripMenuItem2.Visible = true;
                toolStripMenuItem3.Visible = true;
                toolStripMenuItem4.Visible = true;
                toolStripMenuItem5.Visible = true;
                toolStripMenuItem6.Visible = true;
                toolStripMenuItem7.Visible = true;
                toolStripMenuItem8.Visible = true;
                toolStripMenuItem9.Visible = true;
                toolStripMenuItem10.Visible = true;
                toolStripMenuItem11.Visible = true;
                addToolStripMenuItem1.Visible = true;

                reportsToolStripMenuItem.Visible = true;

                rolesToolStripMenuItem.Visible = true;
            }

            if (user == "seller")
            {
                editToolStripMenuItem.Visible = false;
                editToolStripMenuItem1.Visible = false;
                editToolStripMenuItem2.Visible = false;
                editToolStripMenuItem3.Visible = false;
                editToolStripMenuItem4.Visible = false;
                editToolStripMenuItem5.Visible = false;
                editToolStripMenuItem6.Visible = false;
                editToolStripMenuItem7.Visible = false;
                editToolStripMenuItem8.Visible = false;
                editToolStripMenuItem9.Visible = false;
                editToolStripMenuItem10.Visible = false;
                editToolStripMenuItem11.Visible = false;
                editToolStripMenuItem12.Visible = false;

                addToolStripMenuItem.Visible = false;
                toolStripMenuItem1.Visible = false;
                toolStripMenuItem2.Visible = false;
                toolStripMenuItem3.Visible = false;
                toolStripMenuItem4.Visible = false;
                toolStripMenuItem5.Visible = false;
                toolStripMenuItem6.Visible = false;
                toolStripMenuItem7.Visible = false;
                toolStripMenuItem8.Visible = true;
                toolStripMenuItem9.Visible = false;
                toolStripMenuItem10.Visible = true;
                toolStripMenuItem11.Visible = false;
                addToolStripMenuItem1.Visible = true;

                reportsToolStripMenuItem.Visible = false;

                rolesToolStripMenuItem.Visible = false;
            }

            if (user == "manager")
            {
                editToolStripMenuItem.Visible = false;
                editToolStripMenuItem1.Visible = false;
                editToolStripMenuItem2.Visible = false;
                editToolStripMenuItem3.Visible = false;
                editToolStripMenuItem4.Visible = false;
                editToolStripMenuItem5.Visible = false;
                editToolStripMenuItem6.Visible = false;
                editToolStripMenuItem7.Visible = false;
                editToolStripMenuItem8.Visible = true;
                editToolStripMenuItem9.Visible = false;
                editToolStripMenuItem10.Visible = true;
                editToolStripMenuItem11.Visible = false;
                editToolStripMenuItem12.Visible = true;

                addToolStripMenuItem.Visible = false;
                toolStripMenuItem1.Visible = false;
                toolStripMenuItem2.Visible = false;
                toolStripMenuItem3.Visible = false;
                toolStripMenuItem4.Visible = false;
                toolStripMenuItem5.Visible = false;
                toolStripMenuItem6.Visible = false;
                toolStripMenuItem7.Visible = false;
                toolStripMenuItem8.Visible = true;
                toolStripMenuItem9.Visible = true;
                toolStripMenuItem10.Visible = true;
                toolStripMenuItem11.Visible = true;
                addToolStripMenuItem1.Visible = true;

                reportsToolStripMenuItem.Visible = true;

                rolesToolStripMenuItem.Visible = false;
            }
        }

        public string getStyle()
        {
            return style;
        }

        public void setStyle(string style)
        {
            config.Remove("style");
            config.Add("style", style);

            using (StreamWriter file = new StreamWriter(cfg))
            {
                foreach(string key in config.Keys)
                {
                    config.TryGetValue(key, out string value);
                    file.WriteLine(key+":"+value);
                }
            }

            this.style = style;

            if (style == "white")
            {
                this.BackColor = Color.WhiteSmoke;
                dataGridView1.BackgroundColor= Color.WhiteSmoke;
                menuStrip1.BackColor = Color.Gainsboro;
                panel1.BackColor = Color.LightGray;
                panel2.BackColor = Color.LightGray;
                panel3.BackColor = Color.LightGray;
                tablesToolStripMenuItem.BackColor = Color.LightGray;
                settingsToolStripMenuItem.BackColor = Color.LightGray;
                reportsToolStripMenuItem.BackColor = Color.LightGray;
                rolesToolStripMenuItem.BackColor = Color.LightGray;

                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                label6.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
                checkBox1.ForeColor = Color.Black;

                label8.ForeColor = Color.Black;
                label9.ForeColor = Color.Black;
                label10.ForeColor = Color.Black;
                label11.ForeColor = Color.Black;
                label12.ForeColor = Color.Black;
                label13.ForeColor = Color.Black;
            }
            else
            {
                this.BackColor = Color.FromArgb(35, 35, 35);
                dataGridView1.BackgroundColor = Color.FromArgb(35, 35, 35);
                menuStrip1.BackColor = Color.DimGray;
                panel1.BackColor = Color.Gray;
                panel2.BackColor = Color.Gray;
                panel3.BackColor = Color.Gray;
                tablesToolStripMenuItem.BackColor = Color.Gray;
                settingsToolStripMenuItem.BackColor = Color.Gray;
                reportsToolStripMenuItem.BackColor = Color.Gray;
                rolesToolStripMenuItem.BackColor = Color.Gray;

                label1.ForeColor = Color.WhiteSmoke;
                label2.ForeColor = Color.WhiteSmoke;
                label3.ForeColor = Color.WhiteSmoke;
                label4.ForeColor = Color.WhiteSmoke;
                label5.ForeColor = Color.WhiteSmoke;
                label6.ForeColor = Color.WhiteSmoke;
                label7.ForeColor = Color.WhiteSmoke;
                checkBox1.ForeColor = Color.WhiteSmoke;

                label8.ForeColor = Color.WhiteSmoke;
                label9.ForeColor = Color.WhiteSmoke;
                label10.ForeColor = Color.WhiteSmoke;
                label11.ForeColor = Color.WhiteSmoke;
                label12.ForeColor = Color.WhiteSmoke;
                label13.ForeColor = Color.WhiteSmoke;
            }
        }



        public string getUser()
        {
            return this.user;
        }

        public void setUser(string user)
        {
            this.user = user;
        }

        public string getUserLogin()
        {
            return this.user_login;
        }

        public void setUserLogin(String user_login)
        {
            this.user_login = user_login;
        }



        private void styleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StyleForm style = new StyleForm(this);
            style.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateManufacturer();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            updateSupplier();
        }

        private void openToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            updateMonitor();
        }

        private void openToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            updatePhone();
        }

        private void openToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            updateComputer();
        }

        private void openToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            updatePrinter();
        }
        
        private void openToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            updateProductType();
        }

        private void openToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            updatePayment();            
        }

        private void openToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            updateChek();
        }

        private void openToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            updateConsignment();
        }

        private void openToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            updateChekInfoChek();
        }

        private void openToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            updateProduct();
        }

        private void openToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            updatePasportData();
        }


        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManEditForm add = new ManEditForm(this, "add");
            add.ShowDialog();
            updateManufacturer();
        }
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManEditForm edit = new ManEditForm(this, "edit");
            edit.ShowDialog();
            updateManufacturer();
        }



        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SupEditForm add = new SupEditForm(this, "add");
            add.ShowDialog();
            updateSupplier();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SupEditForm edit = new SupEditForm(this, "edit");
            edit.ShowDialog();
            updateSupplier();
        }



        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MonitorEditForm add = new MonitorEditForm(this, "add");
            add.ShowDialog();
            updateMonitor();
        }

        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            MonitorEditForm edit = new MonitorEditForm(this, "edit");
            edit.ShowDialog();
            updateMonitor();
        }



        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PhoneEditForm add = new PhoneEditForm(this, "add");
            add.ShowDialog();
            updatePhone();
        }

        private void editToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PhoneEditForm edit = new PhoneEditForm(this, "edit");
            edit.ShowDialog();
            updatePhone();
        }



        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ComputerEditForm add = new ComputerEditForm(this, "add");
            add.ShowDialog();
            updateComputer();
        }

        private void editToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ComputerEditForm edit = new ComputerEditForm(this, "edit");
            edit.ShowDialog();
            updateComputer();
        }



        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            PrinterEditForm add = new PrinterEditForm(this, "add");
            add.ShowDialog();
            updatePrinter();
        }

        private void editToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            PrinterEditForm edit = new PrinterEditForm(this, "edit");
            edit.ShowDialog();
            updatePrinter();
        }



        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ProductTypeEditForm add = new ProductTypeEditForm(this, "add");
            add.ShowDialog();
            updateProductType();
        }

        private void editToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ProductTypeEditForm edit = new ProductTypeEditForm(this, "edit");
            edit.ShowDialog();
            updateProductType();
        }



        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            PaymentEditForm add = new PaymentEditForm(this, "add");
            add.ShowDialog();
            updatePayment();
        }

        private void editToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            PaymentEditForm edit = new PaymentEditForm(this, "edit");
            edit.ShowDialog();
            updatePayment();
        }



        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ChekEditForm add = new ChekEditForm(this, "add");
            add.ShowDialog();
            updateChek();
        }

        private void editToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (user == "manager")
            {
                ChekEditForm edit = new ChekEditForm(this, "edit-only");
                edit.ShowDialog();
                updateChek();
            } 
            else
            {
                ChekEditForm edit = new ChekEditForm(this, "edit");
                edit.ShowDialog();
                updateChek();
            }
            
        }



        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ConsignmentEditForm add = new ConsignmentEditForm(this, "add");
            add.ShowDialog();
            updateConsignment();
        }

        private void editToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ConsignmentEditForm edit = new ConsignmentEditForm(this, "edit");
            edit.ShowDialog();
            updateConsignment();
        }



        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            ChekInfoChekEditForm add = new ChekInfoChekEditForm(this, "add");
            add.ShowDialog();
            updateChekInfoChek();
        }

        private void editToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            if (user == "manager")
            {
                ChekInfoChekEditForm edit = new ChekInfoChekEditForm(this, "edit-only");
                edit.ShowDialog();
                updateChekInfoChek();
            }
            else
            {
                ChekInfoChekEditForm edit = new ChekInfoChekEditForm(this, "edit");
                edit.ShowDialog();
                updateChekInfoChek();
            }

            
        }



        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            ProductEditForm add = new ProductEditForm(this, "add");
            add.ShowDialog();
            updateProduct();
        }

        private void editToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            ProductEditForm edit = new ProductEditForm(this, "edit");
            edit.ShowDialog();
            updateProduct();
        }





        private void updateManufacturer()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            table = "manufacturer";
            //search
            label1.Visible= true;
            label2.Visible= false;
            textBox1.Visible= false;
            button1.Visible= false;
            button2.Visible= false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;

            //filter
            label3.Visible=true;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            comboBox2.Items.Clear();
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;

            //calculs
            
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            comboBox6.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            comboBox10.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;

            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from manufacturer";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 4;
                    int i = 0;

                    while (reader.Read())
                    {
                        //MessageBox.Show($"{reader.GetValue(0)}  {reader.GetValue(1)}    {reader.GetValue(2)}    {reader.GetValue(3)}");
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
                    if (user == "user") dataGridView1.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void updateSupplier()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            //searchs
            label1.Visible = true;
            label2.Visible = true;
            label2.Text = "Telephone: ";
            table = "supplier";
            textBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;


            //filter
            label3.Visible = true;
            label4.Visible = true;
            label4.Text = "Prod. type:";
            comboBox2.Visible = true;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("all");
            comboBox2.Items.Add("PC");
            comboBox2.Items.Add("Laptop");
            comboBox2.Items.Add("Monitor");
            comboBox2.Items.Add("Phone");
            comboBox2.Items.Add("Printer");
            comboBox2.Items.Add("Tablet");
            comboBox2.SelectedIndex = 0;
            button3.Visible = true;
            button4.Visible = true;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;


            //calculs


            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            comboBox6.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            comboBox10.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;



            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from supplier";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 6;
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
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Supplier id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Supplier name";
                    dataGridView1.Columns[2].HeaderCell.Value = "Supplier adres";
                    dataGridView1.Columns[3].HeaderCell.Value = "Production type";
                    dataGridView1.Columns[4].HeaderCell.Value = "Telephones";
                    dataGridView1.Columns[5].HeaderCell.Value = "Manufactirer id";
                    if (user == "user")
                    {
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[5].Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void updateMonitor()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            //search
            label1.Visible=true;
            label2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;

            //filter
            label3.Visible = true;
            label4.Visible = true;
            label4.Text = "Application:";
            comboBox2.Visible = true;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("all");
            comboBox2.Items.Add("Gaming");
            comboBox2.Items.Add("Office");
            comboBox2.SelectedIndex = 0;
            button3.Visible = true;
            button4.Visible = true;
            label5.Visible = true;
            label5.Text = "Matrix type:";
            label6.Visible = true;
            label6.Text = "Screen type:";
            label7.Visible = false;
            comboBox3.Visible = true;
            comboBox3.Items.Clear();
            comboBox3.Items.Add("all");
            comboBox3.Items.Add("IPS");
            comboBox3.Items.Add("TN");
            comboBox3.SelectedIndex = 0;
            comboBox4.Visible = true;
            comboBox4.Items.Clear();
            comboBox4.Items.Add("all");
            comboBox4.Items.Add("mate");
            comboBox4.Items.Add("glossy");
            comboBox4.SelectedIndex = 0;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            table = "monitor";


            //calculs
            
            label8.Visible= true;
            label9.Visible= true;
            label10.Visible = false;
            label11.Visible = false;
            comboBox6.Visible = true;
            comboBox6.Items.Clear();
            comboBox6.Items.Add("diagonal");
            comboBox6.SelectedIndex= 0;
            comboBox7.Visible = true;
            comboBox7.Items.Clear();
            comboBox7.Items.Add("diagonal");
            comboBox7.SelectedIndex = 0;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            button5.Visible = true; 
            button6.Visible = true;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = true;
            comboBox10.Visible = false;
            comboBox10.Items.Clear();
            label12.Visible = false;


            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Monitor";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 6;
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
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Matrix type";
                    dataGridView1.Columns[2].HeaderCell.Value = "Diagonal";
                    dataGridView1.Columns[3].HeaderCell.Value = "Monitor type";
                    dataGridView1.Columns[4].HeaderCell.Value = "Screen type";
                    dataGridView1.Columns[5].HeaderCell.Value = "Application";
                    if (user == "user") dataGridView1.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updatePhone()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            //search
            label1.Visible=true;
            label2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;

            //filter
            label3.Visible = true;
            label4.Visible = true;
            label4.Text = "CPU(min):";
            comboBox2.Visible = false;
            comboBox2.Items.Clear();
            button3.Visible = true;
            button4.Visible = true;
            label5.Visible = true;
            label5.Text = "RAM(min):";
            label6.Visible = true;
            label6.Text = "Intern.(min):";
            label7.Visible = true;
            label7.Text = "Application:";
            comboBox3.Visible = false;
            comboBox3.Items.Clear();
            comboBox4.Visible = false;
            comboBox4.Items.Clear();
            comboBox5.Visible = true;
            comboBox5.Items.Clear();
            comboBox5.Items.Add("all");
            comboBox5.Items.Add("Daily");
            comboBox5.Items.Add("Graphic design");
            comboBox5.SelectedIndex = 0;
            checkBox1.Visible = false;
            textBox5.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            table = "phone";

            //calculs
            
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = false;
            label11.Visible = false;
            comboBox6.Visible = true;
            comboBox6.Items.Clear();
            comboBox6.Items.Add("CPU");
            comboBox6.Items.Add("RAM");
            comboBox6.Items.Add("Internal memory");
            comboBox6.SelectedIndex = 0;
            comboBox7.Visible = true;
            comboBox7.Items.Clear();
            comboBox7.Items.Add("CPU");
            comboBox7.Items.Add("RAM");
            comboBox7.Items.Add("Internal memory");
            comboBox7.SelectedIndex = 0;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = true;
            comboBox10.Visible = false;
            comboBox10.Items.Clear();
            label12.Visible = false;


            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Phone";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 5;
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
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                    dataGridView1.Columns[1].HeaderCell.Value = "CPU frequency";
                    dataGridView1.Columns[2].HeaderCell.Value = "RAM";
                    dataGridView1.Columns[3].HeaderCell.Value = "Internal memory";
                    dataGridView1.Columns[4].HeaderCell.Value = "Application";
                    if (user == "user") dataGridView1.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateComputer()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            //search
            label1.Visible=true;
            label2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;

            //filter
            label3.Visible = true;
            label4.Visible = true;
            label4.Text = "CPU(min):";
            comboBox2.Visible = false;
            comboBox2.Items.Clear();
            button3.Visible = true;
            button4.Visible = true;
            label5.Visible = true;
            label5.Text = "RAM(min):";
            label6.Visible = true;
            label6.Text = "HDD(min):";
            label7.Visible = true;
            label7.Text = "Application:";
            comboBox3.Visible = false;
            comboBox3.Items.Clear();
            comboBox4.Visible = false;
            comboBox4.Items.Clear();
            comboBox5.Visible = true;
            comboBox5.Items.Clear();
            comboBox5.Items.Add("all");
            comboBox5.Items.Add("Gaming");
            comboBox5.Items.Add("Office");
            comboBox5.Items.Add("Home");
            comboBox5.Items.Add("Business");
            comboBox5.SelectedIndex = 0;
            checkBox1.Visible = true;
            checkBox1.Text = "CD included: ";
            textBox5.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            table = "computer";

            //calculs
            
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = false;
            label11.Visible = false;
            comboBox6.Visible = true;
            comboBox6.Items.Clear();
            comboBox6.Items.Add("CPU");
            comboBox6.Items.Add("RAM");
            comboBox6.Items.Add("HDD");
            comboBox6.SelectedIndex = 0;
            comboBox7.Visible = true;
            comboBox7.Items.Clear();
            comboBox7.Items.Add("CPU");
            comboBox7.Items.Add("RAM");
            comboBox7.Items.Add("HDD");
            comboBox7.SelectedIndex = 0;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = true;
            comboBox10.Visible = false;
            comboBox10.Items.Clear();
            label12.Visible = false;


            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Computer";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 6;
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
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                    dataGridView1.Columns[1].HeaderCell.Value = "CPU frequency";
                    dataGridView1.Columns[2].HeaderCell.Value = "RAM";
                    dataGridView1.Columns[3].HeaderCell.Value = "HDD";
                    dataGridView1.Columns[4].HeaderCell.Value = "CD";
                    dataGridView1.Columns[5].HeaderCell.Value = "Application";
                    if (user == "user") dataGridView1.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updatePrinter()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            //search
            label1.Visible=true;
            label2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;

            //filter
            label3.Visible = true;
            label4.Visible = true;
            label4.Text = "Type:";
            comboBox2.Visible = true;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("all");
            comboBox2.Items.Add("laser");
            comboBox2.Items.Add("color");
            comboBox2.SelectedIndex = 0;
            button3.Visible = true;
            button4.Visible = true;
            label5.Visible = true;
            label5.Text = "Application:";
            label6.Visible = false;
            label7.Visible = false;
            comboBox3.Visible = true;
            comboBox3.Items.Clear();
            comboBox3.Items.Add("all");
            comboBox3.Items.Add("Office");
            comboBox3.Items.Add("Photos");
            comboBox3.SelectedIndex = 0;
            comboBox4.Visible = false;
            comboBox4.Items.Clear();
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            table = "printer";


            //calculs

            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            comboBox6.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            comboBox10.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;



            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Printer";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

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
                    if (user == "user") dataGridView1.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateProductType()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            table = "product_type";
            //search
            label1.Visible=true;
            label2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;

            //filter
            label3.Visible=true;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            comboBox2.Items.Clear();
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;


            //calculs

            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            comboBox6.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            comboBox10.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;



            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Product_type";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 2;
                    int i = 0;

                    while (reader.Read())
                    {
                        //MessageBox.Show($"{reader.GetValue(0)}  {reader.GetValue(1)}    {reader.GetValue(2)}    {reader.GetValue(3)}");
                        dataGridView1.RowCount++;
                        dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                        dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Type id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Production type";
                    if (user == "user") dataGridView1.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updatePayment()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            table = "payment";
            //search
            label1.Visible=true;
            label2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;

            //filter
            label3.Visible=true;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            comboBox2.Items.Clear();
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;


            //calculs

            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            comboBox6.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            comboBox10.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;



            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Payment";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 2;
                    int i = 0;

                    while (reader.Read())
                    {
                        //MessageBox.Show($"{reader.GetValue(0)}  {reader.GetValue(1)}    {reader.GetValue(2)}    {reader.GetValue(3)}");
                        dataGridView1.RowCount++;
                        dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                        dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Payment code";
                    dataGridView1.Columns[1].HeaderCell.Value = "Payment type";
                    if (user == "user") dataGridView1.Columns[0].Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateChek()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            table = "chek";
            //search
            label1.Visible=true;
            label2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;

            //filter
            label3.Visible=true;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            comboBox2.Items.Clear();
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;

            //calculs


            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            comboBox6.Visible = false;
            comboBox7.Visible = false;
            comboBox8.Visible = false;
            comboBox9.Visible = false;
            comboBox10.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;




            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Chek";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

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
                    dataGridView1.Columns[0].HeaderCell.Value = "Check id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Client id";
                    dataGridView1.Columns[2].HeaderCell.Value = "Payment code";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateConsignment()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            //search
            label1.Visible = true;
            label2.Visible = false;
            //label2.Text = "Consign.№:";
            table = "consignment";
            textBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            comboBox1.Visible = true;
            comboBox1.SelectedIndex = 0;
            dateTimePicker1.Visible = false;

            //filter
            label3.Visible = true;
            label4.Visible = true;
            label4.Text = "Prod. type:";
            comboBox2.Visible = true;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("all");
            comboBox2.Items.Add("PC");
            comboBox2.Items.Add("Laptop");
            comboBox2.Items.Add("Monitor");
            comboBox2.Items.Add("Phone");
            comboBox2.Items.Add("Tablet PC");
            comboBox2.Items.Add("Printer");
            comboBox2.SelectedIndex = 0;
            button3.Visible = true;
            button4.Visible = true;
            label5.Visible = true;
            label5.Text = "Min Prod.cnt:";
            label6.Visible = false;
            label7.Visible = false;
            comboBox3.Visible = false;
            comboBox3.Items.Clear();
            comboBox4.Visible = false;
            comboBox4.Items.Clear();
            comboBox5.Visible = false;
            comboBox5.Items.Clear();
            checkBox1.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = true;
            textBox3.Text = "1";
            textBox4.Visible = false;
            table = "consignment";

            //calculs
            
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            comboBox6.Visible = true;
            comboBox6.Items.Clear();
            comboBox6.Items.Add("Price");
            comboBox6.SelectedIndex = 0;
            comboBox7.Visible = true;
            comboBox7.Items.Clear();
            comboBox7.Items.Add("Price");
            comboBox7.SelectedIndex = 0;
            comboBox8.Visible = true;
            comboBox8.Items.Clear();
            comboBox8.Items.Add("Price");
            comboBox8.SelectedIndex = 0;
            comboBox9.Visible = true;
            comboBox9.Items.Clear();
            comboBox9.Items.Add("outcome");
            comboBox9.SelectedIndex = 0;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            comboBox10.Visible = false;
            comboBox10.Items.Clear();
            label12.Visible = false;


            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from consignment";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

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

        private void updateChekInfoChek()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            //search
            label1.Visible = true;
            label2.Visible = true;
            label2.Text = "Purc. date:";
            table = "chekInfo";
            button1.Visible = true;
            button2.Visible = true;
            comboBox1.Visible = false;
            comboBox1.SelectedIndex = 0;
            textBox1.Visible = false;
            dateTimePicker1.Visible = true;

            //filter
            label3.Visible=true;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            comboBox2.Items.Clear();
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            comboBox5.Visible = false;
            checkBox1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;


            //calculs
            
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            comboBox6.Visible = true;
            comboBox6.Items.Clear();
            comboBox6.Items.Add("Price");
            comboBox6.SelectedIndex = 0;
            comboBox7.Visible = true;
            comboBox7.Items.Clear();
            comboBox7.Items.Add("Price");
            comboBox7.SelectedIndex = 0;
            comboBox8.Visible = true;
            comboBox8.Items.Clear();
            comboBox8.Items.Add("Price");
            comboBox8.SelectedIndex = 0;
            comboBox9.Visible = true;
            comboBox9.Items.Clear();
            comboBox9.Items.Add("income");
            comboBox9.SelectedIndex = 0;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            comboBox10.Visible = false;
            comboBox10.Items.Clear();
            label12.Visible = false;


            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Chek_infoChek";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 4;
                    int i = 0;

                    while (reader.Read())
                    {
                        //MessageBox.Show($"{reader.GetValue(0)}  {reader.GetValue(1)}    {reader.GetValue(2)}    {reader.GetValue(3)}");
                        dataGridView1.RowCount++;
                        dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                        dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                        dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                        dataGridView1.Rows[i].Cells[3].Value = reader.GetValue(3);
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Check id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Product id";
                    dataGridView1.Columns[2].HeaderCell.Value = "Purchasing date";
                    dataGridView1.Columns[3].HeaderCell.Value = "Total price";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void updateProduct()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = true;
            //search
            label1.Visible = true;
            label2.Visible = true;
            label2.Text = "Product name:";
            textBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;
            table = "product";

            //filter
            label3.Visible = true;
            label4.Visible = true;
            label4.Text = "Prod. type:";
            comboBox2.Visible = true;
            comboBox2.Items.Clear();
            comboBox2.Items.Add("all");
            comboBox2.Items.Add("PC");
            comboBox2.Items.Add("Laptop");
            comboBox2.Items.Add("Monitor");
            comboBox2.Items.Add("Phone");
            comboBox2.Items.Add("Tablet PC");
            comboBox2.Items.Add("Printer");
            comboBox2.SelectedIndex = 0;
            button3.Visible = true;
            button4.Visible = true;
            label5.Visible = true;
            label5.Text = "Min price:";
            label6.Visible = false;
            label7.Visible = false;
            comboBox3.Visible = false;
            comboBox3.Items.Clear();
            comboBox4.Visible = false;
            comboBox4.Items.Clear();
            comboBox5.Visible = false;
            comboBox5.Items.Clear();
            checkBox1.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = true;
            textBox3.Text = "100";
            textBox4.Visible = false;

            //calculs
            
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = false;
            label11.Visible = false;
            comboBox6.Visible = true;
            comboBox6.Items.Clear();
            comboBox6.Items.Add("Price");
            comboBox6.SelectedIndex = 0;
            comboBox7.Visible = true;
            comboBox7.Items.Clear();
            comboBox7.Items.Add("Price");
            comboBox7.SelectedIndex = 0;
            comboBox8.Visible = false;
            comboBox8.Items.Clear();
            comboBox9.Visible = false;
            comboBox9.Items.Clear();
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = true;
            comboBox10.Visible = true;
            comboBox10.Items.Clear();
            comboBox10.Items.Add("all");
            comboBox10.Items.Add("PC");
            comboBox10.Items.Add("Laptop");
            comboBox10.Items.Add("Monitor");
            comboBox10.Items.Add("Phone");
            comboBox10.Items.Add("Tablet PC");
            comboBox10.Items.Add("Printer");
            comboBox10.SelectedIndex = 0;
            label12.Visible = true;


            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Product";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

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
                        pictureBox1.BackgroundImage = Image.FromFile(reader.GetValue(6).ToString());
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                    dataGridView1.Columns[1].HeaderCell.Value = "Production type id";
                    dataGridView1.Columns[2].HeaderCell.Value = "Manufacturer id";
                    dataGridView1.Columns[3].HeaderCell.Value = "Product name";
                    dataGridView1.Columns[4].HeaderCell.Value = "Product price";
                    dataGridView1.Columns[5].HeaderCell.Value = "Waranty";
                    dataGridView1.Columns[6].HeaderCell.Value = "Picture";
                    if (user == "user")
                    {
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.Columns[1].Visible = false;
                        dataGridView1.Columns[2].Visible = false;
                        dataGridView1.Columns[6].Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void updatePasportData()
        {
            panel1.Visible = true;
            panel2.Visible = true;
            panel3.Visible = true;
            label13.Visible = false;
            pictureBox1.Visible = false;
            //search
            label1.Visible = true;
            label2.Visible = true;
            label2.Text = "     IDNP:";
            textBox1.Visible = true;
            button1.Visible = true;
            button2.Visible = true;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = false;
            table = "pasport";

            //filter
            label3.Visible=true;
            label4.Visible = false;
            label4.Text = "";
            comboBox2.Visible = false;
            comboBox2.Items.Clear();
            button3.Visible = false;
            button4.Visible = false;
            label5.Visible = false;
            label5.Text = "";
            label6.Visible = false;
            label7.Visible = false;
            comboBox3.Visible = false;
            comboBox3.Items.Clear();
            comboBox4.Visible = false;
            comboBox4.Items.Clear();
            comboBox5.Visible = false;
            comboBox5.Items.Clear();
            checkBox1.Visible = false;
            textBox5.Visible = false;
            textBox3.Visible = false;
            textBox3.Text = "";
            textBox4.Visible = false;

            //calculs
            
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            comboBox6.Visible = false;
            comboBox6.Items.Clear();
            comboBox7.Visible = false;
            comboBox7.Items.Clear();
            comboBox8.Visible = false;
            comboBox8.Items.Clear();
            comboBox9.Visible = false;
            comboBox9.Items.Clear();
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = false;
            button9.Visible = false;
            comboBox10.Visible = false;
            comboBox10.Items.Clear();
            label12.Visible = false;


            dataGridView1.Visible = true;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();


            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();

                    string query = "select * from Pasport_data";

                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.ColumnCount = 6;
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
                        dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                        i++;
                    }
                    dataGridView1.Columns[0].HeaderCell.Value = "Client id";
                    dataGridView1.Columns[1].HeaderCell.Value = "IDNP";
                    dataGridView1.Columns[2].HeaderCell.Value = "Client adres";
                    dataGridView1.Columns[3].HeaderCell.Value = "Client surname";
                    dataGridView1.Columns[4].HeaderCell.Value = "Client name";
                    dataGridView1.Columns[5].HeaderCell.Value = "Client father name";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void manufacturerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateManufacturer();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void suplToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateSupplier();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void monitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateMonitor();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void phoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updatePhone();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void computerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateComputer();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void printerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updatePrinter();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void productTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateProductType();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void printerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            updatePayment();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void chekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateChek();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void consignmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateConsignment();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void chekInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateChekInfoChek();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void aaaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateProduct();
            tablesToolStripMenuItem.HideDropDown();
        }

        private void pasportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updatePasportData();
        }





        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(table == "supplier")
            {
                if(!Regex.IsMatch(textBox1.Text + e.KeyChar, "^\\+(\\d+)?$") && (int)e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            } else if(table == "consignment")
            {
                if (!Regex.IsMatch(textBox1.Text + e.KeyChar, "^\\d+$") && (int)e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
        }





        private void button1_Click(object sender, EventArgs e)
        {
            error.Clear();
            bool flag = true;
            if(table == "supplier" && !Regex.IsMatch(textBox1.Text,"^\\+373\\d\\d\\d\\d\\d\\d\\d\\d$"))
            {
                flag = false;
                error.SetError(textBox1, "Number should be in forat +373xxxxxxxx");
            }

            try { 
                if (table == "supplier" && flag)
                {
                    string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
                    SqlConnection connection = new SqlConnection(connectionString);
                    using (connection)
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand("select * from supplier where Telephone = @Telephone", connection);

                        command.Parameters.Add("@Telephone", textBox1.Text);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            dataGridView1.Rows.Clear();
                            int i = 0;

                            while (reader.Read())
                            {
                                dataGridView1.RowCount++;
                                dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                                dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                                dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                                dataGridView1.Rows[i].Cells[3].Value = reader.GetValue(3);
                                dataGridView1.Rows[i].Cells[4].Value = reader.GetValue(4);
                                dataGridView1.Rows[i].Cells[5].Value = reader.GetValue(5);
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                        }
                    }   
                }



                if (table == "consignment" && flag)
                {
                    string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
                    SqlConnection connection = new SqlConnection(connectionString);
                    if (comboBox1.Text == "Date")
                    {
                        //MessageBox.Show(dateTimePicker1.Text + " == " + dateTimePicker1.Value.ToString());
                        using (connection)
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand("select * from consignment where Consignment_date = @Consignment_date", connection);

                            command.Parameters.Add("@Consignment_date", dateTimePicker1.Value);

                            SqlDataReader reader = command.ExecuteReader();

                            //MessageBox.Show(reader.HasRows.ToString());

                            if (reader.HasRows)
                            {
                                dataGridView1.Rows.Clear();
                                int i = 0;

                                while (reader.Read())
                                {
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
                            }
                        }
                    }
                    else
                    {
                        using (connection)
                        {
                            connection.Open();

                            SqlCommand command = new SqlCommand("select * from consignment where consignment_number = @consignment_number", connection);

                            command.Parameters.Add("@consignment_number", textBox1.Text);

                            SqlDataReader reader = command.ExecuteReader();

                            if (reader.HasRows)
                            {
                                dataGridView1.Rows.Clear();
                                int i = 0;

                                while (reader.Read())
                                {
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
                            }
                        }
                    }
                }

                if(table == "chekInfo" && flag)
                {
                    string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
                    SqlConnection connection = new SqlConnection(connectionString);
                    using (connection)
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand("select * from Chek_infoChek where Purc_date = @Purc_date", connection);

                        command.Parameters.Add("@Purc_date", dateTimePicker1.Value);

                        SqlDataReader reader = command.ExecuteReader();

                        //MessageBox.Show(reader.HasRows.ToString());

                        if (reader.HasRows)
                        {
                            dataGridView1.Rows.Clear();
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
                        }
                    }
                }


                if (table == "product" && flag)
                {
                    string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
                    SqlConnection connection = new SqlConnection(connectionString);
                    using (connection)
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand("select * from Product where Prod_name like @Prod_name", connection);

                        command.Parameters.Add("@Prod_name", "%" + textBox1.Text + "%");

                        SqlDataReader reader = command.ExecuteReader();

                        //MessageBox.Show(reader.HasRows.ToString());

                        if (reader.HasRows)
                        {
                            dataGridView1.Rows.Clear();
                            int i = 0;

                            while (reader.Read())
                            {
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
                            dataGridView1.Columns[6].Visible = false;
                        }
                    }
                }
                if (table == "pasport" && flag)
                {
                    string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
                    SqlConnection connection = new SqlConnection(connectionString);
                    using (connection)
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand("select * from Pasport_data where IDNP like @IDNP", connection);

                        command.Parameters.Add("@IDNP", "%" + textBox1.Text + "%");

                        SqlDataReader reader = command.ExecuteReader();

                        //MessageBox.Show(reader.HasRows.ToString());

                        if (reader.HasRows)
                        {
                            dataGridView1.Rows.Clear();
                            int i = 0;

                            while (reader.Read())
                            {
                                dataGridView1.RowCount++;
                                dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                                dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                                dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                                dataGridView1.Rows[i].Cells[3].Value = reader.GetValue(3);
                                dataGridView1.Rows[i].Cells[4].Value = reader.GetValue(4);
                                dataGridView1.Rows[i].Cells[5].Value = reader.GetValue(5);
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(table == "supplier")
            {
                updateSupplier();
            }
            if(table == "consignment")
            {
                updateConsignment();
            }
            if(table == "chekInfo")
            {
                updateChekInfoChek();
            }
            if(table == "product")
            {
                updateProduct();
            }
            if(table == "pasport")
            {
                updatePasportData();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Date")
            {
                dateTimePicker1.Visible = true;
                textBox1.Visible = false;
            }
            else
            {
                dateTimePicker1.Visible = false;
                textBox1.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(table == "supplier")
            {
                updateSupplier();
            }
            if (table == "monitor")
            {
                updateMonitor();
            }
            if (table == "phone")
            {
                updatePhone();
            }
            if (table == "computer")
            {
                updateComputer();
            }
            if (table == "printer")
            {
                updatePrinter();
            }
            if (table == "consignment")
            {
                updateConsignment();
            }
            if (table == "chekInfo")
            {
                updateChekInfoChek();
            }
            if (table == "product")
            {
                updateProduct();
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);

            try { 
                if(table == "supplier")
                {
                    using (connection)
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("select * from supplier where production_type like @production_type", connection))
                        {
                            command.Parameters.Add("@production_type", comboBox2.Text == "all" ? "%": "%"+comboBox2.Text + "%");

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
                                int i = 0;

                                while (reader.Read())
                                {
                                    dataGridView1.RowCount++;
                                    dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                                    dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                                    dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                                    dataGridView1.Rows[i].Cells[3].Value = reader.GetValue(3);
                                    dataGridView1.Rows[i].Cells[4].Value = reader.GetValue(4);
                                    dataGridView1.Rows[i].Cells[5].Value = reader.GetValue(5);
                                    dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                    i++;
                                }
                                dataGridView1.Columns[0].HeaderCell.Value = "Supplier id";
                                dataGridView1.Columns[1].HeaderCell.Value = "Supplier name";
                                dataGridView1.Columns[2].HeaderCell.Value = "Supplier adres";
                                dataGridView1.Columns[3].HeaderCell.Value = "Production type";
                                dataGridView1.Columns[4].HeaderCell.Value = "Telephones";
                                dataGridView1.Columns[5].HeaderCell.Value = "Manufactirer id";
                        }
                    }    
                }

                if(table == "monitor")
                {
                    using (connection)
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("select * from Monitor where Tatrix_type like @Tatrix_type and Matrix_size like @Matrix_size and Applic like @Applic", connection))
                        {
                            command.Parameters.Add("@Applic", comboBox2.Text == "all" ? "%" : "%" + comboBox2.Text + "%");
                            command.Parameters.Add("@Tatrix_type", comboBox3.Text == "all" ? "%" : "%" + comboBox3.Text + "%");
                            command.Parameters.Add("@Matrix_size", comboBox4.Text == "all" ? "%" : "%" + comboBox4.Text + "%");

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "Matrix type";
                            dataGridView1.Columns[2].HeaderCell.Value = "Diagonal";
                            dataGridView1.Columns[3].HeaderCell.Value = "Monitor type";
                            dataGridView1.Columns[4].HeaderCell.Value = "Screen type";
                            dataGridView1.Columns[5].HeaderCell.Value = "Application";
                        }
                    }
                }

                if (table == "phone")
                {
                    using (connection)
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("select * from Phone where CPU_freq >= @CPU_freq and RAM >= @RAM and Intern_mem >= @Intern_mem and Applic like @Applic", connection))
                        {
                            command.Parameters.Add("@CPU_freq", textBox5.Text);
                            command.Parameters.Add("@RAM", textBox3.Text);
                            command.Parameters.Add("@Intern_mem", Convert.ToInt32(textBox4.Text));
                            command.Parameters.Add("@Applic", comboBox5.Text == "all" ? "%" : comboBox5.Text);

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "CPU frequency";
                            dataGridView1.Columns[2].HeaderCell.Value = "RAM";
                            dataGridView1.Columns[3].HeaderCell.Value = "Internal memory";
                            dataGridView1.Columns[4].HeaderCell.Value = "Application";
                        }
                    }
                }

                if (table == "computer")
                {
                    using (connection)
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("select * from Computer where CPU_freq >= @CPU_freq and RAM >= @RAM and HDD >= @HDD and CD = @CD and Applic like @Applic", connection))
                        {
                            command.Parameters.Add("@CPU_freq", textBox5.Text);
                            command.Parameters.Add("@RAM", textBox3.Text);
                            command.Parameters.Add("@HDD", Convert.ToInt32(textBox4.Text));
                            command.Parameters.Add("@CD", checkBox1.Checked);
                            command.Parameters.Add("@Applic", comboBox5.Text == "all" ? "%" : comboBox5.Text);

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "CPU frequency";
                            dataGridView1.Columns[2].HeaderCell.Value = "RAM";
                            dataGridView1.Columns[3].HeaderCell.Value = "HDD";
                            dataGridView1.Columns[4].HeaderCell.Value = "CD";
                            dataGridView1.Columns[5].HeaderCell.Value = "Application";
                        }
                    }
                }

                if (table == "printer")
                {
                    using (connection)
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("select * from Printer where Printer_type like @Printer_type and Applic like @Applic", connection))
                        {
                            command.Parameters.Add("@Printer_type", comboBox2.Text == "all" ? "%" : "%" + comboBox2.Text + "%");
                            command.Parameters.Add("@Applic", comboBox3.Text == "all" ? "%" : "%" + comboBox3.Text + "%");

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                }

                if (table == "consignment")
                {
                    using (connection)
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("select * from consignment where cod_prod in (select cod_prod from Product where Cod_type in (select Cod_type from Product_type where Product_type like @Product_type)) and prod_number >= @prod_number", connection))
                        {
                            //MessageBox.Show((comboBox2.Text == "all" ? "%" : comboBox2.Text) + textBox3.Text);
                            command.Parameters.Add("@Product_type", comboBox2.Text == "all" ? "%" : comboBox2.Text);
                            command.Parameters.Add("@prod_number", Convert.ToInt32(textBox3.Text));

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                }

                if (table == "product")
                {
                    using (connection)
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("select * from Product where Cod_type in (select Cod_type from Product_type where Product_type like @Product_type) and Price >= @Price", connection))
                        {
                            //MessageBox.Show((comboBox2.Text == "all" ? "%" : comboBox2.Text) + textBox3.Text);
                            command.Parameters.Add("@Product_type", comboBox2.Text == "all" ? "%" : comboBox2.Text);
                            command.Parameters.Add("@Price", Convert.ToInt32(textBox3.Text));

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                            dataGridView1.Columns[6].Visible = false;
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "Production type id";
                            dataGridView1.Columns[2].HeaderCell.Value = "Manufacturer id";
                            dataGridView1.Columns[3].HeaderCell.Value = "Product name";
                            dataGridView1.Columns[4].HeaderCell.Value = "Product price";
                            dataGridView1.Columns[5].HeaderCell.Value = "Waranty";
                        }
                    }
                }

                




            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(textBox5.Text + e.KeyChar, "^\\d+$") && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(textBox3.Text + e.KeyChar, "^\\d+$") && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Regex.IsMatch(textBox4.Text + e.KeyChar, "^\\d+$") && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }


        private void button9_Click(object sender, EventArgs e)
        {
            if (table == "monitor")
            {
                updateMonitor();
            }
            if (table == "phone")
            {
                updatePhone();
            }
            if (table == "computer")
            {
                updateComputer();
            }
            if (table == "consignment")
            {
                updateConsignment();
            }
            if (table == "chekInfo")
            {
                updateChekInfoChek();
            }
            if (table == "product")
            {
                updateProduct();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    if (table == "monitor")
                    {
                        using (SqlCommand command = new SqlCommand("select * from Monitor where Diagonal = (select max(Diagonal) from Monitor)", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "Matrix type";
                            dataGridView1.Columns[2].HeaderCell.Value = "Diagonal";
                            dataGridView1.Columns[3].HeaderCell.Value = "Monitor type";
                            dataGridView1.Columns[4].HeaderCell.Value = "Screen type";
                            dataGridView1.Columns[5].HeaderCell.Value = "Application";
                        }
                    }

                    if (table == "phone")
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            if (comboBox6.Text == "CPU")
                                command.CommandText = "select * from Phone where CPU_freq = (select max(CPU_freq) from Phone)";
                            else if(comboBox6.Text == "RAM")
                            {
                                command.CommandText = $"select * from Phone where RAM = (select max({comboBox6.Text}) from Phone)";
                            } else
                                command.CommandText = $"select * from Phone where Intern_mem = (select max(Intern_mem) from Phone)";

                            command.Connection = connection;

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "CPU frequency";
                            dataGridView1.Columns[2].HeaderCell.Value = "RAM";
                            dataGridView1.Columns[3].HeaderCell.Value = "Internal memory";
                            dataGridView1.Columns[4].HeaderCell.Value = "Application";
                        }
                    }

                    if (table == "computer")
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            if (comboBox6.Text == "CPU")
                                command.CommandText = "select * from Computer where CPU_freq = (select max(CPU_freq) from Computer)";
                            else
                                command.CommandText = $"select * from Computer where {comboBox6.Text} = (select max({comboBox6.Text}) from Computer)";


                            command.Connection = connection;

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "CPU frequency";
                            dataGridView1.Columns[2].HeaderCell.Value = "RAM";
                            dataGridView1.Columns[3].HeaderCell.Value = "HDD";
                            dataGridView1.Columns[4].HeaderCell.Value = "CD";
                            dataGridView1.Columns[5].HeaderCell.Value = "Application";
                        }
                    }

                    if (table == "consignment")
                    {
                        using (SqlCommand command = new SqlCommand("select * from consignment where Price = (select max(Price) from consignment)", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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

                    if (table == "chekInfo")
                    {
                        using (SqlCommand command = new SqlCommand("select * from Chek_infoChek where Gen_price = (select max(Gen_price) from Chek_infoChek)", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
                            int i = 0;

                            while (reader.Read())
                            {
                                //MessageBox.Show($"{reader.GetValue(0)}  {reader.GetValue(1)}    {reader.GetValue(2)}    {reader.GetValue(3)}");
                                dataGridView1.RowCount++;
                                dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                                dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                                dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                                dataGridView1.Rows[i].Cells[3].Value = reader.GetValue(3);
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Check id";
                            dataGridView1.Columns[1].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[2].HeaderCell.Value = "Purchasing date";
                            dataGridView1.Columns[3].HeaderCell.Value = "Total price";
                        }
                    }


                    if (table == "product")
                    {
                        using (SqlCommand command = new SqlCommand("select * from Product where Price = (select max(Price) from Product where Cod_type in (select Cod_type from Product_type where Product_type like @Product_type)) and cod_prod in (select cod_prod from Product where Cod_type in (select Cod_type from Product_type where Product_type like @Product_type))", connection))
                        {
                            command.Parameters.Add("@Product_type", comboBox10.Text == "all"?"%":comboBox10.Text);

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "Production type id";
                            dataGridView1.Columns[2].HeaderCell.Value = "Manufacturer id";
                            dataGridView1.Columns[3].HeaderCell.Value = "Product name";
                            dataGridView1.Columns[4].HeaderCell.Value = "Product price";
                            dataGridView1.Columns[5].HeaderCell.Value = "Waranty";
                        }
                    }





                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    if (table == "monitor")
                    {
                        using (SqlCommand command = new SqlCommand("select * from Monitor where Diagonal = (select min(Diagonal) from Monitor)", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "Matrix type";
                            dataGridView1.Columns[2].HeaderCell.Value = "Diagonal";
                            dataGridView1.Columns[3].HeaderCell.Value = "Monitor type";
                            dataGridView1.Columns[4].HeaderCell.Value = "Screen type";
                            dataGridView1.Columns[5].HeaderCell.Value = "Application";
                        }
                    }

                    if (table == "phone")
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            if (comboBox7.Text == "CPU")
                                command.CommandText = "select * from Phone where CPU_freq = (select min(CPU_freq) from Phone)";
                            else if (comboBox7.Text == "RAM")
                            {
                                command.CommandText = $"select * from Phone where RAM = (select min({comboBox7.Text}) from Phone)";
                            }
                            else
                                command.CommandText = $"select * from Phone where Intern_mem = (select min(Intern_mem) from Phone)";

                            command.Connection = connection;

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "CPU frequency";
                            dataGridView1.Columns[2].HeaderCell.Value = "RAM";
                            dataGridView1.Columns[3].HeaderCell.Value = "Internal memory";
                            dataGridView1.Columns[4].HeaderCell.Value = "Application";
                        }
                    }

                    if (table == "computer")
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            if (comboBox7.Text == "CPU")
                                command.CommandText = "select * from Computer where CPU_freq = (select min(CPU_freq) from Computer)";
                            else
                                command.CommandText = $"select * from Computer where {comboBox7.Text} = (select min({comboBox7.Text}) from Computer)";


                            command.Connection = connection;

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "CPU frequency";
                            dataGridView1.Columns[2].HeaderCell.Value = "RAM";
                            dataGridView1.Columns[3].HeaderCell.Value = "HDD";
                            dataGridView1.Columns[4].HeaderCell.Value = "CD";
                            dataGridView1.Columns[5].HeaderCell.Value = "Application";
                        }
                    }

                    if (table == "consignment")
                    {
                        using (SqlCommand command = new SqlCommand("select * from consignment where Price = (select min(Price) from consignment)", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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

                    if (table == "chekInfo")
                    {
                        using (SqlCommand command = new SqlCommand("select * from Chek_infoChek where Gen_price = (select min(Gen_price) from Chek_infoChek)", connection))
                        {
                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
                            int i = 0;

                            while (reader.Read())
                            {
                                //MessageBox.Show($"{reader.GetValue(0)}  {reader.GetValue(1)}    {reader.GetValue(2)}    {reader.GetValue(3)}");
                                dataGridView1.RowCount++;
                                dataGridView1.Rows[i].Cells[0].Value = reader.GetValue(0);
                                dataGridView1.Rows[i].Cells[1].Value = reader.GetValue(1);
                                dataGridView1.Rows[i].Cells[2].Value = reader.GetValue(2);
                                dataGridView1.Rows[i].Cells[3].Value = reader.GetValue(3);
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Check id";
                            dataGridView1.Columns[1].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[2].HeaderCell.Value = "Purchasing date";
                            dataGridView1.Columns[3].HeaderCell.Value = "Total price";
                        }
                    }

                    if (table == "product")
                    {
                        using (SqlCommand command = new SqlCommand("select * from Product where Price = (select min(Price) from Product where Cod_type in (select Cod_type from Product_type where Product_type like @Product_type)) and cod_prod in (select cod_prod from Product where Cod_type in (select Cod_type from Product_type where Product_type like @Product_type))", connection))
                        {
                            command.Parameters.Add("@Product_type", comboBox10.Text == "all" ? "%" : comboBox10.Text);

                            SqlDataReader reader = command.ExecuteReader();

                            dataGridView1.Rows.Clear();
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
                                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                                i++;
                            }
                            dataGridView1.Columns[0].HeaderCell.Value = "Product id";
                            dataGridView1.Columns[1].HeaderCell.Value = "Production type id";
                            dataGridView1.Columns[2].HeaderCell.Value = "Manufacturer id";
                            dataGridView1.Columns[3].HeaderCell.Value = "Product name";
                            dataGridView1.Columns[4].HeaderCell.Value = "Product price";
                            dataGridView1.Columns[5].HeaderCell.Value = "Waranty";
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    if (table == "consignment")
                    {
                        using (SqlCommand command = new SqlCommand("select AVG(Price) from consignment", connection))
                        {
                            MessageBox.Show($"Consignments' Average Price = {command.ExecuteScalar()}MDL", "Average price", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }

                    if (table == "chekInfo")
                    {
                        using (SqlCommand command = new SqlCommand("select AVG(Gen_price) from Chek_infoChek", connection))
                        {
                            MessageBox.Show($"Average buying price = {command.ExecuteScalar()}MDL", "Average buying price", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    if (table == "consignment")
                    {
                        using (SqlCommand command = new SqlCommand("select Sum(Price) from consignment", connection))
                        {
                            MessageBox.Show($"Total consignments' outcome = {command.ExecuteScalar()}MDL", "Total outcome", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (table == "chekInfo")
                    {
                        using (SqlCommand command = new SqlCommand("select Sum(Gen_price) from Chek_infoChek", connection))
                        {
                            MessageBox.Show($"Total income = {command.ExecuteScalar()}MDL", "Total income", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void consignmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConsignmentReportForm consignmentRF = new ConsignmentReportForm(ds);
            consignmentRF.Show();
        }

        private void checksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckReportForm crf = new CheckReportForm();
            crf.Show();
        }

        private void informationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ProductReportForm prf = new ProductReportForm();
            prf.Show();
        }

        private void diagramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductDiagramReportForm pdrf = new ProductDiagramReportForm();
            pdrf.Show();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PasportDataEditForm pasportData = new PasportDataEditForm(this, "add");
            pasportData.ShowDialog();
            updatePasportData();
        }

        private void editToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            if (user == "manager")
            {
                PasportDataEditForm pasportData = new PasportDataEditForm(this, "edit-only");
                pasportData.ShowDialog();
                updatePasportData();
            }
            else
            {
                PasportDataEditForm pasportData = new PasportDataEditForm(this, "edit");
                pasportData.ShowDialog();
                updatePasportData();
            }
        }

        private void addNewAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RolesEditForm roles = new RolesEditForm(this);
            roles.ShowDialog();
        }

        private void backupDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            String backupName = now.Year.ToString() + "." + now.Month.ToString() + "." + now.Day.ToString() + "_Backup.bak";
            saveFileDialog1.FileName= backupName;
            saveFileDialog1.DefaultExt = ".bak";

            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(Regex.IsMatch(saveFileDialog1.FileName, ".+\\.bak")){

                    //MessageBox.Show(saveFileDialog1.FileName);


                    try
                    {
                        using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                        {
                            connection.Open();

                            String query = "backup database Computer_magazine to disk = @Path";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.Add("@Path", saveFileDialog1.FileName);

                            command.ExecuteNonQuery();
                        }
                        
                        MessageBox.Show("Successfully saved!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect backup extension!", "Invalid extension", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void restoreDataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if(Regex.IsMatch(openFileDialog1.FileName, ".+\\.bak"))
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True"))
                        {
                            connection.Open();

                            String query = "use master";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.ExecuteNonQuery();

                            command.CommandText = "restore database Computer_magazine from disk = @Path";
                            command.Parameters.Add("@Path", openFileDialog1.FileName);
                            command.ExecuteNonQuery();

                            command.CommandText = "use Computer_magazine";
                            command.ExecuteNonQuery();

                            MessageBox.Show("Successfully restored!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    } catch(Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect backup extension!", "Invalid extension", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (table == "product")
            {
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                pictureBox1.BackgroundImage = Image.FromFile(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
            }
        }
    }
}



//help viewer
//CHM file