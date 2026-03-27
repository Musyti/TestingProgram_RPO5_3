namespace WinFormsApp1.Models
{
    public class ResultRange
    {
        public int MinPoints { get; set; }
        public int MaxPoints { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public ResultRange()
        {
            Title = "";
            Description = "";
            ImagePath = "";
        }

        public bool Contains(int points)
        {
            return points >= MinPoints && points <= MaxPoints;
        }

        public bool IsValid()
        {
            return MinPoints <= MaxPoints &&
                   !string.IsNullOrWhiteSpace(Title);
        }
    }
}