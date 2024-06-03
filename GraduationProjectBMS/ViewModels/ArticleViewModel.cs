namespace GraduationProjectBMS.ViewModels
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }
        public string? ArticleTitle { get; set; }
        public string? ArticleContent { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
