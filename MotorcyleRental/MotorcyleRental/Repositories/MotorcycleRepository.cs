using Dapper;
using Microsoft.Extensions.Configuration;
using MotorcyleRental.Domain;
using MotorcyleRental.Repositories.Interfaces;
using Npgsql;

namespace MotorcyleRental.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly IConfiguration _configuration;

        public MotorcycleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task AddAsync(Motorcycle motorcyle)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var command = new NpgsqlCommand("INSERT INTO tbMotorcycles (FactoryYear, Model, Plate,CreationDate) VALUES (@FactoryYear, @Model, @Plate,getDate())", connection);
            command.Parameters.AddWithValue("FactoryYear", motorcyle.FactoryYear);
            command.Parameters.AddWithValue("Model", motorcyle.Model);
            command.Parameters.AddWithValue("Plate", motorcyle.Plate);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var command = new NpgsqlCommand("DELETE FROM tbMotorcycles WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("Id", id);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public async Task EditAsync(Motorcycle motorcyle)
        {
            using var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var command = new NpgsqlCommand("UPDATE tbMotorcycles " +
                                                "SET MaintenanceDate = getDate(), " +
                                                "Plate = @Plate, " +
                                                "Model = @Model, " +
                                                "FactoryYear = @FactoryYear, " +
                                                "WHERE Id = @Id", connection);

            command.Parameters.AddWithValue("Plate", motorcyle.Plate);
            command.Parameters.AddWithValue("Model", motorcyle.Model);
            command.Parameters.AddWithValue("FactoryYear", motorcyle.FactoryYear);
            command.Parameters.AddWithValue("Id", motorcyle.Id);
            
                

            connection.Open();
            command.ExecuteNonQuery();
        }

        public async Task<IEnumerable<Motorcycle>> GetAsync()
        {
            await using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var sql = "Select From tbMotorcycles" +
                     "order by CreationDate desc";

                return await connection.QueryAsync<Motorcycle>(sql);
            }
        }
    }
}
