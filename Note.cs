namespace YMH219_NotepadApp
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }


        public Note()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            UpdateAt = CreatedAt;
        }

        public Note(string title, string content) : this()
        {
            Title = title;
            Content = content;
        }

        public void Update(string title, string content)
        {
            Title = title;
            Content = content;

            UpdateAt = DateTime.Now;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Title) ? "(Başlıksız Not)" : Title;
        }

    }
}