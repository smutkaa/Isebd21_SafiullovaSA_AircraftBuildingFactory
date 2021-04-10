using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.BusinessLogics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AbstractFactoryView
{
    public partial class FormClients : Form
    {
		[Dependency]
		public new IUnityContainer Container { get; set; }
		private readonly ClientLogic logicC;
		public FormClients(ClientLogic logic)
        {
            InitializeComponent();
			this.logicC = logic;
		}

        private void buttonAdd_Click(object sender, EventArgs e)
        {
			var form = Container.Resolve<FormClient>();
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}

        private void buttonDelete_Click(object sender, EventArgs e)
        {
			if (dataGridView.SelectedRows.Count == 1)
			{
				if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
					try
					{
						logicC.Delete(new ClientBindingModel { Id = id });
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					LoadData();
				}
			}
		}

        private void buttonChange_Click(object sender, EventArgs e)
        {
			if (dataGridView.SelectedRows.Count == 1)
			{
				var form = Container.Resolve<FormClient>();
				form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				if (form.ShowDialog() == DialogResult.OK)
				{
					LoadData();
				}
			}
		}

        private void buttoRef_Click(object sender, EventArgs e)
        {
			LoadData();
		}
		private void LoadData()
		{
			try
			{
				var list = logicC.Read(null);
				if (list != null)
				{
					dataGridView.DataSource = list;
					dataGridView.Columns[0].Visible = false;
					dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void FormClients_Load(object sender, EventArgs e)
        {
			LoadData();
		}
    }
}
