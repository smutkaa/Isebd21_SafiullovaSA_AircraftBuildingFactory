using System;
using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.BusinessLogics;
using System.Windows.Forms;

namespace AbstractFactoryView
{
    public partial class FormMail : Form
    {
		private readonly MailLogic _mailLogic;
		public FormMail(MailLogic logic)
        {
            InitializeComponent();
			this._mailLogic = logic;
		}

        private void FormMail_Load(object sender, EventArgs e)
        {
            LoadData();
        }
		private void LoadData()
		{
			try
			{
				Program.ConfigGrid(_mailLogic.Read(null), dataGridView);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
			   MessageBoxIcon.Error);
			}
		}
	}
}
