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
        internal HRQueue hrQueue = new HRQueue();
        internal ComboBox cmbDepartments;
        internal TextBox txtCompanyName, txtEmployees, txtHours, txtRate, txtTax, txtAddress, txtContact;
        internal TextBox txtUpdateEmployees, txtUpdateHours, txtUpdateRate, txtUpdateTax, txtUpdateAddress, txtUpdateContact;
        internal DataGridView dgvDepartments;


        public HRForm()
        {
            InitializeComponent();
            InitializeFormComponents();

            // Подписка на события добавления и обновления департамента с выводом сообщений
            hrQueue.ItemAdded += msg => MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            hrQueue.ItemUpdated += msg => MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            Label lblEmployees = new Label { Width = 250, Text = "Кол-во сотрудников" };
            Label lblHours = new Label { Width = 250, Text = "Часы в месяц" };
            Label lblRate = new Label { Width = 250, Text = "Почасовая ставка" };
            Label lblTax = new Label { Width = 250, Text = "Налог" };
            Label lblAddress = new Label { Width = 250, Text = "Адрес" };
            Label lblContact = new Label { Width = 250, Text = "Контакт" };

            txtCompanyName = new TextBox { Width = 250 };
            txtEmployees = new TextBox { Width = 250 };
            txtHours = new TextBox { Width = 250 };
            txtRate = new TextBox { Width = 250 };
            txtTax = new TextBox { Width = 250 };
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
            txtUpdateHours = new TextBox { Width = 250 };
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
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Название компании", DataPropertyName = "CompanyName", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кол-во сотрудников", DataPropertyName = "Employees", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Часы в месяц", DataPropertyName = "HoursPerMonth", Width = 100 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Почасовая ставка", DataPropertyName = "HourlyRate", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Налог", DataPropertyName = "TaxRate", Width = 100 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Адрес", DataPropertyName = "Address", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Контакт", DataPropertyName = "Contact", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Средняя зп сотрудника", DataPropertyName = "GrossSalary", Width = 150 });

            UpdateDataGridView();
            return dgvDepartments;
        }

        internal void BtnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Введите название компании");
                return;
            }

            if (hrQueue.GetDepartments().Any(d => d.CompanyName.Equals(txtCompanyName.Text, StringComparison.OrdinalIgnoreCase)))
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

            var department = new HRDepartment(
                txtCompanyName.Text,
                int.TryParse(txtEmployees.Text, out int emp) ? emp : 0,
                double.TryParse(txtHours.Text, out double hours) ? hours : 0,
                decimal.TryParse(txtRate.Text, out decimal rate) ? rate : 0,
                double.TryParse(txtTax.Text, out double tax) ? tax : 0,
                txtAddress.Text,
                txtContact.Text
            );

            hrQueue.Enqueue(department);
            UpdateDepartmentList();
        }

        internal void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbDepartments.SelectedItem is HRDepartment selectedDept)
            {
                if (!int.TryParse(txtUpdateEmployees.Text, out int updatedEmployees) || updatedEmployees < 0 ||
                    !double.TryParse(txtUpdateHours.Text, out double updatedHours) || updatedHours < 0 ||
                    !decimal.TryParse(txtUpdateRate.Text, out decimal updatedRate) || updatedRate < 0 ||
                    !double.TryParse(txtUpdateTax.Text, out double updatedTax) || updatedTax < 0 ||
                    !Regex.IsMatch(txtUpdateContact.Text, @"^\+?\d{1,4}?[-.\s]?(\(?\d{1,3}?\)?[-.\s]?)?\d{1,3}[-.\s]?\d{1,3}[-.\s]?\d{1,4}$"))
                {
                    MessageBox.Show("Введите корректные данные.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                hrQueue.UpdateDepartment(selectedDept, updatedEmployees, updatedHours, updatedRate, updatedTax, txtUpdateAddress.Text, txtUpdateContact.Text);
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
            foreach (var dept in hrQueue.GetDepartments())
            {
                cmbDepartments.Items.Add(dept);
            }
            cmbDepartments.DisplayMember = "CompanyName";

            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            dgvDepartments.Rows.Clear();
            foreach (var dept in hrQueue.GetDepartments())
            {
                dgvDepartments.Rows.Add(
                    dept.CompanyName,
                    dept.Employees,
                    dept.HoursPerMonth,
                    dept.HourlyRate,
                    dept.TaxRate,
                    dept.Address,
                    dept.Contact,
                    dept.CalculateSalary()
                );
            }
        }
    }
}