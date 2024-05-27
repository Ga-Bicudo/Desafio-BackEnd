using Dapper;
using MotorcyleRental.Domain;
using MotorcyleRental.Repositories.Interfaces;
using System.Data;

namespace MotorcyleRental.Repositories
{
    public class DeliverymanRepository : IDeliverymanRepository
    {
        private readonly IDbConnection _dbConnection;

        public DeliverymanRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddDeliveryman(Deliveryman deliveryman)
        {
            string sql = @"INSERT INTO Deliverymen (Name, CNPJ, DateOfBirth, CNHNumber, CNHType, CNHImage)
                       VALUES (@Name, @CNPJ, @DateOfBirth, @CNHNumber, @CNHType, @CNHImage)";
            await _dbConnection.ExecuteAsync(sql, deliveryman);
        }

        public async Task<Deliveryman> GetDeliverymanById(int id)
        {
            string sql = "SELECT * FROM Deliverymen WHERE Id = @Id";
            var result = await _dbConnection.QueryAsync<Deliveryman>(sql, new { Id = id });
            return result.FirstOrDefault();
        }

        public async Task UpdateCNHImage(int id, string imagePath)
        {
            string sql = "UPDATE Deliverymen SET CNHImage = @ImagePath WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { ImagePath = imagePath, Id = id });
        }

        public async Task<bool> CNPJExists(string cnpj)
        {
            string sql = "SELECT COUNT(1) FROM Deliverymen WHERE CNPJ = @CNPJ";
            var result = await _dbConnection.ExecuteScalarAsync<int>(sql, new { CNPJ = cnpj });
            return result > 0;
        }

        public async Task<bool> CNHNumberExists(string cnhNumber)
        {
            string sql = "SELECT COUNT(1) FROM Deliverymen WHERE CNHNumber = @CNHNumber";
            var result = await _dbConnection.ExecuteScalarAsync<int>(sql, new { CNHNumber = cnhNumber });
            return result > 0;
        }
    }
}
