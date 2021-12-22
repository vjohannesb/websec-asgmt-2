namespace asgmt_2.Models
{
    public class AppFile
    {
        public AppFile()
        {
            var id = Guid.NewGuid();
            Id = id;
            Name = id.ToString();
            Timestamp = DateTime.Now;
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
                var shortName = filename[..Math.Min(16, filename.Length - 3)];
                return shortName.Length + 3 < filename.Length 
                    ? $"{shortName}(...){FileExtension}" 
                    : filename + FileExtension;
            }
        }
    }
}
