using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Forms
{
    public partial class TestEditorForm : Form
    {
        private DataService _dataService;
        private Category _category;
        private bool _isEditMode;

        private TextBox txtName;
        private TextBox txtDescription;
        private TabControl tabControl;
        private Button btnSave;
        private Button btnCancel;
        private Label lblStatus;

        private List<QuestionEditorPanel> _questionPanels;
        private List<ResultEditorPanel> _resultPanels;

        public TestEditorForm(DataService dataService, Category category = null)
        {
            _dataService = dataService;
            _category = category ?? new Category();
            _isEditMode = category != null;
            _questionPanels = new List<QuestionEditorPanel>();
            _resultPanels = new List<ResultEditorPanel>();

            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.txtName = new TextBox();
            this.txtDescription = new TextBox();
            this.tabControl = new TabControl();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.lblStatus = new Label();
            this.SuspendLayout();

            // Настройка формы
            this.Text = _isEditMode ? "Редактирование теста" : "Создание теста";
            this.ClientSize = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Название
            var lblName = new Label { Text = "Название теста:", Location = new Point(10, 15), Size = new Size(100, 25) };
            txtName.Location = new Point(120, 15);
            txtName.Size = new Size(300, 22);
            txtName.Text = _category.Name;

            // Описание
            var lblDesc = new Label { Text = "Описание:", Location = new Point(10, 45), Size = new Size(100, 25) };
            txtDescription.Location = new Point(120, 45);
            txtDescription.Size = new Size(760, 22);
            txtDescription.Text = _category.Description;

            // TabControl
            tabControl.Location = new Point(10, 80);
            tabControl.Size = new Size(870, 520);

            // Кнопки
            btnSave.Text = "Сохранить";
            btnSave.Location = new Point(710, 620);
            btnSave.Size = new Size(100, 35);
            btnSave.BackColor = Color.LightGreen;
            btnSave.Click += BtnSave_Click;

            btnCancel.Text = "Отмена";
            btnCancel.Location = new Point(820, 620);
            btnCancel.Size = new Size(70, 35);
            btnCancel.Click += (s, e) => this.Close();

            lblStatus.Location = new Point(10, 620);
            lblStatus.Size = new Size(690, 35);
            lblStatus.ForeColor = Color.Red;

            this.Controls.AddRange(new Control[] {
                lblName, txtName, lblDesc, txtDescription,
                tabControl, btnSave, btnCancel, lblStatus
            });
            this.ResumeLayout(false);
        }

        private void LoadData()
        {
            // Вкладка с вопросами
            var questionsTab = new TabPage("Вопросы (10)");
            var questionsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            for (int i = 0; i < 10; i++)
            {
                var question = _category.Questions.Count > i ? _category.Questions[i] : new Question();
                var panel = new QuestionEditorPanel(i + 1, question);
                _questionPanels.Add(panel);
                questionsPanel.Controls.Add(panel);
            }
            questionsTab.Controls.Add(questionsPanel);

            // Вкладка с результатами
            var resultsTab = new TabPage("Результаты (3)");
            var resultsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false
            };

            for (int i = 0; i < 3; i++)
            {
                var result = _category.Results.Count > i ? _category.Results[i] : new ResultRange();
                var panel = new ResultEditorPanel(i + 1, result);
                _resultPanels.Add(panel);
                resultsPanel.Controls.Add(panel);
            }
            resultsTab.Controls.Add(resultsPanel);

            tabControl.TabPages.Add(questionsTab);
            tabControl.TabPages.Add(resultsTab);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Проверка названия
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                ShowStatus("Введите название теста!", true);
                return;
            }

            // Сбор вопросов
            var questions = new List<Question>();
            foreach (var panel in _questionPanels)
            {
                var question = panel.GetQuestion();
                if (!question.IsValid())
                {
                    ShowStatus($"Вопрос {panel.Number} заполнен не полностью!", true);
                    tabControl.SelectedIndex = 0;
                    return;
                }
                questions.Add(question);
            }

            // Сбор результатов
            var results = new List<ResultRange>();
            foreach (var panel in _resultPanels)
            {
                var result = panel.GetResult();
                if (!result.IsValid())
                {
                    ShowStatus($"Результат {panel.Number} заполнен не полностью!", true);
                    tabControl.SelectedIndex = 1;
                    return;
                }
                results.Add(result);
            }

            // Проверка диапазонов результатов
            var sortedResults = results.OrderBy(r => r.MinPoints).ToList();
            for (int i = 0; i < sortedResults.Count; i++)
            {
                if (i > 0 && sortedResults[i].MinPoints <= sortedResults[i - 1].MaxPoints)
                {
                    ShowStatus("Диапазоны результатов не должны пересекаться!", true);
                    return;
                }
            }

            // Сохранение
            _category.Name = txtName.Text.Trim();
            _category.Description = txtDescription.Text.Trim();
            _category.Questions = questions;
            _category.Results = results;

            if (_isEditMode)
            {
                _dataService.UpdateCategory(_category);
            }
            else
            {
                _dataService.AddCategory(_category);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ShowStatus(string message, bool isError)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = isError ? Color.Red : Color.Green;

            var timer = new System.Windows.Forms.Timer { Interval = 3000 };
            timer.Tick += (s, e) =>
            {
                lblStatus.Text = "";
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }
    }

    // Панель редактора вопроса
    public class QuestionEditorPanel : Panel
    {
        public int Number { get; private set; }
        private TextBox txtContent;
        private TextBox[] txtOptions;
        private NumericUpDown[] nudPoints;
        private Question _question;

        public QuestionEditorPanel(int number, Question question)
        {
            Number = number;
            _question = question;
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new Size(850, 280);
            this.Margin = new Padding(0, 0, 0, 10);

            var lblNumber = new Label
            {
                Text = $"Вопрос {Number}",
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                Location = new Point(5, 5),
                Size = new Size(100, 25)
            };

            // Текст вопроса
            var lblContent = new Label { Text = "Текст вопроса:", Location = new Point(5, 35), Size = new Size(100, 25) };
            txtContent = new TextBox { Location = new Point(110, 35), Size = new Size(730, 22) };

            // Варианты ответов
            txtOptions = new TextBox[4];
            nudPoints = new NumericUpDown[4];

            for (int i = 0; i < 4; i++)
            {
                int y = 70 + i * 45;

                var lblOption = new Label { Text = $"Вариант {i + 1}:", Location = new Point(5, y), Size = new Size(70, 25) };
                txtOptions[i] = new TextBox { Location = new Point(80, y), Size = new Size(500, 22) };

                var lblPoints = new Label { Text = "Баллы:", Location = new Point(590, y), Size = new Size(45, 25) };
                nudPoints[i] = new NumericUpDown
                {
                    Location = new Point(635, y),
                    Size = new Size(60, 22),
                    Minimum = 0,
                    Maximum = 100
                };

                this.Controls.AddRange(new Control[] { lblOption, txtOptions[i], lblPoints, nudPoints[i] });
            }

            this.Controls.AddRange(new Control[] { lblNumber, lblContent, txtContent });
        }

        private void LoadData()
        {
            txtContent.Text = _question.Content;

            for (int i = 0; i < 4 && i < _question.Options.Count; i++)
            {
                txtOptions[i].Text = _question.Options[i];
            }

            for (int i = 0; i < 4 && i < _question.PointsOptions.Count; i++)
            {
                nudPoints[i].Value = _question.PointsOptions[i];
            }
        }

        public Question GetQuestion()
        {
            var options = new List<string>();
            var points = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                options.Add(txtOptions[i].Text.Trim());
                points.Add((int)nudPoints[i].Value);
            }

            return new Question
            {
                Content = txtContent.Text.Trim(),
                Options = options,
                PointsOptions = points
            };
        }
    }

    // Панель редактора результата
    public class ResultEditorPanel : Panel
    {
        public int Number { get; private set; }
        private NumericUpDown nudMin;
        private NumericUpDown nudMax;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private ResultRange _result;

        public ResultEditorPanel(int number, ResultRange result)
        {
            Number = number;
            _result = result;
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Size = new Size(850, 120);
            this.Margin = new Padding(0, 0, 0, 10);

            var lblNumber = new Label
            {
                Text = $"Результат {Number}",
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                Location = new Point(5, 5),
                Size = new Size(100, 25)
            };

            // Диапазон баллов
            var lblMin = new Label { Text = "Мин. баллов:", Location = new Point(5, 35), Size = new Size(70, 25) };
            nudMin = new NumericUpDown { Location = new Point(80, 35), Size = new Size(60, 22), Minimum = 0, Maximum = 1000 };

            var lblMax = new Label { Text = "Макс. баллов:", Location = new Point(150, 35), Size = new Size(80, 25) };
            nudMax = new NumericUpDown { Location = new Point(235, 35), Size = new Size(60, 22), Minimum = 0, Maximum = 1000 };

            // Название результата
            var lblTitle = new Label { Text = "Название:", Location = new Point(5, 65), Size = new Size(70, 25) };
            txtTitle = new TextBox { Location = new Point(80, 65), Size = new Size(300, 22) };

            // Описание
            var lblDesc = new Label { Text = "Описание:", Location = new Point(390, 65), Size = new Size(70, 25) };
            txtDescription = new TextBox { Location = new Point(465, 65), Size = new Size(370, 22) };

            this.Controls.AddRange(new Control[] {
                lblNumber, lblMin, nudMin, lblMax, nudMax,
                lblTitle, txtTitle, lblDesc, txtDescription
            });
        }

        private void LoadData()
        {
            nudMin.Value = _result.MinPoints;
            nudMax.Value = _result.MaxPoints;
            txtTitle.Text = _result.Title;
            txtDescription.Text = _result.Description;
        }

        public ResultRange GetResult()
        {
            return new ResultRange
            {
                MinPoints = (int)nudMin.Value,
                MaxPoints = (int)nudMax.Value,
                Title = txtTitle.Text.Trim(),
                Description = txtDescription.Text.Trim()
            };
        }
    }
}