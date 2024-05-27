using MotorcyleRental.Domain;

namespace MotorcyleRental.Repositories.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task<IEnumerable<Motorcycle>> GetAsync();
        Task AddAsync(Motorcycle motorcyle);
        Task EditAsync(Motorcycle motorcyle);
        Task DeleteAsync(int id);
    }
}
