namespace LearnEasyOnline.Api.Data
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; } // Nullable for free courses
        public string? Content { get; set; } // Example: Could be a path to content
    }
}
