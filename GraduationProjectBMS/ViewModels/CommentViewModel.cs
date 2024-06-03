namespace GraduationProjectBMS.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        public string? CommentContent { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<ReplyViewModel>? Replies { get; set; }
        public int? ArticleId { get; set; }
    }
}
