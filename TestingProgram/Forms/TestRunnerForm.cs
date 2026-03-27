using WinFormsApp1.Models;
using WinFormsApp1.Services;

public partial class TestRunnerForm : Form
{
    private DataService _dataService;
    private User _user;
    private Category _category;
    private List<int> _selectedOptions;
    private int _currentQuestionIndex;
    private Label lblQuestionCounter;
    private Label lblQuestionText;
    private RadioButton[] rbOptions;
    private Button btnNext;
    private Button btnExit;
    private Panel optionsPanel;

    public TestRunnerForm(DataService dataService, User user, Category category)
    {
        _dataService = dataService;
        _user = user;
        _category = category;
        _selectedOptions = new List<int>();
        _currentQuestionIndex = 0;

        // Инициализация выбранных ответов (-1 означает не отвечено)
        for (int i = 0; i < _category.Questions.Count; i++)
        {
            _selectedOptions.Add(-1);
        }

        InitializeComponent();
        LoadQuestion(0);
    }

    private void InitializeComponent()
    {
        this.lblQuestionCounter = new Label();
        this.lblQuestionText = new Label();
        this.optionsPanel = new Panel();
        this.btnNext = new Button();
        this.btnExit = new Button();
        this.SuspendLayout();

        // Настройка формы
        this.Text = $"Прохождение теста: {_category.Name}";
        this.ClientSize = new Size(600, 500);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;

        // Счетчик вопросов
        lblQuestionCounter.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
        lblQuestionCounter.Location = new Point(20, 20);
        lblQuestionCounter.Size = new Size(560, 30);
        lblQuestionCounter.TextAlign = ContentAlignment.MiddleCenter;

        // Текст вопроса
        lblQuestionText.Font = new Font("Microsoft Sans Serif", 11F);
        lblQuestionText.Location = new Point(20, 70);
        lblQuestionText.Size = new Size(560, 80);
        lblQuestionText.TextAlign = ContentAlignment.MiddleLeft;

        // Панель для вариантов ответов
        optionsPanel.Location = new Point(20, 160);
        optionsPanel.Size = new Size(560, 200);
        optionsPanel.AutoScroll = true;

        // Создание RadioButton для вариантов
        rbOptions = new RadioButton[4];
        for (int i = 0; i < 4; i++)
        {
            rbOptions[i] = new RadioButton
            {
                Location = new Point(10, 10 + i * 45),
                Size = new Size(540, 40),
                Text = "",
                Tag = i
            };
            rbOptions[i].CheckedChanged += Option_CheckedChanged;
            optionsPanel.Controls.Add(rbOptions[i]);
        }

        // Кнопка "Далее"
        btnNext.Text = "Далее";
        btnNext.Location = new Point(400, 380);
        btnNext.Size = new Size(100, 40);
        btnNext.BackColor = Color.LightBlue;
        btnNext.Click += BtnNext_Click;

        // Кнопка "Выйти"
        btnExit.Text = "Выйти";
        btnExit.Location = new Point(280, 380);
        btnExit.Size = new Size(100, 40);
        btnExit.Click += (s, e) =>
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти? Прогресс не сохранится.",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                this.DialogResult = DialogResult.Cancel;
        };

        this.Controls.AddRange(new Control[] {
                lblQuestionCounter, lblQuestionText, optionsPanel, btnNext, btnExit
            });
        this.ResumeLayout(false);
    }

    private void LoadQuestion(int index)
    {
        if (index >= _category.Questions.Count)
        {
            FinishTest();
            return;
        }

        _currentQuestionIndex = index;
        var question = _category.Questions[index];

        // Обновляем счетчик
        lblQuestionCounter.Text = $"Вопрос {index + 1} из {_category.Questions.Count}";

        // Текст вопроса
        lblQuestionText.Text = question.Content;

        // Загрузка вариантов ответов
        for (int i = 0; i < 4 && i < question.Options.Count; i++)
        {
            rbOptions[i].Text = $"{i + 1}. {question.Options[i]}";
            rbOptions[i].Checked = (_selectedOptions[index] == i);
            rbOptions[i].Visible = true;
        }

        // Скрываем неиспользуемые варианты
        for (int i = question.Options.Count; i < 4; i++)
        {
            rbOptions[i].Visible = false;
        }

        // Обновляем текст кнопки
        btnNext.Text = (index == _category.Questions.Count - 1) ? "Завершить" : "Далее";
    }

    private void Option_CheckedChanged(object sender, EventArgs e)
    {
        var rb = sender as RadioButton;
        if (rb != null && rb.Checked && rb.Tag != null)
        {
            _selectedOptions[_currentQuestionIndex] = (int)rb.Tag;
        }
    }

    private void BtnNext_Click(object sender, EventArgs e)
    {
        // Проверяем, выбран ли ответ
        if (_selectedOptions[_currentQuestionIndex] == -1)
        {
            MessageBox.Show("Пожалуйста, выберите вариант ответа.", "Внимание",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Переходим к следующему вопросу
        if (_currentQuestionIndex < _category.Questions.Count - 1)
        {
            LoadQuestion(_currentQuestionIndex + 1);
        }
        else
        {
            FinishTest();
        }
    }

    private void FinishTest()
    {
        // Подсчет баллов
        int totalPoints = _category.CalculateTotalPoints(_selectedOptions);
        var result = _category.GetResultByPoints(totalPoints);

        // Сохраняем результат
        _dataService.SaveTestResult(_user.Username, _category.Id, totalPoints);

        // Показываем результат
        string message = $"Вы набрали {totalPoints} баллов!\n\n";
        if (result != null)
        {
            message += $"Результат: {result.Title}\n";
            message += $"{result.Description}";
        }
        else
        {
            message += "Результат не определен.";
        }

        MessageBox.Show(message, "Тест завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);

        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}