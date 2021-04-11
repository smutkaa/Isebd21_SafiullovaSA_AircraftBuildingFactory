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
    public partial class FormImplementer : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ImplementersLogic logicI;

        private int? id;
        public FormImplementer(ImplementersLogic logic)
        {
            InitializeComponent();
            this.logicI = logic;
        }
        private void FormImplementer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logicI.Read(new ImplementerBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.ImplementerName;
                        textBoxWorkingTime.Text = Convert.ToString(view.WorkingTime);
                        textBoxPauseTime.Text = Convert.ToString(view.PauseTime);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text) || string.IsNullOrEmpty(textBoxWorkingTime.Text) ||
                string.IsNullOrEmpty(textBoxPauseTime.Text))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                return;
            }
            try
            {
                logicI.CreateOrUpdate(new ImplementerBindingModel
                {
                    Id = id,
                    ImplementerName = textBoxName.Text,
                    WorkingTime = Convert.ToInt32(textBoxWorkingTime.Text),
                    PauseTime = Convert.ToInt32(textBoxPauseTime.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }
        }
    }
}
