namespace SV_prep_1.Server.Models
{
    public class Address
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public Store Store { get; set; }

        public Address()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
