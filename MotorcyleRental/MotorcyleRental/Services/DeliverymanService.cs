using AutoMapper;
using MotorcyleRental.Domain;
using MotorcyleRental.Models;
using MotorcyleRental.Repositories.Interfaces;
using MotorcyleRental.Services.Interfaces;

namespace MotorcyleRental.Services
{
    public class DeliverymanService : IDeliverymanService
    {
        private readonly IDeliverymanRepository _repository;
        private readonly IMapper _mapper;

        public DeliverymanService(IDeliverymanRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateDeliveryman(DeliverymanViewModel deliveryman)
        {
            if (await _repository.CNPJExists(deliveryman.CNPJ))
            {
                throw new Exception("CNPJ already exists.");
            }

            if (await _repository.CNHNumberExists(deliveryman.CNHNumber))
            {
                throw new Exception("CNH number already exists.");
            }

            await _repository.AddDeliveryman(_mapper.Map<Deliveryman>( deliveryman));

        }

        public async Task<DeliverymanViewModel> GetDeliverymanById(int id)
        {
            return _mapper.Map<DeliverymanViewModel>(await _repository.GetDeliverymanById(id));
        }

        public async Task UpdateCNHImage(int id, string imagePath)
        {
            await _repository.UpdateCNHImage(id, imagePath);
        }
    }
}
