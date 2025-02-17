using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OOP_LB1
{
    public partial class HRForm : Form
    {
        private List<HRDepartment> departments = new List<HRDepartment>();
        private ComboBox cmbDepartments;
        private TextBox txtCompanyName, txtEmployees, txtHours, txtRate, txtTax, txtAddress, txtContact;
        private TextBox txtUpdateEmployees, txtUpdateHours, txtUpdateRate, txtUpdateTax, txtUpdateAddress, txtUpdateContact;
        private Label lblCompanyName, lblEmployees, lblHours, lblRate, lblTax, lblAddress, lblContact;
        private DataGridView dgvDepartments;

        public HRForm()
        {
            InitializeComponent();
            InitializeFormComponents();
        }

        private void InitializeFormComponents()
        {
            this.Text = "HR Department Management";
            this.Size = new System.Drawing.Size(1000, 850);
            this.BackColor = System.Drawing.Color.LightSkyBlue;

            TabControl tabControl = new TabControl { Dock = DockStyle.Fill, BackColor = System.Drawing.Color.AliceBlue };
            TabPage createPage = new TabPage("Создать") { BackColor = System.Drawing.Color.LightSteelBlue };
            TabPage updatePage = new TabPage("Обновить") { BackColor = System.Drawing.Color.LightSteelBlue };
            TabPage viewPage = new TabPage("Просмотр") { BackColor = System.Drawing.Color.LightSteelBlue };

            createPage.Controls.Add(CreateDepartmentPanel());
            updatePage.Controls.Add(UpdateDepartmentPanel());
            viewPage.Controls.Add(CreateViewPanel());

            tabControl.TabPages.AddRange(new TabPage[] { createPage, updatePage, viewPage });
            this.Controls.Add(tabControl);
        }

        private FlowLayoutPanel CreateDepartmentPanel()
        {
            FlowLayoutPanel panel = CreateBasePanel();
            lblCompanyName = new Label { Width = 250, Text = "Название компании" };
            lblEmployees = new Label{ Width = 250, Text = "Кол-во сотрудников" };
            lblHours = new Label { Width = 250, Text = "Часы в месяц" };
            lblRate = new Label { Width = 250, Text = "Почасовая ставка" };
            lblTax = new Label { Width = 250, Text = "Налог" };
            lblAddress = new Label { Width = 250, Text = "Адрес" };
            lblContact = new Label { Width = 250, Text = "Контакт" };

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

            txtUpdateEmployees = new TextBox { Width = 250, Text = "Кол-во сотрудников" };
            txtUpdateHours = new TextBox { Width = 250, Text = "Часы в месяц" };
            txtUpdateRate = new TextBox { Width = 250, Text = "Почасовая ставка" };
            txtUpdateTax = new TextBox { Width = 250, Text = "Налог" };
            txtUpdateAddress = new TextBox { Width = 250, Text = "Адрес" };
            txtUpdateContact = new TextBox { Width = 250, Text = "Контакт" };

            Button btnUpdate = new Button { Text = "Обновить", Width = 250 };
            btnUpdate.Click += BtnUpdate_Click;

            panel.Controls.AddRange(new Control[] { cmbDepartments, txtUpdateEmployees, txtUpdateHours, txtUpdateRate, txtUpdateTax, txtUpdateAddress, txtUpdateContact, btnUpdate });
            return panel;
        }
        private FlowLayoutPanel CreateBasePanel()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(5),
                WrapContents = false
            };
        }


        private Control CreateViewPanel()
        {
            dgvDepartments = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                BackgroundColor = System.Drawing.Color.LightSkyBlue,
                AllowUserToAddRows = false
            };

            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Название компании", DataPropertyName = "CompanyName", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Кол-во сотрудников", DataPropertyName = "Employees", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Часы в месяц", DataPropertyName = "HoursPerMonth", Width = 100 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Почасовая ставка", DataPropertyName = "HourlyRate", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Налог", DataPropertyName = "TaxRate", Width = 100 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Адрес", DataPropertyName = "Address", Width = 150 });
            dgvDepartments.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Контакт", DataPropertyName = "Contact", Width = 150 });

            dgvDepartments.DataSource = departments;
            return dgvDepartments;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                MessageBox.Show("Введите название компании");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCompanyName.Text) ||
                string.IsNullOrWhiteSpace(txtEmployees.Text) ||
                string.IsNullOrWhiteSpace(txtHours.Text) ||
                string.IsNullOrWhiteSpace(txtRate.Text) ||
                string.IsNullOrWhiteSpace(txtTax.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка уникальности названия компании
            if (departments.Any(d => d.CompanyName.Equals(txtCompanyName.Text, StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Компания с таким названием уже существует!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HRDepartment department = new HRDepartment(
                txtCompanyName.Text,
                int.TryParse(txtEmployees.Text, out int emp) ? emp : 0,
                double.TryParse(txtHours.Text, out double hours) ? hours : 0,
                decimal.TryParse(txtRate.Text, out decimal rate) ? rate : 0,
                double.TryParse(txtTax.Text, out double tax) ? tax : 0,
                txtAddress.Text,
                txtContact.Text
            );

            departments.Add(department);
            MessageBox.Show("Отдел создан!");
            UpdateDepartmentList();
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (cmbDepartments.SelectedItem is HRDepartment selectedDept)
            {
                selectedDept.Employees = int.TryParse(txtUpdateEmployees.Text, out int emp) ? emp : selectedDept.Employees;
                selectedDept.HoursPerMonth = double.TryParse(txtUpdateHours.Text, out double hours) ? hours : selectedDept.HoursPerMonth;
                selectedDept.HourlyRate = decimal.TryParse(txtUpdateRate.Text, out decimal rate) ? rate : selectedDept.HourlyRate;
                selectedDept.TaxRate = double.TryParse(txtUpdateTax.Text, out double tax) ? tax : selectedDept.TaxRate;
                selectedDept.Address = txtUpdateAddress.Text;
                selectedDept.Contact = txtUpdateContact.Text;
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

        private void UpdateDepartmentList()
        {
            cmbDepartments.Items.Clear();
            cmbDepartments.Items.AddRange(departments.ToArray());
            cmbDepartments.DisplayMember = "CompanyName";

            dgvDepartments.DataSource = null;  // Очищаем источник данных
            dgvDepartments.DataSource = departments;  // Устанавливаем заново
            dgvDepartments.Refresh();  
        }
    }
}
