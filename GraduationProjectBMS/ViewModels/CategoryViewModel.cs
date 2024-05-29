﻿using GraduationProjectBMS.Models;

namespace GraduationProjectBMS
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<Article>? Articles { get; set; }
    }
}
