using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Presentation.Modules.Categories.ViewModels
{
    public class CategoryEditVM
    {
        public int Id {  get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 to 50 character")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description name is required")]
        [StringLength(50, ErrorMessage = "Name must be up to 500 characters")]
        [Display(Name = "Category Description")]
        public string? Description { get; set; }

        public DateTime CreateDate {  get; set; }
    }
}
