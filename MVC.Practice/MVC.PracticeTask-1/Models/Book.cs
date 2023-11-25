﻿namespace MVC.PracticeTask_1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Tax { get; set; }
        public string Code { get; set; }
        public bool IsAvailable { get; set; }
        public double CostPrice { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPercent { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}