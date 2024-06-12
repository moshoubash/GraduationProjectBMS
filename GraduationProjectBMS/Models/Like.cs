﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectBMS.Models
{
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeId { get; set; }

        public string? UserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
