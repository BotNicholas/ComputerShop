using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Computer_magazine
{
    public partial class StyleForm : Form
    {
        Form caller;
        public StyleForm(Form caller)
        {
            InitializeComponent();
            this.caller = caller;

            if (((Form1)this.caller).getStyle() == "white")
            {
                checkBox1.Checked = false;
                this.BackColor = Color.WhiteSmoke;
                label1.ForeColor = Color.Black;
                checkBox1.ForeColor = Color.Black;
            }
            else
            {
                checkBox1.Checked = true;
                this.BackColor = Color.FromArgb(35, 35, 35);
                label1.ForeColor = Color.White;
                checkBox1.ForeColor = Color.White;
            };
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.BackColor = Color.FromArgb(35, 35, 35);
                label1.ForeColor = Color.White;
                checkBox1.ForeColor = Color.White;
                ((Form1)this.caller).setStyle("dark");                
            } else
            {
                this.BackColor = Color.WhiteSmoke;
                label1.ForeColor = Color.Black;
                checkBox1.ForeColor = Color.Black;
                ((Form1)this.caller).setStyle("white");
            }
        }
    }
}
