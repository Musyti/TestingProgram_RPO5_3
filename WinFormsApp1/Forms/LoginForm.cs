using System;
using System.Windows.Forms;
using WinFormsApp1.Forms;
using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class LoginForm : Form
    {
        private DataService _dataService;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private ComboBox cmbRole;
        private Button btnLogin;
        private Button btnRegister;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private Label lblRole;

        public LoginForm()
        {
            InitializeComponent();
            _dataService = new DataService();
        }

        private void InitializeComponent()
        {
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            cmbRole = new ComboBox();
            btnLogin = new Button();
            btnRegister = new Button();
            lblTitle = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            lblRole = new Label();
            SuspendLayout();
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(150, 80);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(200, 23);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(150, 115);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(200, 23);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // cmbRole
            // 
            cmbRole.Items.AddRange(new object[] { "Тестируемый", "Администратор" });
            cmbRole.Location = new Point(150, 150);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(200, 23);
            cmbRole.TabIndex = 6;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(100, 200);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(100, 35);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Войти";
            btnLogin.Click += BtnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(220, 200);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(100, 35);
            btnRegister.TabIndex = 8;
            btnRegister.Text = "Регистрация";
            btnRegister.Click += BtnRegister_Click;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(82, 7);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(300, 70);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Тестирующая программа";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Click += lblTitle_Click;
            // 
            // lblUsername
            // 
            lblUsername.Location = new Point(50, 80);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(100, 25);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Логин:";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(50, 115);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 25);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Пароль:";
            // 
            // lblRole
            // 
            lblRole.Location = new Point(50, 150);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(100, 25);
            lblRole.TabIndex = 5;
            lblRole.Text = "Роль:";
            // 
            // LoginForm
            // 
            ClientSize = new Size(450, 280);
            Controls.Add(lblTitle);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblRole);
            Controls.Add(cmbRole);
            Controls.Add(btnLogin);
            Controls.Add(btnRegister);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Вход в систему";
            ResumeLayout(false);
            PerformLayout();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = _dataService.Login(username, password);
            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (user.Role == UserRole.Admin)
            {
                var adminForm = new AdminForm(_dataService);
                adminForm.ShowDialog();
            }
            else
            {
                var testForm = new TestForm(_dataService, user);
                testForm.ShowDialog();
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text.Trim();
            var password = txtPassword.Text;
            var role = cmbRole.SelectedIndex == 0 ? UserRole.Tested : UserRole.Admin;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_dataService.RegisterUser(username, password, role))
            {
                MessageBox.Show("Регистрация успешна! Теперь войдите.", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUsername.Clear();
                txtPassword.Clear();
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}