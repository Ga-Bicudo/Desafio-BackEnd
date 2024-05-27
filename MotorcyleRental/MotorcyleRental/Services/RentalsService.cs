using MotorcyleRental.Domain;
using MotorcyleRental.Repositories;
using MotorcyleRental.Repositories.Interfaces;
using MotorcyleRental.Services.Interfaces;

namespace MotorcyleRental.Services
{
    public class RentalsService : IRentalsService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IDeliverymanRepository _deliverymanRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;

        public RentalsService(IRentalRepository rentalRepository, IDeliverymanRepository deliverymanRepository, IMotorcycleRepository motorcycleRepository)
        {
            _rentalRepository = rentalRepository;
            _deliverymanRepository = deliverymanRepository;
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<bool> CreateRental(int deliverymanId, int motorcycleId, int rentalPlanId)
        {
            var deliveryman = await _deliverymanRepository.GetDeliverymanById(deliverymanId);
            if (deliveryman == null || !(deliveryman.CNHType.Contains("A") || deliveryman.CNHType.Contains("B")))
            {
                throw new Exception("Only valid deliverymen with a category A or B CNH can rent a motorcycle.");
            }

            var rentalPlans = await _rentalRepository.GetRentalPlans();
            var rentalPlan = rentalPlans.FirstOrDefault(rp => rp.Id == rentalPlanId);
            if (rentalPlan == null)
            {
                throw new Exception("Invalid rental plan.");
            }

            var startDate = DateTime.Now.AddDays(1);
            var endDate = startDate.AddDays(rentalPlan.Days);
            var totalCost = rentalPlan.Days * rentalPlan.DailyRate;

            var rental = new Rental
            {
                DeliverymanId = deliverymanId,
                MotorcycleId = motorcycleId,
                StartDate = startDate,
                EndDate = endDate,
                ExpectedEndDate = endDate,
                TotalCost = totalCost,
                IsActive = true
            };

            await _rentalRepository.AddRental(rental);
            return true;
        }

        public async Task<decimal> CalculateTotalCost(int rentalId, DateTime actualEndDate)
        {
            var rental = await _rentalRepository.GetRentalById(rentalId);
            if (rental == null)
            {
                throw new Exception("Rental not found.");
            }

            var rentalPlans = await _rentalRepository.GetRentalPlans();
            var rentalPlan = rentalPlans.FirstOrDefault(rp => rp.Days == (rental.ExpectedEndDate - rental.StartDate).Days);
            if (rentalPlan == null)
            {
                throw new Exception("Rental plan not found.");
            }

            decimal totalCost = rental.TotalCost;

            if (actualEndDate < rental.ExpectedEndDate)
            {
                var daysDifference = (rental.ExpectedEndDate - actualEndDate).Days;
                var penalty = daysDifference * rentalPlan.DailyRate * rentalPlan.EarlyReturnPenalty;
                totalCost -= penalty;
            }
            else if (actualEndDate > rental.ExpectedEndDate)
            {
                var extraDays = (actualEndDate - rental.ExpectedEndDate).Days;
                totalCost += extraDays * 50;
            }

            rental.EndDate = actualEndDate;
            rental.TotalCost = totalCost;
            rental.IsActive = false;

            await _rentalRepository.UpdateRental(rental);

            return totalCost;
        }
    }
}
