namespace Computer_magazine
{
    partial class ProductReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductReportForm));
            this.productviewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.computer_magazineDataSet = new Computer_magazine.Computer_magazineDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.productviewBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.product_viewTableAdapter1 = new Computer_magazine.Computer_magazineDataSetTableAdapters.Product_viewTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.productviewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer_magazineDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productviewBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // productviewBindingSource
            // 
            this.productviewBindingSource.DataMember = "Product_view";
            this.productviewBindingSource.DataSource = this.computer_magazineDataSet;
            // 
            // computer_magazineDataSet
            // 
            this.computer_magazineDataSet.DataSetName = "Computer_magazineDataSet";
            this.computer_magazineDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ProducsDataSet";
            reportDataSource1.Value = this.productviewBindingSource1;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Computer_magazine.ProductReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // productviewBindingSource1
            // 
            this.productviewBindingSource1.DataMember = "Product_view";
            this.productviewBindingSource1.DataSource = this.computer_magazineDataSet;
            // 
            // product_viewTableAdapter1
            // 
            this.product_viewTableAdapter1.ClearBeforeFill = true;
            // 
            // ProductReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Report";
            this.Load += new System.EventHandler(this.ProductReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.productviewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer_magazineDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productviewBindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Computer_magazineDataSet computer_magazineDataSet;
        private System.Windows.Forms.BindingSource productviewBindingSource;
        private Computer_magazineDataSetTableAdapters.Product_viewTableAdapter product_viewTableAdapter;
        private System.Windows.Forms.BindingSource productviewBindingSource1;
        private Computer_magazineDataSetTableAdapters.Product_viewTableAdapter product_viewTableAdapter1;
    }
}