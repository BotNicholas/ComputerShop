namespace Computer_magazine
{
    partial class ProductDiagramReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductDiagramReportForm));
            this.Product_viewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.computer_magazineDataSet = new Computer_magazine.Computer_magazineDataSet();
            this.computermagazineDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.productviewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.product_viewTableAdapter = new Computer_magazine.Computer_magazineDataSetTableAdapters.Product_viewTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.Product_viewBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer_magazineDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.computermagazineDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productviewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // Product_viewBindingSource
            // 
            this.Product_viewBindingSource.DataMember = "Product_view";
            this.Product_viewBindingSource.DataSource = this.computer_magazineDataSet;
            // 
            // computer_magazineDataSet
            // 
            this.computer_magazineDataSet.DataSetName = "Computer_magazineDataSet";
            this.computer_magazineDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // computermagazineDataSetBindingSource
            // 
            this.computermagazineDataSetBindingSource.DataSource = this.computer_magazineDataSet;
            this.computermagazineDataSetBindingSource.Position = 0;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ProductDiagramDataSet";
            reportDataSource1.Value = this.Product_viewBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Computer_magazine.ProductDiagramReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 450);
            this.reportViewer1.TabIndex = 0;
            // 
            // productviewBindingSource
            // 
            this.productviewBindingSource.DataMember = "Product_view";
            this.productviewBindingSource.DataSource = this.computer_magazineDataSet;
            // 
            // product_viewTableAdapter
            // 
            this.product_viewTableAdapter.ClearBeforeFill = true;
            // 
            // ProductDiagramReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductDiagramReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Diagram Report";
            this.Load += new System.EventHandler(this.ProductDiagramReportForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Product_viewBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.computer_magazineDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.computermagazineDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productviewBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Computer_magazineDataSet computer_magazineDataSet;
        private System.Windows.Forms.BindingSource computermagazineDataSetBindingSource;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Product_viewBindingSource;
        private System.Windows.Forms.BindingSource productviewBindingSource;
        private Computer_magazineDataSetTableAdapters.Product_viewTableAdapter product_viewTableAdapter;
    }
}