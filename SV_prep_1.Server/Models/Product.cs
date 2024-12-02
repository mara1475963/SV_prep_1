using System.Text.Json.Serialization;

namespace SV_prep_1.Server.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public decimal Price { get; set; }
        public int StoreId { get; set; }

        [JsonIgnore]
        public Store Store { get; set; }
    }
}
