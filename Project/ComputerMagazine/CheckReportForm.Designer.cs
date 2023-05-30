namespace Computer_magazine
{
    partial class CheckReportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckReportForm));
            this.chekviewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.computer_magazineDataSet = new Computer_magazine.Computer_magazineDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.chekviewBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.chek_viewTableAdapter1 = new Computer_magazine.Computer_magazineDataSetTableAdapters.Chek_viewTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.chekviewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer_magazineDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chekviewBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // chekviewBindingSource
            // 
            this.chekviewBindingSource.DataMember = "Chek_view";
            this.chekviewBindingSource.DataSource = this.computer_magazineDataSet;
            // 
            // computer_magazineDataSet
            // 
            this.computer_magazineDataSet.DataSetName = "Computer_magazineDataSet";
            this.computer_magazineDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "CheckDataSet";
            reportDataSource1.Value = this.chekviewBindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Computer_magazine.CheckReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // chekviewBindingSource1
            // 
            this.chekviewBindingSource1.DataMember = "Chek_view";
            this.chekviewBindingSource1.DataSource = this.computer_magazineDataSet;
            // 
            // chek_viewTableAdapter1
            // 
            this.chek_viewTableAdapter1.ClearBeforeFill = true;
            // 
            // CheckReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CheckReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Check Report";
            this.Load += new System.EventHandler(this.CheckReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chekviewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer_magazineDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chekviewBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Computer_magazineDataSet computer_magazineDataSet;
        private System.Windows.Forms.BindingSource chekviewBindingSource;
        private Computer_magazineDataSetTableAdapters.Chek_viewTableAdapter chek_viewTableAdapter;
        private System.Windows.Forms.BindingSource chekviewBindingSource1;
        private Computer_magazineDataSetTableAdapters.Chek_viewTableAdapter chek_viewTableAdapter1;
    }
}