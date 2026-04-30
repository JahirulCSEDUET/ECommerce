using AutoMapper;
using ECommerce.Domain.Entities;
using ECommerce.Presentation.Modules.Categories.ViewModels;
namespace ECommerce.Presentation.Modules.Categories
{
    public class CategoryMappingProfile:Profile
    {
        public CategoryMappingProfile() {
            //ViewModel -> Entity
            CreateMap<CategoryCreateVM, Category>();
            CreateMap<CategoryEditVM,Category>();
            //Entity -> ViewModel
            CreateMap<Category, CategoryListVM>();
            CreateMap<Category, CategoryEditVM>();
        }
    }
}
