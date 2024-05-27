using MotorcyleRental.Domain;

namespace MotorcyleRental.Repositories.Interfaces
{
    public interface IDeliverymanRepository
    {
        Task AddDeliveryman(Deliveryman deliveryman);
        Task<Deliveryman> GetDeliverymanById(int id);
        Task UpdateCNHImage(int id, string imagePath);
        Task<bool> CNPJExists(string cnpj);
        Task<bool> CNHNumberExists(string cnhNumber);
    }
}
