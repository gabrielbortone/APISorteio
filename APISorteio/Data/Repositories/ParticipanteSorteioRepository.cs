using APISorteio.Data.Repositories.Interfaces;
using APISorteio.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APISorteio.Data.Repositories
{
    public class ParticipanteSorteioRepository : IParticipanteSorteioRepository
    {
        private readonly string _connectionString;
        public ParticipanteSorteioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<int> Add(ParticipanteSorteio entity)
        {
            var sql = "INSERT INTO ParticipanteSorteio(ParticipanteId, SorteioId) VALUES (@ParticipanteId, @SorteioId)";
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public async Task<int> Delete(int idParticipante, int idSorteio)
        {
            var sql = "DELETE FROM ParticipanteSorteio WHERE ParticipanteId = @Id_Participante AND SorteioId = @Id_Sorteio";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id_Participante = idParticipante, Id_Sorteio = idSorteio });
                return affectedRows;
            }
        }

        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ParticipanteSorteio> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<ParticipanteSorteio>> GetAll()
        {
            var sql = "SELECT * FROM ParticipanteSorteio";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<ParticipanteSorteio>(sql);
                return (IQueryable<ParticipanteSorteio>)result;
            }
        }

        public async Task<ParticipanteSorteio> GetByParticipante(int id)
        {
            var sql = "SELECT * FROM ParticipanteSorteio WHERE ParticipanteId = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<ParticipanteSorteio>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<ParticipanteSorteio> GetBySorteio(int id)
        {
            var sql = "SELECT * FROM ParticipanteSorteio WHERE SorteioId = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<ParticipanteSorteio>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<int> Update(ParticipanteSorteio entity)
        {
            var sql = "UPDATE Sorteio SET ParticipanteId = @ParticipanteId, SorteioId = @SorteioID" +
                "WHERE ";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}
