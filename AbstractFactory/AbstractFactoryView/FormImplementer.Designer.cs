
namespace AbstractFactoryView
{
    partial class FormImplementer
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
            this.textBoxPauseTime = new System.Windows.Forms.TextBox();
            this.textBoxWorkingTime = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelPause = new System.Windows.Forms.Label();
            this.labelWorkingTime = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(210, 150);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(123, 150);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxPauseTime
            // 
            this.textBoxPauseTime.Location = new System.Drawing.Point(123, 112);
            this.textBoxPauseTime.Name = "textBoxPauseTime";
            this.textBoxPauseTime.Size = new System.Drawing.Size(162, 20);
            this.textBoxPauseTime.TabIndex = 13;
            // 
            // textBoxWorkingTime
            // 
            this.textBoxWorkingTime.Location = new System.Drawing.Point(123, 78);
            this.textBoxWorkingTime.Name = "textBoxWorkingTime";
            this.textBoxWorkingTime.Size = new System.Drawing.Size(162, 20);
            this.textBoxWorkingTime.TabIndex = 12;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(123, 41);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(162, 20);
            this.textBoxName.TabIndex = 11;
            // 
            // labelPause
            // 
            this.labelPause.AutoSize = true;
            this.labelPause.Location = new System.Drawing.Point(15, 115);
            this.labelPause.Name = "labelPause";
            this.labelPause.Size = new System.Drawing.Size(102, 13);
            this.labelPause.TabIndex = 10;
            this.labelPause.Text = "Время на перерыв";
            // 
            // labelWorkingTime
            // 
            this.labelWorkingTime.AutoSize = true;
            this.labelWorkingTime.Location = new System.Drawing.Point(15, 78);
            this.labelWorkingTime.Name = "labelWorkingTime";
            this.labelWorkingTime.Size = new System.Drawing.Size(88, 13);
            this.labelWorkingTime.TabIndex = 9;
            this.labelWorkingTime.Text = "Время на заказ";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(15, 44);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(34, 13);
            this.labelName.TabIndex = 8;
            this.labelName.Text = "ФИО";
            // 
            // FormImplementer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 220);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxPauseTime);
            this.Controls.Add(this.textBoxWorkingTime);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelPause);
            this.Controls.Add(this.labelWorkingTime);
            this.Controls.Add(this.labelName);
            this.Name = "FormImplementer";
            this.Text = "Создать исполнителя";
            this.Load += new System.EventHandler(this.FormImplementer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxPauseTime;
        private System.Windows.Forms.TextBox textBoxWorkingTime;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelPause;
        private System.Windows.Forms.Label labelWorkingTime;
        private System.Windows.Forms.Label labelName;
    }
}