using Microsoft.Reporting.WinForms;
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
    public partial class ConsignmentReportForm : Form
    {
        DataSet ds;
        public ConsignmentReportForm(DataSet ds)
        {
            InitializeComponent();
            this.ds = ds;
        }

        private void ConsignmentReportForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'computer_magazineDataSet.consignment_view' table. You can move, or remove it, as needed.
            this.consignment_viewTableAdapter.Fill(this.computer_magazineDataSet.consignment_view);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "computer_magazineDataSet.consignment_view". При необходимости она может быть перемещена или удалена.
            this.consignment_viewTableAdapter.Fill(this.computer_magazineDataSet.consignment_view);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "computer_magazineDataSet.consignment_view". При необходимости она может быть перемещена или удалена.
            this.consignment_viewTableAdapter.Fill(this.computer_magazineDataSet.consignment_view);

            this.reportViewer1.RefreshReport();

            //ReportDataSource reportDataSource = new ReportDataSource("consignmentDataSource", ds.Tables[0]);

            //reportViewer1.LocalReport.DataSources.Clear();
            //reportViewer1.LocalReport.DataSources.Add(reportDataSource);
        }
    }
}
