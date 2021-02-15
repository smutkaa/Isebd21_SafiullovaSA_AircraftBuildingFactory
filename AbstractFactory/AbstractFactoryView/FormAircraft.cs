using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.BusinessLogics;
using AbstractAircraftFactoryLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace AbstractFactoryView
{
	public partial class FormAircraft : Form
	{
		[Dependency]
		public new IUnityContainer Container { get; set; }
		public int Id { set { id = value; } }
		private readonly AircraftLogic logic;
		private int? id;
		private Dictionary<int, (string, int)> aircraftComponents;

		public FormAircraft(AircraftLogic service)
		{
			InitializeComponent();
			this.logic = service;
		}
		private void FormAircraft_Load(object sender, EventArgs e)
		{
			if (id.HasValue)
			{
				try
				{
					AircraftViewModel view = logic.Read(new AircraftBindingModel { Id = id.Value })?[0];
					if (view != null)
					{
						textBoxName.Text = view.AircraftName;
						textBoxPrice.Text = view.Price.ToString();
						aircraftComponents = view.AircraftComponents;
						LoadData();
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				aircraftComponents = new Dictionary<int, (string, int)>();
			}
		}
		private void LoadData()
		{
			try
			{
				if (aircraftComponents != null)
				{
					dataGridView.Rows.Clear();
					foreach (var pc in aircraftComponents)
					{
						dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		private void buttonAdd_Click(object sender, EventArgs e)
		{
			var form = Container.Resolve<FormAircraftComponent>();

			if (form.ShowDialog() == DialogResult.OK)
			{
				if (aircraftComponents.ContainsKey(form.Id))
				{
					aircraftComponents[form.Id] = (form.ComponentName, form.Count);
				}
				else
				{
					aircraftComponents.Add(form.Id, (form.ComponentName, form.Count));
				}
				LoadData();
			}
		}

		private void buttonChange_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				var form = Container.Resolve<FormAircraftComponent>();
				int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				form.Id = id;
				form.Count = aircraftComponents[id].Item2;

				if (form.ShowDialog() == DialogResult.OK)
				{
					aircraftComponents[form.Id] = (form.ComponentName, form.Count);
					LoadData();
				}
			}
		}
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (aircraftComponents == null || aircraftComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new AircraftBindingModel
                {
                    Id = id,
                    AircraftName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    AircraftComponents = aircraftComponents
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        aircraftComponents.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtoRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
