using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestForms
{
    public partial class test_started : Form
    {
        private int currentQuestion = 1;
        private const int totalQuestions = 10;
        int rightAnswers = 0;
        string description = "Шиноби скрытого листа"; //Будет брать текст на основе rightAnswers

        public test_started()
        {
            InitializeComponent();

            // Подписываем все кнопки на один обработчик
            var1.Click += AnswerButton_Click;
            var2.Click += AnswerButton_Click;
            var3.Click += AnswerButton_Click;
            var4.Click += AnswerButton_Click;

            // Подписываем кнопку возврата в меню
            back_to_menu_button.Click += Button1_Click;
        }

        private void AnswerButton_Click(object sender, EventArgs e)
        {
            if (currentQuestion < totalQuestions)
            {
                currentQuestion++;
                UpdateCounter();

                // Здесь можно добавить логику загрузки следующего вопроса
                LoadNextQuestion();
            }
            else
            {
                // Достигнут последний вопрос
                FinishTest();
            }
        }

        private void UpdateCounter()
        {
            counter.Text = $"{currentQuestion}/{totalQuestions}";
        }

        private void LoadNextQuestion()
        {
            // Пример обновления текста вопроса
            question_label.Text = $"Вопрос вопрос вопрос";

            // Здесь можно обновлять тексты кнопок-ответов
            // var1.Text = $"Ответ {currentQuestion}.1";
            // var2.Text = $"Ответ {currentQuestion}.2";
            // var3.Text = $"Ответ {currentQuestion}.3";
            // var4.Text = $"Ответ {currentQuestion}.4";
        }

        private void FinishTest()
        {
            // Блокируем кнопки
            var1.Enabled = false;
            var2.Enabled = false;
            var3.Enabled = false;
            var4.Enabled = false;
            var1.Visible = false;
            var2.Visible = false;
            var3.Visible = false;
            var4.Visible = false;
            question_label.Visible = false;
            counter.Visible = false;
            congrat_label.Visible = true;
            pictureBox1.Visible = true;
            result_description_label.Text = description; //Подвязка string description к result_description_label
            result_description_label.Visible = true;
            //MessageBox.Show($"Тест завершен! Количество правильных ответов: {rightAnswers}");

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Возврат в меню
            DialogResult result = MessageBox.Show("Вернуться в меню? Прогресс будет потерян!",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}