using System.ComponentModel.DataAnnotations;

namespace ECommerce.Presentation.Modules.Categories.ViewModels
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 to 50 character")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description name is required")]
        [StringLength(50, ErrorMessage = "Name must be up to 500 characters")]
        [Display(Name = "Category Description")]
        public string? Description { get; set; }
    }
}
