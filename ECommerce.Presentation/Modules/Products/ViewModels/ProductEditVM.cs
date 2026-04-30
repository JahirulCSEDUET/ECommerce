
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Presentation.Modules.Products.ViewModels
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 to 100 character!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required!")]
        [StringLength(200, ErrorMessage = "Description must be less than or equal 200 character!")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than or equal 0!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "SKU is required!")]
        [StringLength(100, ErrorMessage = "SKU must be less than or equal 200 character!")]
        public string SKU { get; set; }

        [Required(ErrorMessage = "Category is required!")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Product Image")]
        public string? ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;

    }
}
