namespace AbstractFactoryView
{
    partial class FormStorage
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxComponent = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.NameComponent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxResponsiblePerson = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelResponsiblePerson = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.groupBoxComponent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(399, 221);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(298, 221);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // groupBoxComponent
            // 
            this.groupBoxComponent.Controls.Add(this.dataGridView);
            this.groupBoxComponent.Location = new System.Drawing.Point(241, 21);
            this.groupBoxComponent.Name = "groupBoxComponent";
            this.groupBoxComponent.Size = new System.Drawing.Size(263, 194);
            this.groupBoxComponent.TabIndex = 12;
            this.groupBoxComponent.TabStop = false;
            this.groupBoxComponent.Text = "Детали на складе";
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameComponent,
            this.Count});
            this.dataGridView.Location = new System.Drawing.Point(19, 32);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(238, 150);
            this.dataGridView.TabIndex = 0;
            // 
            // NameComponent
            // 
            this.NameComponent.HeaderText = "Название";
            this.NameComponent.Name = "NameComponent";
            // 
            // Count
            // 
            this.Count.HeaderText = "Количество";
            this.Count.Name = "Count";
            // 
            // textBoxResponsiblePerson
            // 
            this.textBoxResponsiblePerson.Location = new System.Drawing.Point(15, 112);
            this.textBoxResponsiblePerson.Name = "textBoxResponsiblePerson";
            this.textBoxResponsiblePerson.Size = new System.Drawing.Size(205, 20);
            this.textBoxResponsiblePerson.TabIndex = 11;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(15, 63);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(205, 20);
            this.textBoxName.TabIndex = 10;
            // 
            // labelResponsiblePerson
            // 
            this.labelResponsiblePerson.AutoSize = true;
            this.labelResponsiblePerson.Location = new System.Drawing.Point(12, 96);
            this.labelResponsiblePerson.Name = "labelResponsiblePerson";
            this.labelResponsiblePerson.Size = new System.Drawing.Size(144, 13);
            this.labelResponsiblePerson.TabIndex = 9;
            this.labelResponsiblePerson.Text = "ФИО ответственного лица";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 37);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(57, 13);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "Название";
            // 
            // FormStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 280);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.groupBoxComponent);
            this.Controls.Add(this.textBoxResponsiblePerson);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelResponsiblePerson);
            this.Controls.Add(this.labelName);
            this.Name = "FormStorage";
            this.Text = "FormStorage";
            this.Load += new System.EventHandler(this.FormStorage_Load);
            this.groupBoxComponent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.GroupBox groupBoxComponent;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameComponent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Count;
        private System.Windows.Forms.TextBox textBoxResponsiblePerson;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelResponsiblePerson;
        private System.Windows.Forms.Label labelName;
    }
}