using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class TestForm : Form
    {
        private DataService _dataService;
        private User _user;
        private List<Category> _availableCategories;
        private FlowLayoutPanel panelTests;
        private Button btnLogout;
        private Label lblTitle;

        public TestForm(DataService dataService, User user)
        {
            _dataService = dataService;
            _user = user;
            InitializeComponent();
            LoadTests();
        }

        private void InitializeComponent()
        {
            this.panelTests = new FlowLayoutPanel();
            this.btnLogout = new Button();
            this.lblTitle = new Label();
            this.SuspendLayout();

            this.Text = "Доступные тесты";
            this.ClientSize = new Size(700, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblTitle.Text = $"Добро пожаловать, {_user.Username}!";
            lblTitle.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 10);
            lblTitle.Size = new Size(680, 30);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            btnLogout.Text = "Выйти из аккаунта";
            btnLogout.Location = new Point(10, 10);
            btnLogout.Size = new Size(120, 30);
            btnLogout.Click += (s, e) => this.Close();

            panelTests.AutoScroll = true;
            panelTests.Location = new Point(10, 50);
            panelTests.Size = new Size(680, 480);
            panelTests.FlowDirection = FlowDirection.TopDown;
            panelTests.WrapContents = false;

            this.Controls.AddRange(new Control[] { btnLogout, lblTitle, panelTests });
            this.ResumeLayout(false);
        }

        private void LoadTests()
        {
            panelTests.Controls.Clear();
            _availableCategories = _dataService.GetAllCategories();

            if (_availableCategories.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "Тестов пока нет. Зайдите позже.",
                    Font = new Font("Microsoft Sans Serif", 14F),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(660, 100)
                };
                panelTests.Controls.Add(lblEmpty);
                return;
            }

            foreach (var category in _availableCategories)
            {
                var panel = new Panel
                {
                    Size = new Size(660, 100),
                    BorderStyle = BorderStyle.FixedSingle,
                    Margin = new Padding(0, 0, 0, 10)
                };

                var lblName = new Label
                {
                    Text = category.Name,
                    Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                    Location = new Point(10, 10),
                    Size = new Size(400, 30)
                };

                var lblDesc = new Label
                {
                    Text = category.Description,
                    Location = new Point(10, 45),
                    Size = new Size(450, 25),
                    ForeColor = Color.Gray
                };

                var btnStart = new Button
                {
                    Text = "Пройти",
                    Location = new Point(550, 35),
                    Size = new Size(100, 35),
                    BackColor = _user.CompletedTests.Contains(category.Id) ? Color.LightGreen : Color.LightBlue,
                    Tag = category
                };
                btnStart.Click += BtnStart_Click;

                // Если тест уже пройден, показываем метку
                if (_user.CompletedTests.Contains(category.Id))
                {
                    var lblCompleted = new Label
                    {
                        Text = "✓ Пройден",
                        ForeColor = Color.Green,
                        Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold),
                        Location = new Point(550, 75),
                        Size = new Size(100, 20),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    panel.Controls.Add(lblCompleted);
                }

                panel.Controls.AddRange(new Control[] { lblName, lblDesc, btnStart });
                panelTests.Controls.Add(panel);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button?.Tag as Category;
            if (category != null)
            {
                var testRunner = new TestRunnerForm(_dataService, _user, category);
                if (testRunner.ShowDialog() == DialogResult.OK)
                {
                    LoadTests(); // Обновляем список после прохождения
                }
            }
        }
    }
}

    // Форма прохождения теста
  