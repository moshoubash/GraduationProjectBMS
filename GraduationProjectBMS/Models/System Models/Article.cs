using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GraduationProjectBMS.Models.System_Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        [Required]
        public string? ArticleTitle { get; set; }
        public string? ArticleThumbnail { get; set; }
        public string? ArticleDescription { get; set; }
        public string? ArticleContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EditAt { get; set; }

        public string? Id { get; set; } // ForeignKey from AppUser class
        public AppUser? AppUser { get; set; }
    }
}
