using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace Bubnov_omg
{
    public class TestResult
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // В продакшене хэшируйте!
        public DateTime TestDate { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public List<string> AnsweredQuestions { get; set; } = new(); // Названия или ID отвеченных вопросов
        public TimeSpan TestDuration { get; set; }
}


}
