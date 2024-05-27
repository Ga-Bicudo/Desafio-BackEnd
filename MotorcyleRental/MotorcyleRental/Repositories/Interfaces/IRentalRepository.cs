using MotorcyleRental.Domain;

namespace MotorcyleRental.Repositories.Interfaces
{
    public interface IRentalRepository
    {
        Task <IEnumerable<Rental>> GetRent(int motorcycleId);
        Task AddRental(Rental rental);
        Task<Rental> GetRentalById(int id);
        Task<List<RentalPlan>> GetRentalPlans();
        Task UpdateRental(Rental rental);
    }
}
