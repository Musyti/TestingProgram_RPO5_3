namespace Bubnov_omg
{
    public class Question
    {
        public string Text { get; set; } = "";
        public string[] Answers { get; set; } = new string[0];
        public int CorrectAnswer { get; set; } // Индекс правильного ответа (0-3)

        public Question(string text, string[] answers, int correctAnswer)
        {
            Text = text;
            Answers = answers;
            CorrectAnswer = correctAnswer;
        }
    }
}