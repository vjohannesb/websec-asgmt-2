using asgmt_2.Models;

namespace asgmt_2.Services
{
    public interface IDbService
    {
        List<Comment> Comments { get; set; }
        List<AppFile> Files { get; set; }

        void AddComment(Comment comment);

        void UploadFile(AppFile file);

        AppFile? GetFile(Guid id);
    }
}
