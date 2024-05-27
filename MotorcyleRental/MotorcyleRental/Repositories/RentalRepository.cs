using Dapper;
using MotorcyleRental.Domain;
using MotorcyleRental.Repositories.Interfaces;
using System.Data;

namespace MotorcyleRental.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IDbConnection _dbConnection;

        public RentalRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddRental(Rental rental)
        {
            string sql = @"INSERT INTO Rentals (DeliverymanId, MotorcycleId, StartDate, EndDate, ExpectedEndDate, TotalCost, IsActive)
                       VALUES (@DeliverymanId, @MotorcycleId, @StartDate, @EndDate, @ExpectedEndDate, @TotalCost, @IsActive)";
            await _dbConnection.ExecuteAsync(sql, rental);
        }

        public async Task<IEnumerable<Rental>> GetRent(int motorcycleId)
        {
            string sql = "SELECT * FROM Rentals WHERE motorcycleId = @motorcycleId";
            var result = await _dbConnection.QueryAsync<Rental>(sql, new { motorcycleId = motorcycleId });
            return result;
        }

        public async Task<Rental> GetRentalById(int id)
        {
            string sql = "SELECT * FROM Rentals WHERE Id = @Id";
            var result = await _dbConnection.QueryAsync<Rental>(sql, new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<List<RentalPlan>> GetRentalPlans()
        {
            string sql = "SELECT * FROM RentalPlans";
            var result = await _dbConnection.QueryAsync<RentalPlan>(sql);
            return result.ToList();
        }

        public async Task UpdateRental(Rental rental)
        {
            string sql = @"UPDATE Rentals SET EndDate = @EndDate, TotalCost = @TotalCost, IsActive = @IsActive 
                       WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, rental);
        }
    }
}
