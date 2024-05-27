using AutoMapper;
using MotorcyleRental.Domain;
using MotorcyleRental.Models;

namespace MotorcyleRental.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DeliverymanViewModel, Deliveryman>();
            CreateMap<Deliveryman, DeliverymanViewModel>();
            CreateMap<Motorcycle, MotorCycleViewModel>();
            CreateMap<MotorCycleViewModel, Motorcycle>();
        }
    }
}
