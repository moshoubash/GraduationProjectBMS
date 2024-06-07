using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectBMS.Models
{
    public class Reply
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReplyId { get; set; }
        public string? ReplyContent { get; set; }
        public DateTime CreatedAt { get; set; }

        // referance for user
        public string? UserId { get; set; }
        public AppUser? AppUser { get; set; }

        // referance for comment id
        public int CommentId { get; set; }
        public Comment? Comment { get; set; }

        // referance for comment id
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
