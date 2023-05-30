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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Computer_magazine
{
    public partial class SupEditForm : Form
    {
        private SqlConnection connection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Computer_magazine;Integrated Security=True");
        private ErrorProvider error;
        private Form caller;
        private string mode;
        private int max_id;
        private int old_id;

        public SupEditForm(Form caller, string mode)
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
                checkBox1.ForeColor = Color.White;
                dataGridView1.BackgroundColor = Color.FromArgb(35, 35, 35);
            }

            if (mode == "add")
            {
                this.Width = 288;
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
                SqlCommand command = new SqlCommand("select Man_cod from supplier", connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetValue(0).ToString());
                }

                comboBox1.Sorted= true;
                comboBox1.SelectedIndex= 0;

            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar) != 8 && !Regex.IsMatch(textBox1.Text + e.KeyChar, "^\\d+$"))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                mode = "delete";
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox6.Enabled = false;
                comboBox1.Enabled = false;
                button1.Visible= false;
            }
            else
            {
                mode = "edit";
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox6.Enabled = true;
                comboBox1.Enabled = true;
                button1.Visible= true;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (mode == "edit")
            {
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
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
                            string query = $"delete from supplier where Sup_code = @Sup_code";

                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.Clear();
                            command.Parameters.Add("@Sup_code", dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

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

            if(mode != "delete" && !Regex.IsMatch(textBox6.Text, "^\\+373\\d\\d\\d\\d\\d\\d\\d\\d$")){
                error.SetError(textBox6, "Enter phone in format +373xxxxxxxx");
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
                            //query = $"insert into supplier values({Convert.ToInt32(textBox1.Text)}, '{textBox2.Text}', '{textBox3.Text}', '{textBox4.Text}', '{textBox6.Text}', {Convert.ToInt32(comboBox1.Text)})";
                            query = "insert into supplier values(@Sup_code, @Sup_name, @Sup_Adres, @production_type, @Telephone, @Man_cod)";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@Sup_code", textBox1.Text);
                            command.Parameters.AddWithValue("@Sup_name", textBox2.Text);
                            command.Parameters.AddWithValue("@Sup_Adres", textBox3.Text);
                            command.Parameters.AddWithValue("@production_type", textBox4.Text);
                            command.Parameters.AddWithValue("@Telephone", textBox6.Text);
                            command.Parameters.AddWithValue("@Man_cod", comboBox1.Text);

                            MessageBox.Show($"Success! {command.ExecuteNonQuery()} rows affected!");

                            Close();
                        }
                        else if (mode == "edit")
                        {
                            query = $"update supplier set Sup_code = @Sup_code, Sup_name = @Sup_name, Sup_Adres = @Sup_Adres, production_type = @production_type, Telephone = @Telephone, Man_cod = @Man_cod where Sup_code = {old_id}";

                            SqlCommand command = new SqlCommand(query, connection);

                            command.Parameters.AddWithValue("@Sup_code", textBox1.Text);
                            command.Parameters.AddWithValue("@Sup_name", textBox2.Text);
                            command.Parameters.AddWithValue("@Sup_Adres", textBox3.Text);
                            command.Parameters.AddWithValue("@production_type", textBox4.Text);
                            command.Parameters.AddWithValue("@Telephone", textBox6.Text);
                            command.Parameters.AddWithValue("@Man_cod", comboBox1.Text);

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

                    string query = "select max(Sup_code) from supplier";
                    SqlCommand command = new SqlCommand(query, connection);

                    max_id = (int)command.ExecuteScalar();


                    query = "select * from supplier";
                    command.CommandText = query;

                    SqlDataReader reader = command.ExecuteReader();

                    dataGridView1.Visible = true;
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
