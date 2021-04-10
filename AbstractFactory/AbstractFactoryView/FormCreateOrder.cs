using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.BusinessLogics;
using AbstractAircraftFactoryLogic.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
using System.Collections.Generic;

namespace AbstractFactoryView
{
	public partial class FormCreateOrder : Form
	{
		[Dependency]
		public new IUnityContainer Container { get; set; }
		private readonly AircraftLogic _logicA;
		private readonly OrderLogic _logicO;
		private readonly ClientLogic _logicC;
		public FormCreateOrder(AircraftLogic logicP, OrderLogic logicO, ClientLogic logicC)
		{
			InitializeComponent();
			_logicA = logicP;
			_logicO = logicO;
			_logicC = logicC;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(textBoxCount.Text))
			{
				MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (comboBoxAircraft.SelectedValue == null)
			{
				MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (comboBoxClient.SelectedValue == null)
			{
				MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK,
				MessageBoxIcon.Error);
				return;
			}
			try
			{
				_logicO.CreateOrder(new CreateOrderBindingModel
				{
					AircraftId = Convert.ToInt32(comboBoxAircraft.SelectedValue),
					ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
					Count = Convert.ToInt32(textBoxCount.Text),
					Sum = Convert.ToDecimal(textBoxSum.Text)
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

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void FormCreateOrder_Load(object sender, EventArgs e)
		{
            try
            {
                List<AircraftViewModel> aircraftlist = _logicA.Read(null);
                if (aircraftlist != null)
                {
                    comboBoxAircraft.DisplayMember = "AircraftName";
                    comboBoxAircraft.ValueMember = "Id";
                    comboBoxAircraft.DataSource = aircraftlist;
                    comboBoxAircraft.SelectedItem = null;
                }
				var listClients = _logicC.Read(null);
				foreach (var component in listClients)
				{
					comboBoxClient.DisplayMember = "ClientName";
					comboBoxClient.ValueMember = "Id";
					comboBoxClient.DataSource = listClients;
					comboBoxClient.SelectedItem = null;
				}
			}

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}
		private void CalcSum()
		{
			if (comboBoxAircraft.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
			{
				try
				{
					int id = Convert.ToInt32(comboBoxAircraft.SelectedValue);
					AircraftViewModel aircraft = _logicA.Read(new AircraftBindingModel { Id = id })?[0];
					int count = Convert.ToInt32(textBoxCount.Text);
					textBoxSum.Text = (count * aircraft?.Price ?? 0).ToString();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

        private void ComboBoxAircraft_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

		private void textBoxCount_TextChanged(object sender, EventArgs e)
		{
			CalcSum();
		}
	}
}
