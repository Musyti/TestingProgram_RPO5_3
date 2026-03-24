using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text.Json;
using System.IO;

namespace Bubnov_omg
{
    public partial class Form1 : Form
    {
        private User? currentUser;
        private const string ADMIN_PASSWORD = "admin123";

        // Тестовые переменные
        private int currentQuestion = 0;
        private int currentScore = 0;
        private DateTime testStartTime;
        private List<string> answeredQuestions = new List<string>();
        private List<Question> questions = new List<Question>();

        // UI элементы для теста
        private TextBox txtLogin, txtPassword;
        private Button btnLogin;
        private Label lblTitle, lblLoginLabel, lblPasswordLabel;
        private Panel panelLogin, panelTest, panelAdmin;
        private Label lblQuestion, lblScore, lblRole, lblProgress;
        private RadioButton rb1, rb2, rb3, rb4;
        private Button btnPrev, btnNext, btnFinish, btnLogout;

        public Form1()
        {
            InitializeComponent();
            CreateLoginForm();
            InitializeQuizData();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Form1
            // 
            ClientSize = new Size(684, 511);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Тестовое приложение v2.0";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        private void CreateLoginForm()
        {
            this.Controls.Clear();

            panelLogin = new Panel { Dock = DockStyle.Fill, BackColor = Color.LightBlue };

            lblTitle = new Label
            {
                Text = "🔐 АВТОРИЗАЦИЯ",
                Font = new Font("Arial", 20, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 80,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DarkBlue
            };

            // Простая подсказка
            var lblHint = new Label
            {
                Text = "👑 admin/admin123 = Админ панель\n👤 Любой логин/пароль = Тест",
                Left = 150,
                Top = 100,
                Size = new Size(400, 50),
                Font = new Font("Arial", 11, FontStyle.Bold),
                ForeColor = Color.DarkRed,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(255, 255, 200),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblLoginLabel = new Label
            {
                Text = "Логин:",
                Left = 150,
                Top = 170,
                Size = new Size(100, 30),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            txtLogin = new TextBox
            {
                Left = 270,
                Top = 170,
                Width = 220,
                Height = 30,
                Font = new Font("Arial", 12)
            };

            lblPasswordLabel = new Label
            {
                Text = "Пароль:",
                Left = 150,
                Top = 220,
                Size = new Size(100, 30),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            txtPassword = new TextBox
            {
                Left = 270,
                Top = 220,
                Width = 220,
                Height = 30,
                PasswordChar = '*',
                Font = new Font("Arial", 12)
            };

            btnLogin = new Button
            {
                Text = "🚀 ВОЙТИ",
                Left = 270,
                Top = 280,
                Width = 120,
                Height = 45,
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Green,
                ForeColor = Color.White
            };
            btnLogin.Click += BtnLogin_Click;

            panelLogin.Controls.AddRange(new Control[]
            { lblTitle, lblHint, lblLoginLabel, txtLogin, lblPasswordLabel, txtPassword, btnLogin });
            this.Controls.Add(panelLogin);
        }
        private void InitializeQuizData()
        {
            questions.Clear();
            questions.Add(new Question("1. Что такое класс в C#?", new[] { "Структура данных", "Ссылочный тип", "Значимый тип", "Перечисление" }, 1));
            questions.Add(new Question("2. Где хранятся объекты классов?", new[] { "Стек", "Куча (Heap)", "Регистры CPU", "Диск" }, 1));
            questions.Add(new Question("3. Что делает оператор new?", new[] { "Освобождает память", "Создает объект", "Копирует данные", "Удаляет переменную" }, 1));
            questions.Add(new Question("4. Что такое конструктор?", new[] { "Метод уничтожения", "Метод создания объекта", "Переменная", "Свойство" }, 1));
            questions.Add(new Question("5. Можно ли наследовать от struct?", new[] { "Да", "Нет", "Только от class", "Только от interface" }, 1));
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("❌ Введите логин и пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем админа
            if (User.IsAdminLogin(login, password))
            {
                currentUser = new User { Login = login, Password = password, Role = "Admin" };
                CreateAdminPanel();
                MessageBox.Show($"✅ Добро пожаловать, {login}! 👑 Администратор", "Вход выполнен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // ЛЮБОЙ другой логин/пароль = обычный пользователь
                currentUser = new User { Login = login, Password = password, Role = "User" };
                CreateTestPanel();
                MessageBox.Show($"✅ Добро пожаловать, {login}! 📝 Проходите тест", "Вход выполнен", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CreateTestPanel()
        {
            this.Controls.Clear(); // Очищаем все контролы

            panelTest = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(240, 255, 240) };

            // Роль пользователя
            lblRole = new Label
            {
                Text = $"👤 {currentUser.Login} (Пользователь)",
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.LightYellow,
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10)
            };

            // Прогресс
            lblProgress = new Label
            {
                Text = "Прогресс: 1/5",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Arial", 10),
                ForeColor = Color.Blue,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Счет
            lblScore = new Label
            {
                Text = "Баллы: 0/5",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Arial", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Вопрос
            lblQuestion = new Label
            {
                Text = "Вопрос появится здесь...",
                Dock = DockStyle.Top,
                Height = 100,
                Font = new Font("Arial", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.DarkGreen,
                Padding = new Padding(20)
            };

            // Панель для радиокнопок
            Panel radioPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30) };

            rb1 = new RadioButton { Dock = DockStyle.Top, Height = 35, Margin = new Padding(0, 5, 0, 5), Font = new Font("Arial", 11), AutoSize = true };
            rb2 = new RadioButton { Dock = DockStyle.Top, Height = 35, Margin = new Padding(0, 5, 0, 5), Font = new Font("Arial", 11), AutoSize = true };
            rb3 = new RadioButton { Dock = DockStyle.Top, Height = 35, Margin = new Padding(0, 5, 0, 5), Font = new Font("Arial", 11), AutoSize = true };
            rb4 = new RadioButton { Dock = DockStyle.Top, Height = 35, Margin = new Padding(0, 5, 0, 5), Font = new Font("Arial", 11), AutoSize = true };

            radioPanel.Controls.AddRange(new Control[] { rb1, rb2, rb3, rb4 });

            // Кнопки навигации
            Panel buttonPanel = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = Color.LightGray };
            btnPrev = new Button { Text = "⬅️ Назад", Left = 20, Width = 100, Height = 40, Top = 15, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnNext = new Button { Text = "Далее ➡️", Left = 130, Width = 100, Height = 40, Top = 15, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnFinish = new Button { Text = "✅ Завершить", Left = 240, Width = 120, Height = 40, Top = 15, Font = new Font("Arial", 10, FontStyle.Bold) };
            btnLogout = new Button { Text = "🚪 Выход", Left = 450, Width = 100, Height = 40, Top = 15, Font = new Font("Arial", 10, FontStyle.Bold) };

            btnPrev.Click += (s, e) => { if (currentQuestion > 0) { currentQuestion--; ShowCurrentQuestion(); } };
            btnNext.Click += BtnNext_Click;
            btnFinish.Click += BtnFinish_Click;
            btnLogout.Click += (s, e) => CreateLoginForm();

            buttonPanel.Controls.AddRange(new Control[] { btnPrev, btnNext, btnFinish, btnLogout });

            panelTest.Controls.AddRange(new Control[] { lblRole, lblProgress, lblScore, lblQuestion, radioPanel, buttonPanel });
            this.Controls.Add(panelTest);

            testStartTime = DateTime.Now;
            currentQuestion = 0;
            currentScore = 0;
            answeredQuestions.Clear();
            ShowCurrentQuestion();
        }

        private void CreateAdminPanel()
        {
            this.Controls.Clear();

            panelAdmin = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(255, 240, 240) };

            var lblAdminTitle = new Label
            {
                Text = "🔧 ПАНЕЛЬ АДМИНИСТРАТОРА",
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.Orange,
                Font = new Font("Arial", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White
            };

            var btnManageTests = new Button
            {
                Text = "📝 Управление тестами",
                Left = 50,
                Top = 150,
                Size = new Size(250, 60),
                Font = new Font("Arial", 14, FontStyle.Bold),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White
            };

            var btnViewResults = new Button
            {
                Text = "📊 Результаты тестов",
                Left = 320,
                Top = 150,
                Size = new Size(250, 60),
                Font = new Font("Arial", 14, FontStyle.Bold),
                BackColor = Color.ForestGreen,
                ForeColor = Color.White
            };

            var btnLogout = new Button
            {
                Text = "🚪 Выход",
                Left = 250,
                Top = 300,
                Size = new Size(150, 50),
                Font = new Font("Arial", 12, FontStyle.Bold),
                BackColor = Color.Red,
                ForeColor = Color.White
            };

            btnManageTests.Click += (s, e) => MessageBox.Show("🛠️ Здесь будет управление тестами (добавление/редактирование вопросов)", "Управление тестами");
            btnViewResults.Click += BtnViewResults_Click;
            btnLogout.Click += (s, e) => CreateLoginForm();

            panelAdmin.Controls.AddRange(new Control[] { lblAdminTitle, btnManageTests, btnViewResults, btnLogout });
            this.Controls.Add(panelAdmin);
        }

        private void ShowCurrentQuestion()
        {
            if (currentQuestion < questions.Count)
            {
                var q = questions[currentQuestion];
                lblQuestion.Text = q.Text;
                rb1.Text = $"A) {q.Answers[0]}";
                rb2.Text = $"B) {q.Answers[1]}";
                rb3.Text = $"C) {q.Answers[2]}";
                rb4.Text = $"D) {q.Answers[3]}";

                // Сбрасываем выбор
                rb1.Checked = rb2.Checked = rb3.Checked = rb4.Checked = false;

                lblScore.Text = $"Баллы: {currentScore}/{questions.Count}";
                lblProgress.Text = $"Прогресс: {currentQuestion + 1}/{questions.Count}";
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            CheckAnswer();
            if (currentQuestion < questions.Count - 1)
            {
                currentQuestion++;
                ShowCurrentQuestion();
            }
            else
            {
                MessageBox.Show("✅ Последний вопрос! Нажмите 'Завершить' для сохранения результатов.");
            }
        }

        private void CheckAnswer()
        {
            int selected = -1;
            if (rb1.Checked) selected = 0;
            else if (rb2.Checked) selected = 1;
            else if (rb3.Checked) selected = 2;
            else if (rb4.Checked) selected = 3;

            if (selected == questions[currentQuestion].CorrectAnswer)
                currentScore++;

            // Сохраняем информацию о отвеченном вопросе
            answeredQuestions.Add(questions[currentQuestion].Text);
        }

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            CheckAnswer();
            SaveTestResult();

            double percent = (double)currentScore / questions.Count * 100;
            string result = percent >= 80 ? "Отлично! 🎉" : percent >= 60 ? "Хорошо! 👍" : "Нужно подучить 📚";

            MessageBox.Show($"🎯 ТЕСТ ЗАВЕРШЕН!\n\n" +
                $"👤 {currentUser.Login}\n" +
                $"📊 {currentScore}/{questions.Count} баллов ({percent:F1}%)\n" +
                $"⏱️ Время: {(DateTime.Now - testStartTime):mm\\:ss}\n\n" +
                $"{result}", "Результаты", MessageBoxButtons.OK, MessageBoxIcon.Information);

            CreateLoginForm();
        }

        private void BtnViewResults_Click(object sender, EventArgs e)
        {
            if (File.Exists("test_results.json"))
            {
                try
                {
                    string json = File.ReadAllText("test_results.json");
                    var result = JsonSerializer.Deserialize<TestResult>(json);
                    MessageBox.Show($"📋 Последний тест:\n\n" +
                        $"👤 {result.UserLogin} ({result.UserRole})\n" +
                        $"📊 {result.Score}/{result.TotalQuestions} баллов\n" +
                        $"⏱️ {result.TestDuration:mm\\:ss}\n" +
                        $"📅 {result.TestDate:dd.MM.yyyy HH:mm}", "Результаты");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка чтения результатов: {ex.Message}", "Ошибка");
                }
            }
            else
            {
                MessageBox.Show("📭 Нет сохраненных результатов!", "Инфо");
            }
        }

        private void SaveTestResult()
        {
            var result = new TestResult
            {
                UserLogin = currentUser.Login,
                UserRole = currentUser.Role,
                TestDate = DateTime.Now,
                Score = currentScore,
                TotalQuestions = questions.Count,
                AnsweredQuestions = answeredQuestions,
                TestDuration = DateTime.Now - testStartTime
            };
            TestResult.Save(result);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}