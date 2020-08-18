using APISorteio.Data.Repositories.Interfaces;
using APISorteio.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APISorteio.Data.Repositories
{
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly string _connectionString;
        public ParticipanteRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> Add(Participante entity)
        {
            var sql = "INSERT INTO Participante(Nome,Sobrenome, CPF, Email, Telefone, Id_Endereco) " +
                "VALUES(@Nome, @Sobrenome,@CPF, @Email, @Telefone, @Id_Endereco)";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql,entity);
                return affectedRows;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Participante WHERE ParticipanteId = @Id";
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id= id});
                return affectedRows;
            }
        }

        public async Task<Participante> Get(int id)
        {
            var sql = "SELECT * FROM Participante WHERE Participante = @Id";
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Participante>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IQueryable<Participante>> GetAll()
        {
            var sql = "SELECT * FROM Participante";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Participante>(sql);
                return (IQueryable<Participante>)result;
            }
        }

        public async Task<int> Update(Participante entity)
        {
            var sql = "UPDATE Participante SET Nome = @Nome, Sobrenome = @Sobrenome," +
                "CPF = @CPF, Email = @Email, Telefone = @Telefone, Id_Endereco = @Id_Endereco";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}
