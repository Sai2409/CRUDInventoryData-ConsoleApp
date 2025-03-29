using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceInventory.Models
{
    public class ProductStockAvailability
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; }

        public int? QuantityAvailable { get; set; }

        public Product Product { get; set; }
    }

}
