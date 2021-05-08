
namespace AbstractFactoryView
{
    partial class FormReportAircraftComponents
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Aircraft = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Component = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSaveToExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Aircraft,
            this.Component,
            this.Count});
            this.dataGridView.Location = new System.Drawing.Point(100, 56);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(542, 291);
            this.dataGridView.TabIndex = 3;
            // 
            // Aircraft
            // 
            this.Aircraft.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Aircraft.HeaderText = "Изделие";
            this.Aircraft.Name = "Aircraft";
            // 
            // Component
            // 
            this.Component.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Component.HeaderText = "Компонент";
            this.Component.Name = "Component";
            // 
            // Count
            // 
            this.Count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Count.HeaderText = "Количество";
            this.Count.Name = "Count";
            // 
            // buttonSaveToExcel
            // 
            this.buttonSaveToExcel.Location = new System.Drawing.Point(27, 27);
            this.buttonSaveToExcel.Name = "buttonSaveToExcel";
            this.buttonSaveToExcel.Size = new System.Drawing.Size(159, 23);
            this.buttonSaveToExcel.TabIndex = 2;
            this.buttonSaveToExcel.Text = "Сохранить в Excel";
            this.buttonSaveToExcel.UseVisualStyleBackColor = true;
            this.buttonSaveToExcel.Click += new System.EventHandler(this.buttonSaveToExcel_Click);
            // 
            // FormReportAircraftComponents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 392);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonSaveToExcel);
            this.Name = "FormReportAircraftComponents";
            this.Text = "Отчет по компонентам";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Aircraft;
        private System.Windows.Forms.DataGridViewTextBoxColumn Component;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.Button buttonSaveToExcel;
    }
}