using MotorcyleRental.Domain;
using MotorcyleRental.Models;

namespace MotorcyleRental.Services.Interfaces
{
    public interface IMotorcycleService
    {
        Task<IEnumerable<MotorCycleViewModel>> GetAsync(string filter = "");
        Task AddAsync(MotorCycleViewModel motorcyle);
        Task<MotorCycleViewModel> EditAsync(MotorCycleViewModel motorcyle);
        Task DeleteAsync(int id);
    }
}
