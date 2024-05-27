namespace MotorcyleRental.Domain
{
    public class Motorcycle
    {
        public DateOnly CreationDate { get; set; }
        public DateOnly MaintenanceDate { get; set; }

        public string Id { get; set; }
        public string Plate { get; set; }
        public string FactoryYear { get; set; }
        public string Model { get; set; }

        
    }
}
