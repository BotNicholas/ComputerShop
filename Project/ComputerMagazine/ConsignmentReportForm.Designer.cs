namespace Computer_magazine
{
    partial class ConsignmentReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsignmentReportForm));
            this.consignmentviewBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.computermagazineDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.computer_magazineDataSet = new Computer_magazine.Computer_magazineDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.consignmentviewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.consignmentviewBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.consignment_viewTableAdapter = new Computer_magazine.Computer_magazineDataSetTableAdapters.consignment_viewTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.consignmentviewBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.computermagazineDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer_magazineDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.consignmentviewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.consignmentviewBindingSource2)).BeginInit();
            this.SuspendLayout();
            // 
            // consignmentviewBindingSource1
            // 
            this.consignmentviewBindingSource1.DataMember = "consignment_view";
            this.consignmentviewBindingSource1.DataSource = this.computermagazineDataSetBindingSource;
            // 
            // computermagazineDataSetBindingSource
            // 
            this.computermagazineDataSetBindingSource.DataSource = this.computer_magazineDataSet;
            this.computermagazineDataSetBindingSource.Position = 0;
            // 
            // computer_magazineDataSet
            // 
            this.computer_magazineDataSet.DataSetName = "Computer_magazineDataSet";
            this.computer_magazineDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ConsignmentDataSet";
            reportDataSource1.Value = this.consignmentviewBindingSource2;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Computer_magazine.ConsignmentReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // consignmentviewBindingSource
            // 
            this.consignmentviewBindingSource.DataMember = "consignment_view";
            this.consignmentviewBindingSource.DataSource = this.computer_magazineDataSet;
            // 
            // consignmentviewBindingSource2
            // 
            this.consignmentviewBindingSource2.DataMember = "consignment_view";
            this.consignmentviewBindingSource2.DataSource = this.computer_magazineDataSet;
            // 
            // consignment_viewTableAdapter
            // 
            this.consignment_viewTableAdapter.ClearBeforeFill = true;
            // 
            // ConsignmentReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConsignmentReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consignment Report";
            this.Load += new System.EventHandler(this.ConsignmentReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.consignmentviewBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.computermagazineDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer_magazineDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.consignmentviewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.consignmentviewBindingSource2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource computermagazineDataSetBindingSource;
        private Computer_magazineDataSet computer_magazineDataSet;
        private System.Windows.Forms.BindingSource consignmentviewBindingSource;
        private System.Windows.Forms.BindingSource consignmentviewBindingSource1;
        private System.Windows.Forms.BindingSource consignmentviewBindingSource2;
        private Computer_magazineDataSetTableAdapters.consignment_viewTableAdapter consignment_viewTableAdapter;
    }
}