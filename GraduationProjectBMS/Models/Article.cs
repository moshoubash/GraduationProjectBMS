using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GraduationProjectBMS.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string? ArticleTitle { get; set; }
        [Display(Name = "Thumbnail")]
        [DataType(DataType.Upload)]
        public string? ArticleThumbnail { get; set; }
        [Display(Name = "Description")]
        public string? ArticleDescription { get; set; }
        [Display(Name = "Content")]
        public string? ArticleContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EditAt { get; set; }

        public string? Id { get; set; } // ForeignKey from AppUser class
        public AppUser? AppUser { get; set; }
    }
}
