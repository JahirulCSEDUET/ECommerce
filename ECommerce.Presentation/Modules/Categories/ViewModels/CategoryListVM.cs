using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ECommerce.Presentation.Modules.Categories.ViewModels
{
    public class CategoryListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
