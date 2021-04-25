using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AbstractAircraftFactoryLogic.BindingModels;
using AbstractAircraftFactoryLogic.BusinessLogics;
using AbstractAircraftFactoryLogic.ViewModels;
using AbstractFactoryFileImplement.Implements;

namespace AbstractFactoryView
{
    public partial class FormStorageFilling : Form
    {
        private StorageLogic _storageLogic;      
        public int ComponentId
        {
            get { return Convert.ToInt32(comboBoxComponent.SelectedValue); }
            set { comboBoxComponent.SelectedValue = value; }
        }

        public int StorageId
        {
            get { return Convert.ToInt32(comboBoxStorage.SelectedValue); }
            set { comboBoxStorage.SelectedValue = value; }
        }

        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }

        public FormStorageFilling(ComponentLogic componentlogic, StorageLogic storageLogic)
        {
            InitializeComponent();
            _storageLogic = storageLogic;
            List<ComponentViewModel> listComponent = componentlogic.Read(null);
            if (listComponent != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = listComponent;
                comboBoxComponent.SelectedItem = null;
            }

            List<StorageViewModel> listStorage = storageLogic.Read(null);
            if (listStorage != null)
            {
                comboBoxStorage.DisplayMember = "StorageName";
                comboBoxStorage.ValueMember = "Id";
                comboBoxStorage.DataSource = listStorage;
                comboBoxStorage.SelectedItem = null;
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStorage.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            _storageLogic.Restocking(new StorageBindingModel
            {
                Id = StorageId
            }, StorageId, ComponentId, Count);

            DialogResult = DialogResult.OK;
            Close();
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
