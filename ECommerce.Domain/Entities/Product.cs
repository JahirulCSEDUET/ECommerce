using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength =3)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required]
        [StringLength (100)]
        public string SKU { get; set; }
        [StringLength(500)]
        public string? ImagePath {  get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate {  get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
        public Inventory? Inventories { get; set; } 
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
