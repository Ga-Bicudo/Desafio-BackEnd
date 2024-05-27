namespace MotorcyleRental.Domain
{
    public class Rental
    {
        public int Id { get; set; }
        public int DeliverymanId { get; set; }
        public int MotorcycleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsActive { get; set; }

    }
}
