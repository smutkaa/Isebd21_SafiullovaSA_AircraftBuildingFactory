using System;
using System.Windows.Forms;
using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.BusinessLogics;
using Unity;


namespace AbstractFactoryView
{
	public partial class FormDocument : Form
	{
		[Dependency]
		public new IUnityContainer Container { get; set; }
		private readonly AircraftLogic logic;

		public FormDocument(AircraftLogic logic)
		{
			InitializeComponent();
			this.logic = logic;
		}

		private void LoadData()
		{
			try
			{
				var list = logic.Read(null);
				if (list != null)
				{
					dataGridView.DataSource = list;
					dataGridView.Columns[0].Visible = false;
					dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					dataGridView.Columns[3].Visible = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			var form = Container.Resolve<FormAircraft>();
			if (form.ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				var form = Container.Resolve<FormAircraft>();
				form.Id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				if (form.ShowDialog() == DialogResult.OK)
				{
					LoadData();
				}
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
						logic.Delete(new AircraftBindingModel { Id = id });
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					LoadData();
				}
			}
		}

		private void buttoRef_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void FormDocument_Load(object sender, EventArgs e)
		{
			LoadData();
		}
	}
}
