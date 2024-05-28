using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GraduationProjectBMS.Models
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArticleId { get; set; }
        public string? ArticleTitle { get; set; }
        public string? ArticleThumbnail { get; set; }
        public string? ArticleContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime EditAt { get; set; }
        public string? UserFullName { get; set; }

        // referance for AppUser class
        public string? Id { get; set; } 
        public AppUser? AppUser { get; set; }

        // Likes
        public int TotalLikes { get; set; }
        
        // list from comment class
        public List<Comment>? Comments { get; set; }
        
        // list from Tag class
        public List<Tag>? Tags { get; set; }

        // list from like class
        public List<Like>? Likes { get; set; }

        // referance for category class
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
