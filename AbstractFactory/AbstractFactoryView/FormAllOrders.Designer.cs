
namespace AbstractFactoryView
{
    partial class FormAllOrders
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
            this.ReportAllOrdersViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.buttonInPDF = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ReportAllOrdersViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer
            // 
            reportDataSource1.Name = "DataSetAllOrders";
            reportDataSource1.Value = this.ReportAllOrdersViewModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "AbstractFactoryView.ReportAllOrders.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(12, 53);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(776, 385);
            this.reportViewer.TabIndex = 0;
            // 
            // buttonInPDF
            // 
            this.buttonInPDF.Location = new System.Drawing.Point(412, 13);
            this.buttonInPDF.Name = "buttonInPDF";
            this.buttonInPDF.Size = new System.Drawing.Size(107, 24);
            this.buttonInPDF.TabIndex = 7;
            this.buttonInPDF.Text = "В PDF";
            this.buttonInPDF.UseVisualStyleBackColor = true;
            this.buttonInPDF.Click += new System.EventHandler(this.buttonInPDF_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(293, 12);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(98, 23);
            this.buttonCreate.TabIndex = 8;
            this.buttonCreate.Text = "Сформировать";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // FormAllOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.buttonInPDF);
            this.Controls.Add(this.reportViewer);
            this.Name = "FormAllOrders";
            this.Text = "Заказы";
            ((System.ComponentModel.ISupportInitialize)(this.ReportAllOrdersViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.Button buttonInPDF;
        private System.Windows.Forms.BindingSource ReportAllOrdersViewModelBindingSource;
        private System.Windows.Forms.Button buttonCreate;
    }
}