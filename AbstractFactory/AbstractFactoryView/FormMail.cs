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
				var list = _mailLogic.Read(null);
				if (list != null)
				{
					dataGridView.DataSource = list;
					dataGridView.Columns[0].Visible = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

	}
}
