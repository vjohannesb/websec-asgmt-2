namespace asgmt_2.Models
{
    public class Comment
    {
        public Comment()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            Content = string.Empty;
        }

        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
