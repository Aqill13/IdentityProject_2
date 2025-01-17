﻿namespace IdentityProject_2.Models
{
    public class ProductCopy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public IFormFile ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Category Category { get; set; }
    }
}
