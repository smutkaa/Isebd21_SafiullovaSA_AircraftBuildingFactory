using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.BusinessLogics;
using AbstractAircraftFactoryLogic.ViewModels;
using System;
using System.Windows.Forms;
using Unity;
using System.Collections.Generic;

namespace AbstractFactoryView
{
	public partial class FormMain : Form
	{
		[Dependency]
		public new IUnityContainer Container { get; set; }
		private readonly OrderLogic _orderLogic;
		private readonly ReportLogic _report;
		public FormMain(OrderLogic orderLogic, ReportLogic report)
		{
			InitializeComponent();
			this._orderLogic = orderLogic;
			this._report = report;

		}
		private void FormMain_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		public FormMain()
		{
			InitializeComponent();
		}
		private void LoadData()
		{
			try
			{
                List<OrderViewModel> list = _orderLogic.Read(null);
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

		private void buttonCreateOrder_Click(object sender, EventArgs e)
		{
			var form = Container.Resolve<FormCreateOrder>();
			form.ShowDialog();
			LoadData();
		}

		private void buttonPayOrder_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				try
				{
					_orderLogic.PayOrder(new ChangeStatusBindingModel { OrderId = id });
					LoadData();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void buttonOrderReady_Click(object sender, EventArgs e)
		{
			if (dataGridView.SelectedRows.Count == 1)
			{
				int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				try
				{
					_orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = id });
					LoadData();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void buttonTakeOrderInWork_Click(object sender, EventArgs e)
		{
            if (dataGridView.SelectedRows.Count == 1)
			{
				int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
				try
				{
					_orderLogic.TakeOrderInWork(new ChangeStatusBindingModel { OrderId = id });
					LoadData();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void buttonRef_Click(object sender, EventArgs e)
		{
			LoadData();
		}

        private void КомпонентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormComponents>();
            form.ShowDialog();
        }

        private void ИзделияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormAircrafts>();
            form.ShowDialog();
        }

        private void СкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormStorages>();
            form.ShowDialog();
        }

        private void ПополнитьСкладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormStorageFilling>();
            form.ShowDialog();
        }

        private void списокИзделиToolStripMenuItem_Click(object sender, EventArgs e)
        {
			using (var dialog = new SaveFileDialog { Filter = "docx|*.docx" })
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					_report.SaveAircraftsToWordFile(new ReportBindingModel { FileName = dialog.FileName });
					MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

        private void изделияПоКомпонентамToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var form = Container.Resolve<FormReportAircraftComponents>();
			form.ShowDialog();
		}

        private void списокЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
			var form = Container.Resolve<FormClientOrders>();
			form.ShowDialog();
		}
    }
}
