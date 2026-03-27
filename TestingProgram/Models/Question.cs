using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp1.Models
{
    public class Question
    {
        public string Content { get; set; }
        public List<string> Options { get; set; }
        public List<int> PointsOptions { get; set; }
        public int Points { get; set; }

        public Question()
        {
            Options = new List<string>();
            PointsOptions = new List<int>();
            Content = "";
            Points = 0;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Content) &&
                   Options.Count == 4 &&
                   Options.All(o => !string.IsNullOrWhiteSpace(o)) &&
                   PointsOptions.Count == 4;
        }

        public int GetPointsForOption(int optionIndex)
        {
            if (optionIndex >= 0 && optionIndex < PointsOptions.Count)
                return PointsOptions[optionIndex];
            return 0;
        }
    }
}