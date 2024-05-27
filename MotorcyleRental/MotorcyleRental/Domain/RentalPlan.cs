namespace MotorcyleRental.Domain
{
    public class RentalPlan
    {
        public int Id { get; set; }
        public int Days { get; set; }
        public decimal DailyRate { get; set; }
        public decimal EarlyReturnPenalty { get; set; }
    }
}
