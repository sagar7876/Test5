﻿namespace ElitaShop.Services.Dtos.Category
{
    public class CategoryUpdateDto
    {
        public string Title { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public IFormFile? CategoryImage { get; set; };
    }
}
