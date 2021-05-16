using System;
using System.Windows.Forms;
using Unity;
using AbstractAircraftFactoryLogic.BusinessLogics;
using AbstractAircraftFactoryLogic.BindingModels;

namespace AbstractFactoryView
{
    public partial class FormImplementers : Form
    {
		[Dependency]
		public new IUnityContainer Container { get; set; }
		private readonly ImplementersLogic logicI;
		public FormImplementers(ImplementersLogic logic)
        {
            InitializeComponent();
			this.logicI = logic;
		}

		private void LoadData()
		{
			try
			{
				Program.ConfigGrid(logicI.Read(null), dataGridView);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
			   MessageBoxIcon.Error);
			}
		}
		private void buttonAdd_Click(object sender, EventArgs e)
        {
			var form = Container.Resolve<FormImplementer>();
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
						logicI.Delete(new ImplementerBindingModel { Id = id });
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
				var form = Container.Resolve<FormImplementer>();
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

        private void FormImplementers_Load(object sender, EventArgs e)
        {
			LoadData();
        }
		}
	}
}
