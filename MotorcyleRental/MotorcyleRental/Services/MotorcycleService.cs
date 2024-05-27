using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MotorcyleRental.Domain;
using MotorcyleRental.Messaging;
using MotorcyleRental.Models;
using MotorcyleRental.Repositories.Interfaces;
using System.Linq;
using System.Text.Json;
using System.Text;
using MotorcyleRental.Services.Interfaces;
namespace MotorcyleRental.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMapper _mapper;
        private readonly IMessageMQService _MessageMQService;
        private readonly IRentalRepository _rentalRepository;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository, IMapper mapper, IMessageMQService messageMQService, IRentalRepository rentalRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _mapper = mapper;
            _MessageMQService = messageMQService;
            _rentalRepository = rentalRepository;
        }

        public async Task AddAsync(MotorCycleViewModel motorcyle)
        {
            
            var validationMotorcyles = (await GetAsync(motorcyle.Plate)).Where(x => x.Plate.Trim() == motorcyle.Plate.Trim()).ToList();
            if ((validationMotorcyles).Count() > 0)
            {
                throw new Exception("Motorcycle already exists!");
            }

            _MessageMQService.SendMessage(JsonSerializer.Serialize(motorcyle));

            

            var messages = _MessageMQService.ReceiveMessages();
            if (motorcyle.FactoryYear == "2024" && messages.Count() > 0)
            {
                var motorcycleSaving = JsonSerializer.Deserialize<MotorCycleViewModel>(messages.FirstOrDefault()); //Como nesse cenario em especifico sei que voltara apenas uma mensagem por vez...
                motorcyle = motorcycleSaving;
            }

            await _motorcycleRepository.AddAsync(_mapper.Map<Motorcycle>(motorcyle));
        }

        public async Task DeleteAsync(int id)
        {
            var rents = await _rentalRepository.GetRent(id);
            if (rents.Count() > 0)
            {
                throw new Exception("Motorcycle on Rent");
            }
            await _motorcycleRepository.DeleteAsync(id);
        }

        public async Task<MotorCycleViewModel> EditAsync(MotorCycleViewModel motorcyle)
        {
            var existingMotorcycle = (await GetAsync(motorcyle.Plate)).Where(x => x.Id == motorcyle.Id).FirstOrDefault();
            if (existingMotorcycle == null)
            {
                throw new Exception("Motorcycle don't exists!");
            }
            await _motorcycleRepository.EditAsync(_mapper.Map<Motorcycle>(motorcyle));

            return motorcyle;
        }

        public async Task<IEnumerable<MotorCycleViewModel>> GetAsync(string filter = "")
        {
            var motorcycles = _mapper.Map<IEnumerable<MotorCycleViewModel>>(await _motorcycleRepository.GetAsync());

            if (!string.IsNullOrEmpty(filter))
            {
                return motorcycles.Where(x => x.Plate.Contains(filter.Trim())).ToList();
            }
            return motorcycles;
        }

       
    }
}
