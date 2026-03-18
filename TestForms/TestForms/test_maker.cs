using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TestForms
{
    public partial class test_maker : Form
    {
        public class QuestionData
        {
            public string QuestionText { get; set; }
            public string[] Answers { get; set; }
            public int CorrectAnswerIndex { get; set; }

            public QuestionData()
            {
                Answers = new string[4];
                QuestionText = "";
                for (int i = 0; i < 4; i++)
                {
                    Answers[i] = "";
                }
                CorrectAnswerIndex = 0;
            }
        }

        private List<QuestionData> questions;
        private const int TOTAL_QUESTIONS = 10;
        private int currentQuestionNumber = 1;
        private bool isSaving = false;

        public List<QuestionData> CreatedQuestions
        {
            get { return questions; }
        }

        public test_maker()
        {
            try
            {
                InitializeComponent();

                questions = new List<QuestionData>();
                for (int i = 0; i < TOTAL_QUESTIONS; i++)
                {
                    questions.Add(new QuestionData());
                }

                SetupControls();

                LoadQuestion(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupControls()
        {
            try
            {
                this.Text = "Создание теста (10 вопросов)";

                nudCorrectAnswer.Minimum = 1;
                nudCorrectAnswer.Maximum = 4;
                nudCorrectAnswer.Value = 1;

                listQuestions.Items.Clear();
                for (int i = 1; i <= TOTAL_QUESTIONS; i++)
                {
                    listQuestions.Items.Add($"○ Вопрос {i}");
                }
                listQuestions.SelectedIndex = 0;

                btnSaveQuestion.Click += BtnSaveQuestion_Click;
                btnClear.Click += BtnClear_Click;
                btnExit.Click += BtnExit_Click;
                btnNext.Click += BtnNext_Click;
                btnPrevious.Click += BtnPrevious_Click;
                listQuestions.SelectedIndexChanged += ListQuestions_SelectedIndexChanged;

                UpdateFilledQuestionsCounter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при настройке элементов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadQuestion(int questionNumber)
        {
            try
            {
                if (questionNumber < 1 || questionNumber > TOTAL_QUESTIONS)
                    return;

                if (questions == null || questions.Count < questionNumber)
                    return;

                currentQuestionNumber = questionNumber;
                var question = questions[questionNumber - 1];

                txtQuestion.Text = question?.QuestionText ?? "";
                txtAnswer1.Text = question?.Answers?[0] ?? "";
                txtAnswer2.Text = question?.Answers?[1] ?? "";
                txtAnswer3.Text = question?.Answers?[2] ?? "";
                txtAnswer4.Text = question?.Answers?[3] ?? "";

                if (question != null)
                {
                    nudCorrectAnswer.Value = question.CorrectAnswerIndex + 1;
                }

                lblCurrentQuestion.Text = $"Вопрос {questionNumber} из {TOTAL_QUESTIONS}";

                if (listQuestions.Items.Count >= questionNumber)
                {
                    listQuestions.SelectedIndex = questionNumber - 1;
                }

                UpdateNavigationButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке вопроса: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveCurrentQuestion()
        {
            if (isSaving) return;

            try
            {
                isSaving = true;

                if (!ValidateInput())
                    return;

                if (questions == null || questions.Count < currentQuestionNumber)
                    return;

                var question = new QuestionData
                {
                    QuestionText = txtQuestion.Text.Trim(),
                    Answers = new string[]
                    {
                        txtAnswer1.Text.Trim(),
                        txtAnswer2.Text.Trim(),
                        txtAnswer3.Text.Trim(),
                        txtAnswer4.Text.Trim()
                    },
                    CorrectAnswerIndex = (int)nudCorrectAnswer.Value - 1
                };

                questions[currentQuestionNumber - 1] = question;
                UpdateListBoxItem(currentQuestionNumber);
                UpdateFilledQuestionsCounter();

                ShowStatusMessage($"Вопрос {currentQuestionNumber} сохранен!", false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isSaving = false;
            }
        }

        private void UpdateListBoxItem(int questionNumber)
        {
            try
            {
                if (questionNumber < 1 || questionNumber > TOTAL_QUESTIONS)
                    return;

                if (listQuestions.InvokeRequired)
                {
                    listQuestions.Invoke(new Action<int>(UpdateListBoxItem), questionNumber);
                    return;
                }

                if (listQuestions.Items.Count < questionNumber)
                    return;

                if (questions == null || questions.Count < questionNumber)
                    return;

                var question = questions[questionNumber - 1];
                if (question == null) return;

                bool isFilled = !string.IsNullOrWhiteSpace(question.QuestionText) &&
                               !string.IsNullOrWhiteSpace(question.Answers[0]) &&
                               !string.IsNullOrWhiteSpace(question.Answers[1]) &&
                               !string.IsNullOrWhiteSpace(question.Answers[2]) &&
                               !string.IsNullOrWhiteSpace(question.Answers[3]);

                string prefix = isFilled ? "✓ " : "○ ";
                listQuestions.Items[questionNumber - 1] = $"{prefix}Вопрос {questionNumber}";
            }
            catch
            {
            }
        }

        private void UpdateFilledQuestionsCounter()
        {
            try
            {
                int filledCount = 0;
                for (int i = 0; i < TOTAL_QUESTIONS; i++)
                {
                    if (questions != null && i < questions.Count)
                    {
                        var q = questions[i];
                        if (q != null &&
                            !string.IsNullOrWhiteSpace(q.QuestionText) &&
                            q.Answers != null &&
                            !string.IsNullOrWhiteSpace(q.Answers[0]) &&
                            !string.IsNullOrWhiteSpace(q.Answers[1]) &&
                            !string.IsNullOrWhiteSpace(q.Answers[2]) &&
                            !string.IsNullOrWhiteSpace(q.Answers[3]))
                        {
                            filledCount++;
                        }
                    }
                }

                lblTotalQuestions.Text = $"Заполнено: {filledCount}/{TOTAL_QUESTIONS}";

                if (btnFinish != null)
                {
                    btnFinish.Enabled = (filledCount == TOTAL_QUESTIONS);
                }
            }
            catch
            {
            }
        }

        private void UpdateNavigationButtons()
        {
            try
            {
                if (btnPrevious != null)
                    btnPrevious.Enabled = (currentQuestionNumber > 1);

                if (btnNext != null)
                    btnNext.Enabled = (currentQuestionNumber < TOTAL_QUESTIONS);
            }
            catch
            {
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtQuestion.Text))
            {
                ShowStatusMessage("Введите текст вопроса!", true);
                txtQuestion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAnswer1.Text))
            {
                ShowStatusMessage("Введите вариант ответа 1!", true);
                txtAnswer1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAnswer2.Text))
            {
                ShowStatusMessage("Введите вариант ответа 2!", true);
                txtAnswer2.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAnswer3.Text))
            {
                ShowStatusMessage("Введите вариант ответа 3!", true);
                txtAnswer3.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAnswer4.Text))
            {
                ShowStatusMessage("Введите вариант ответа 4!", true);
                txtAnswer4.Focus();
                return false;
            }

            return true;
        }

        private void ShowStatusMessage(string message, bool isError)
        {
            try
            {
                if (lblStatus == null || lblStatus.IsDisposed || this.IsDisposed)
                    return;

                lblStatus.Text = message;
                lblStatus.ForeColor = isError ? Color.Red : Color.Green;

                var timer = new System.Windows.Forms.Timer();
                timer.Interval = 3000;
                timer.Tick += (s, e) =>
                {
                    try
                    {
                        if (timer != null)
                        {
                            timer.Stop();
                            timer.Dispose();
                        }

                        if (!this.IsDisposed && lblStatus != null && !lblStatus.IsDisposed)
                        {
                            lblStatus.Text = "";
                        }
                    }
                    catch
                    {
                    }
                };
                timer.Start();
            }
            catch
            {
            }
        }

        private void BtnSaveQuestion_Click(object sender, EventArgs e)
        {
            SaveCurrentQuestion();
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (currentQuestionNumber < TOTAL_QUESTIONS)
            {
                SaveCurrentQuestion();
                LoadQuestion(currentQuestionNumber + 1);
            }
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (currentQuestionNumber > 1)
            {
                SaveCurrentQuestion();
                LoadQuestion(currentQuestionNumber - 1);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    $"Очистить все поля вопроса {currentQuestionNumber}?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    txtQuestion.Clear();
                    txtAnswer1.Clear();
                    txtAnswer2.Clear();
                    txtAnswer3.Clear();
                    txtAnswer4.Clear();
                    nudCorrectAnswer.Value = 1;

                    if (questions != null && questions.Count >= currentQuestionNumber)
                    {
                        questions[currentQuestionNumber - 1] = new QuestionData();
                        UpdateListBoxItem(currentQuestionNumber);
                        UpdateFilledQuestionsCounter();
                    }

                    ShowStatusMessage($"Вопрос {currentQuestionNumber} очищен", false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при очистке: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCurrentQuestion();

                int filledCount = 0;
                for (int i = 0; i < TOTAL_QUESTIONS; i++)
                {
                    if (questions != null && i < questions.Count)
                    {
                        var q = questions[i];
                        if (q != null &&
                            !string.IsNullOrWhiteSpace(q.QuestionText) &&
                            q.Answers != null &&
                            !string.IsNullOrWhiteSpace(q.Answers[0]) &&
                            !string.IsNullOrWhiteSpace(q.Answers[1]) &&
                            !string.IsNullOrWhiteSpace(q.Answers[2]) &&
                            !string.IsNullOrWhiteSpace(q.Answers[3]))
                        {
                            filledCount++;
                        }
                    }
                }

                if (filledCount < TOTAL_QUESTIONS)
                {
                    DialogResult result = MessageBox.Show(
                        $"Заполнено только {filledCount} из {TOTAL_QUESTIONS} вопросов. Выйти без сохранения?",
                        "Подтверждение",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.No)
                        return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выходе: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listQuestions.SelectedIndex >= 0 && listQuestions.SelectedIndex < TOTAL_QUESTIONS)
                {
                    SaveCurrentQuestion();
                    LoadQuestion(listQuestions.SelectedIndex + 1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе вопроса: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<QuestionData> GetQuestions()
        {
            try
            {
                SaveCurrentQuestion();
                return questions != null ? new List<QuestionData>(questions) : new List<QuestionData>();
            }
            catch
            {
                return new List<QuestionData>();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
    }
}