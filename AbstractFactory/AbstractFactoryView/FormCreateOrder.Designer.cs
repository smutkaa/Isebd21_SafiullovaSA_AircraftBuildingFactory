namespace AbstractFactoryView
{
	partial class FormCreateOrder
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
			this.textBoxCount = new System.Windows.Forms.TextBox();
			this.comboBoxAircraft = new System.Windows.Forms.ComboBox();
			this.labelCount = new System.Windows.Forms.Label();
			this.labelComp = new System.Windows.Forms.Label();
			this.labelSum = new System.Windows.Forms.Label();
			this.textBoxSum = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// buttonCancel
			// 
			this.buttonCancel.Location = new System.Drawing.Point(197, 137);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 11;
			this.buttonCancel.Text = "Отмена";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Location = new System.Drawing.Point(116, 137);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 10;
			this.buttonSave.Text = "Сохранить";
			this.buttonSave.UseVisualStyleBackColor = true;
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// textBoxCount
			// 
			this.textBoxCount.Location = new System.Drawing.Point(116, 69);
			this.textBoxCount.Name = "textBoxCount";
			this.textBoxCount.Size = new System.Drawing.Size(154, 20);
			this.textBoxCount.TabIndex = 9;
			this.textBoxCount.TextChanged += new System.EventHandler(this.textBoxCount_TextChanged);
			// 
			// comboBoxAircraft
			// 
			this.comboBoxAircraft.FormattingEnabled = true;
			this.comboBoxAircraft.Location = new System.Drawing.Point(116, 36);
			this.comboBoxAircraft.Name = "comboBoxAircraft";
			this.comboBoxAircraft.Size = new System.Drawing.Size(154, 21);
			this.comboBoxAircraft.TabIndex = 8;
			this.comboBoxAircraft.SelectedIndexChanged += new System.EventHandler(this.ComboBoxAircraft_SelectedIndexChanged);
			// 
			// labelCount
			// 
			this.labelCount.AutoSize = true;
			this.labelCount.Location = new System.Drawing.Point(47, 69);
			this.labelCount.Name = "labelCount";
			this.labelCount.Size = new System.Drawing.Size(66, 13);
			this.labelCount.TabIndex = 7;
			this.labelCount.Text = "Количество";
			// 
			// labelComp
			// 
			this.labelComp.AutoSize = true;
			this.labelComp.Location = new System.Drawing.Point(47, 39);
			this.labelComp.Name = "labelComp";
			this.labelComp.Size = new System.Drawing.Size(51, 13);
			this.labelComp.TabIndex = 6;
			this.labelComp.Text = "Изделие";
			// 
			// labelSum
			// 
			this.labelSum.AutoSize = true;
			this.labelSum.Location = new System.Drawing.Point(47, 101);
			this.labelSum.Name = "labelSum";
			this.labelSum.Size = new System.Drawing.Size(41, 13);
			this.labelSum.TabIndex = 12;
			this.labelSum.Text = "Сумма";
			// 
			// textBoxSum
			// 
			this.textBoxSum.Location = new System.Drawing.Point(116, 95);
			this.textBoxSum.Name = "textBoxSum";
			this.textBoxSum.Size = new System.Drawing.Size(154, 20);
			this.textBoxSum.TabIndex = 13;
			// 
			// FormCreateOrder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(352, 235);
			this.Controls.Add(this.textBoxSum);
			this.Controls.Add(this.labelSum);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.textBoxCount);
			this.Controls.Add(this.comboBoxAircraft);
			this.Controls.Add(this.labelCount);
			this.Controls.Add(this.labelComp);
			this.Name = "FormCreateOrder";
			this.Text = "FormCreateOrder";
			this.Load += new System.EventHandler(this.FormCreateOrder_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.TextBox textBoxCount;
		private System.Windows.Forms.ComboBox comboBoxAircraft;
		private System.Windows.Forms.Label labelCount;
		private System.Windows.Forms.Label labelComp;
		private System.Windows.Forms.Label labelSum;
		private System.Windows.Forms.TextBox textBoxSum;
	}
}