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
    public partial class CheckReportForm : Form
    {
        public CheckReportForm()
        {
            InitializeComponent();
        }

        private void CheckReportForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'computer_magazineDataSet.Chek_view' table. You can move, or remove it, as needed.
            this.chek_viewTableAdapter1.Fill(this.computer_magazineDataSet.Chek_view);
            //// TODO: данная строка кода позволяет загрузить данные в таблицу "computer_magazineDataSet.Chek_view". При необходимости она может быть перемещена или удалена.
            //this.chek_viewTableAdapter.Fill(this.computer_magazineDataSet.Chek_view);

            this.reportViewer1.RefreshReport();
        }
    }
}
