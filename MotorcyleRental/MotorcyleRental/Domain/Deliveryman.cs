namespace MotorcyleRental.Domain
{
    public class Deliveryman
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CNHNumber { get; set; }
        public string CNHType { get; set; }
        public string CNHImage { get; set; }
    }
}
