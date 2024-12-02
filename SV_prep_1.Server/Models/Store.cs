namespace SV_prep_1.Server.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
