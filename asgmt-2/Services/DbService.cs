using asgmt_2.Models;

namespace asgmt_2.Services
{
    public class DbService : IDbService
    {
        public List<Comment> Comments { get; set; } = new();
        public List<AppFile> Files { get; set; } = new();

        public void AddComment(Comment comment) => Comments.Add(comment);

        public void UploadFile(AppFile file) => Files.Add(file);

        public AppFile? GetFile(Guid id) => Files.FirstOrDefault(f => f.Id == id);
    }
}
