using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OOP_LB1
{
    public partial class HRForm : Form
    {
        internal BindingList<HRDepartment> departments = new BindingList<HRDepartment>();
        internal ComboBox cmbDepartments;
        internal TextBox txtCompanyName, txtEmployees, txtHours, txtRate, txtTax, txtAddress, txtContact;
        internal TextBox txtUpdateEmployees, txtUpdateHours, txtUpdateRate, txtUpdateTax, txtUpdateAddress, txtUpdateContact;
        internal DataGridView dgvDepartments;

        public HRForm()
        {
            InitializeComponent();
            InitializeFormComponents();
        }

        private void InitializeFormComponents()
        {
            string message = "Вариант 7:  Отдел кадров\n" +
                             "23ВП2\n" +
                             "Тареева Мирошниченко";

            MessageBox.Show(message, "Лабораторная работа номер 1", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Text = "HR Department Management";
            this.Size = new System.Drawing.Size(1000, 850);
            this.BackColor = System.Drawing.Color.LightSkyBlue;

            TabControl tabControl = new TabControl { Dock = DockStyle.Fill, BackColor = System.Drawing.Color.AliceBlue };
            TabPage createPage = new TabPage("Создать") { BackColor = System.Drawing.Color.LightSteelBlue };
            TabPage updatePage = new TabPage("Обновить") { BackColor = System.Drawing.Color.LightSteelBlue };
            TabPage viewPage = new TabPage("Просмотр") { BackColor = System.Drawing.Color.LightSteelBlue };

            dgvDepartments = new DataGridView { Dock = DockStyle.Fill };
    cmbDepartments = new ComboBox { Width = 250 };

            createPage.Controls.Add(CreateDepartmentPanel());
            updatePage.Controls.Add(UpdateDepartmentPanel());
            viewPage.Controls.Add(CreateViewPanel());

            tabControl.TabPages.AddRange(new TabPage[] { createPage, updatePage, viewPage });
            this.Controls.Add(tabControl);
        }

        private FlowLayoutPanel CreateDepartmentPanel()
        {
            FlowLayoutPanel panel = CreateBasePanel();
            Label lblCompanyName = new Label { Width = 250, Text = "Название компании" };
            Label lblEmployees = new Label{ Width = 250, Text = "Кол-во сотрудников" };
            Label lblHours = new Label { Width = 250, Text = "Часы в месяц" };
            Label lblRate = new Label { Width = 250, Text = "Почасовая ставка" };
            Label lblTax = new Label { Width = 250, Text = "Налог" };
            Label lblAddress = new Label { Width = 250, Text = "Адрес" };
            Label lblContact = new Label { Width = 250, Text = "Контакт" };

            txtCompanyName = new TextBox { Width = 250};
            txtEmployees = new TextBox { Width = 250 };
            txtHours = new TextBox { Width = 250 };
            txtRate = new TextBox { Width = 250 };
            txtTax = new TextBox { Width = 250};
            txtAddress = new TextBox { Width = 250 };
            txtContact = new TextBox { Width = 250 };

            Button btnCreate = new Button { Text = "Создать", Width = 250 };
            btnCreate.Click += BtnCreate_Click;

            panel.Controls.AddRange(new Control[] { lblCompanyName, txtCompanyName, lblEmployees, txtEmployees, lblHours, txtHours, lblRate, txtRate, lblTax, 
                txtTax, lblAddress, txtAddress, lblContact, txtContact, btnCreate });
            return panel;
        }

        private FlowLayoutPanel UpdateDepartmentPanel()
        {
            FlowLayoutPanel panel = CreateBasePanel();

            cmbDepartments = new ComboBox { Width = 250 };
            cmbDepartments.SelectedIndexChanged += CmbDepartments_SelectedIndexChanged;
            Label lblCompanyName = new Label { Width = 250, Text = "Название компании:" };
            Label lblEmployees = new Label { Width = 250, Text = "Кол-во сотрудников:" };
            Label lblHours = new Label { Width = 250, Text = "Часы в месяц:" };
            Label lblRate = new Label { Width = 250, Text = "Почасовая ставка:" };
            Label lblTax = new Label { Width = 250, Text = "Налог:" };
            Label lblAddress = new Label { Width = 250, Text = "Адрес:" };
            Label lblContact = new Label { Width = 250, Text = "Контакт:" };

            txtUpdateEmployees = new TextBox { Width = 250 };
            txtUpdateHours = new TextBox { Width = 250};
            txtUpdateRate = new TextBox { Width = 250 };
            txtUpdateTax = new TextBox { Width = 250 };
            txtUpdateAddress = new TextBox { Width = 250 };
            txtUpdateContact = new TextBox { Width = 250 };

            Button btnUpdate = new Button { Text = "Обновить", Width = 250 };
            btnUpdate.Click += BtnUpdate_Click;

            panel.Controls.AddRange(new Control[] { lblCompanyName, cmbDepartments, lblEmployees, txtUpdateEmployees, lblHours, txtUpdateHours, 
                lblRate, txtUpdateRate, lblTax, txtUpdateTax, lblAddress, txtUpdateAddress, lblContact, txtUpdateContact, btnUpdate });
            return panel;
        }
        private FlowLayoutPanel CreateBasePanel()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(350, 100, 5, 5),
                WrapContents = false
            };
        }


        private Control CreateViewPanel()
        {
            dgvDepartments.DataSource = new BindingSource { DataSource = departments };

            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Название компании", DataPropertyName = "CompanyName", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кол-во сотрудников", DataPropertyName = "Employees", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Часы в месяц", DataPropertyName = "HoursPerMonth", Width = 100 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Почасовая ставка", DataPropertyName = "HourlyRate", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Налог", DataPropertyName = "TaxRate", Width = 100 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Адрес", DataPropertyName = "Address", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Контакт", DataPropertyName = "Contact", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Средняя зп сотрудника", DataPropertyName = "GrossSalary", Width = 150 });



            dgvDepartments.DataSource = departments;
            return dgvDepartments;
        }

        internal void BtnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Введите название компании");
                return;
            }

            if (departments.Any(d => d.CompanyName.Equals(txtCompanyName.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Компания с таким названием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string contact = txtContact.Text;
            string phonePattern = @"^\+?\d{1,4}?[-.\s]?(\(?\d{1,3}?\)?[-.\s]?)?\d{1,3}[-.\s]?\d{1,3}[-.\s]?\d{1,4}$";
            if (!Regex.IsMatch(contact, phonePattern))
            {
                MessageBox.Show("Введите корректный телефонный номер.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HRDepartment department = null;
            try
            {
                department = new HRDepartment(
                    txtCompanyName.Text,
                    int.TryParse(txtEmployees.Text, out int emp) ? emp : 0,
                    double.TryParse(txtHours.Text, out double hours) ? hours : 0,
                    decimal.TryParse(txtRate.Text, out decimal rate) ? rate : 0,
                    double.TryParse(txtTax.Text, out double tax) ? tax : 0,
                    txtAddress.Text,
                    txtContact.Text
                );

                decimal salary = department.CalculateSalary();  // Это вызовет исключение, если возникнет ошибка
            }
            catch (Exception ex)
            {
                HRDepartment.HandleException(ex);  // Перехватываем исключение и обрабатываем его
                return; 
            }

            departments.Add(department);
            MessageBox.Show($"Отдел создан! ");
            UpdateDepartmentList();
        }

        internal void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbDepartments.SelectedItem is HRDepartment selectedDept)
            {
                // Проверка числовых значений и вывод окна с ошибкой, если введены некорректные или отрицательные данные
                if (!int.TryParse(txtUpdateEmployees.Text, out int updatedEmployees) || updatedEmployees < 0)
                {
                    MessageBox.Show("Введите корректное число для количества сотрудников (не отрицательное).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(txtUpdateHours.Text, out double updatedHours) || updatedHours < 0)
                {
                    MessageBox.Show("Введите корректное число для часов в месяц (не отрицательное).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!decimal.TryParse(txtUpdateRate.Text, out decimal updatedRate) || updatedRate < 0)
                {
                    MessageBox.Show("Введите корректное число для почасовой ставки (не отрицательное).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!double.TryParse(txtUpdateTax.Text, out double updatedTax) || updatedTax < 0)
                {
                    MessageBox.Show("Введите корректное число для налога (не отрицательное).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string contact = txtUpdateContact.Text;
                string phonePattern = @"^\+?\d{1,4}?[-.\s]?(\(?\d{1,3}?\)?[-.\s]?)?\d{1,3}[-.\s]?\d{1,3}[-.\s]?\d{1,4}$";
                if (!Regex.IsMatch(contact, phonePattern))
                {
                    MessageBox.Show("Введите корректный телефонный номер.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Используем метод UpdateFields для обновления данных
                string updatedAddress = txtUpdateAddress.Text;
                string updatedContact = txtUpdateContact.Text;

                selectedDept.UpdateFields(updatedEmployees, updatedHours, updatedRate, updatedTax, updatedAddress, updatedContact);

                MessageBox.Show("Объект обновлен!");
                UpdateDepartmentList();
            }
        }


        private void CmbDepartments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDepartments.SelectedItem is HRDepartment selectedDept)
            {
                txtUpdateEmployees.Text = selectedDept.Employees.ToString();
                txtUpdateHours.Text = selectedDept.HoursPerMonth.ToString();
                txtUpdateRate.Text = selectedDept.HourlyRate.ToString();
                txtUpdateTax.Text = selectedDept.TaxRate.ToString();
                txtUpdateAddress.Text = selectedDept.Address;
                txtUpdateContact.Text = selectedDept.Contact;
            }
        }

        internal void UpdateDepartmentList()
        {
            if (cmbDepartments == null || dgvDepartments == null) return;

            cmbDepartments.Items.Clear();
            cmbDepartments.Items.AddRange(departments.ToArray());
            cmbDepartments.DisplayMember = "CompanyName";

            dgvDepartments.DataSource = null;
            dgvDepartments.DataSource = new BindingSource { DataSource = departments };
            ((BindingSource)dgvDepartments.DataSource).ResetBindings(false);
        }

    }
}
