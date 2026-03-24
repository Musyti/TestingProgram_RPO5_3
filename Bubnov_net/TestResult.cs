using System.Text.Json;
using System.IO;

namespace Bubnov_omg
{
    public class TestResult
    {
        public string UserLogin { get; set; } = "";
        public string UserRole { get; set; } = "User";
        public DateTime TestDate { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public List<string> AnsweredQuestions { get; set; } = new();
        public TimeSpan TestDuration { get; set; }

        public static void Save(TestResult result)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(result, options);
            File.WriteAllText("test_results.json", json);
        }
    }
}
