using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3)]
        public string? Name { get; set; }
        [Required]
        [StringLength (200)]
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
