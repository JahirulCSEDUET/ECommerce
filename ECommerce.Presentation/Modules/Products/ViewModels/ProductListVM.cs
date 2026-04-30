
namespace ECommerce.Presentation.Modules.Products.ViewModels
{
    public class ProductListVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
    }
}
