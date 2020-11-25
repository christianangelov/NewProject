namespace NewProject
{
    public class Address
    {
        public string SSN { get; set; }
        public short AddressNo { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public short StreetNo { get; set; }
        public Person Person { get; set; }
    }
}