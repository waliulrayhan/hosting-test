using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace OnlineEcommerceShop.Models
{
    public class Products
    {
        public int Id { get; set; }


        public string Name { get; set; }


        public string Price { get; set; }


        public string Image { get; set; }


        [Display(Name = "Product Colour")]
        public string ProductColour { get; set; }


        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }
        [ForeignKey(nameof(ProductTypeId))]
        public ProductTypes ProductTypes { get; set; }
    }
}
