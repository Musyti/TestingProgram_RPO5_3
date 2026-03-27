using System.Collections.Generic;
using System.Linq;
using WinFormsApp1.Models;

namespace WinFormsApp1.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; set; }
        public List<ResultRange> Results { get; set; }

        public Category()
        {
            Id = System.Guid.NewGuid().ToString();
            Questions = new List<Question>();
            Results = new List<ResultRange>();
            Name = "";
            Description = "";
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   Questions.Count == 10 &&
                   Questions.All(q => q.IsValid()) &&
                   Results.Count == 3 &&
                   Results.All(r => r.IsValid());
        }

        public int CalculateTotalPoints(List<int> selectedOptions)
        {
            int total = 0;
            for (int i = 0; i < Questions.Count && i < selectedOptions.Count; i++)
            {
                total += Questions[i].GetPointsForOption(selectedOptions[i]);
            }
            return total;
        }

        public ResultRange GetResultByPoints(int points)
        {
            return Results.FirstOrDefault(r => r.Contains(points));
        }
    }
}
