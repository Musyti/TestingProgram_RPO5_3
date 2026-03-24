using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Forms;
using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class AdminForm : Form
    {
        private DataService _dataService;
        private FlowLayoutPanel panelCategories;
        private Button btnCreateTest;
        private Button btnLogout;
        private Label lblTitle;

        public AdminForm(DataService dataService)
        {
            _dataService = dataService;
            InitializeComponent();
            LoadCategories();
        }

        private void InitializeComponent()
        {
            panelCategories = new FlowLayoutPanel();
            btnCreateTest = new Button();
            btnLogout = new Button();
            lblTitle = new Label();
            SuspendLayout();
            // 
            // panelCategories
            // 
            panelCategories.AutoScroll = true;
            panelCategories.FlowDirection = FlowDirection.TopDown;
            panelCategories.Location = new Point(10, 60);
            panelCategories.Name = "panelCategories";
            panelCategories.Size = new Size(680, 450);
            panelCategories.TabIndex = 2;
            panelCategories.WrapContents = false;
            // 
            // btnCreateTest
            // 
            btnCreateTest.BackColor = Color.LightGreen;
            btnCreateTest.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            btnCreateTest.Location = new Point(10, 520);
            btnCreateTest.Name = "btnCreateTest";
            btnCreateTest.Size = new Size(680, 50);
            btnCreateTest.TabIndex = 3;
            btnCreateTest.Text = "+ Создать тест";
            btnCreateTest.UseVisualStyleBackColor = false;
            btnCreateTest.Click += BtnCreateTest_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(10, 10);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(120, 30);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Выйти из аккаунта";
            btnLogout.Click += BtnLogout_Click;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(136, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(450, 35);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Панель администратора";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AdminForm
            // 
            ClientSize = new Size(700, 600);
            Controls.Add(btnLogout);
            Controls.Add(lblTitle);
            Controls.Add(panelCategories);
            Controls.Add(btnCreateTest);
            Name = "AdminForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Администратор";
            ResumeLayout(false);
        }

        private void LoadCategories()
        {
            panelCategories.Controls.Clear();
            var categories = _dataService.GetAllCategories();

            if (categories.Count == 0)
            {
                var lblEmpty = new Label
                {
                    Text = "Увы, тестов еще нет",
                    Font = new Font("Microsoft Sans Serif", 14F),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(660, 100)
                };
                panelCategories.Controls.Add(lblEmpty);
                return;
            }

            foreach (var category in categories)
            {
                var panel = new Panel
                {
                    Size = new Size(660, 80),
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
                    Size = new Size(400, 25),
                    ForeColor = Color.Gray
                };

                var btnEdit = new Button
                {
                    Text = "Редактировать",
                    Location = new Point(520, 15),
                    Size = new Size(100, 25),
                    Tag = category
                };
                btnEdit.Click += BtnEdit_Click;

                var btnDelete = new Button
                {
                    Text = "Удалить",
                    Location = new Point(520, 45),
                    Size = new Size(100, 25),
                    Tag = category,
                    BackColor = Color.LightCoral
                };
                btnDelete.Click += BtnDelete_Click;

                panel.Controls.AddRange(new Control[] { lblName, lblDesc, btnEdit, btnDelete });
                panelCategories.Controls.Add(panel);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button?.Tag as Category;
            if (category != null)
            {
                var editor = new TestEditorForm(_dataService, category);
                if (editor.ShowDialog() == DialogResult.OK)
                {
                    LoadCategories(); // Обновляем список
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var category = button?.Tag as Category;
            if (category != null)
            {
                var result = MessageBox.Show($"Удалить тест \"{category.Name}\"?",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _dataService.DeleteCategory(category.Id);
                    LoadCategories();
                }
            }
        }

        private void BtnCreateTest_Click(object sender, EventArgs e)
        {
            var editor = new TestEditorForm(_dataService);
            if (editor.ShowDialog() == DialogResult.OK)
            {
                LoadCategories();
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}