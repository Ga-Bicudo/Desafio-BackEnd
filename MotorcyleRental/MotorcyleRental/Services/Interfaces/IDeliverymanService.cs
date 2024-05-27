using MotorcyleRental.Domain;
using MotorcyleRental.Models;

namespace MotorcyleRental.Services.Interfaces
{
    public interface IDeliverymanService
    {
        Task CreateDeliveryman(DeliverymanViewModel deliveryPerson);
        Task<DeliverymanViewModel> GetDeliverymanById(int id);
        Task UpdateCNHImage(int id, string imagePath);
    }
}
