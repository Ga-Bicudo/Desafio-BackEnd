namespace MotorcyleRental.Services.Interfaces
{
    public interface IRentalsService
    {
        Task<bool> CreateRental(int deliverymanId, int motorcycleId, int rentalPlanId);
        Task<decimal> CalculateTotalCost(int rentalId, DateTime actualEndDate);
    }
}
