namespace asgmt_2.Models
{
    public class AppFile
    {
        public AppFile()
        {
            var id = Guid.NewGuid();
            Id = id;
            Name = id.ToString();
            Timestamp = DateTime.UtcNow;
            UnsafeName = string.Empty;
            FileExtension = string.Empty;
            Size = 0;
        }

        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string UnsafeName { get; set; }
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public long Size { get; set; }
        public byte[]? Data { get; set; }

        public string DisplayName
        {
            get
            {
                var filename = Path.GetFileNameWithoutExtension(UnsafeName);
                var maxLength = Math.Min(15, Math.Max(0, filename.Length - 5));
                var shortName = filename[..maxLength];
                return shortName.Length + 5 < filename.Length
                    ? $"{shortName}(...){FileExtension}"
                    : filename + FileExtension;
            }
        }
    }
}
