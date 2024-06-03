namespace GraduationProjectBMS.ViewModels
{
    public class ReplyViewModel
    {
        public int ReplyId { get; set; }
        public string? ReplyContent { get; set; }
        public string? UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommentId { get; set; }
    }
}
