using APISorteio.Data.Repositories.Interfaces;
using APISorteio.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APISorteio.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly string _connectionString;
        public EnderecoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> Add(Endereco entity)
        {
            var sql = "INSERT INTO Endereco (Logradouro,Bairro,Cidade,Estado,Pais) " +
                "VALUES (@Logradouro, @Bairro, @Cidade, @Estado, @Pais)";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Endereco WHERE EnderecoId = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        public async Task<Endereco> Get(int id)
        {
            var sql = "SELECT * FROM Endereco WHERE EnderecoId = @Id";
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Endereco>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Endereco>> GetAll()
        {
            var sql = "SELECT * FROM Endereco";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Endereco>(sql);
                return result;
            }
        }

        public async Task<int> Update(Endereco entity)
        {
            var sql = "Update Endereco SET Logradouro = @Logradouro, Bairro = @Bairro, " +
                "Cidade = @Cidade, Estado = @Estado, Pais = @Pais" +
                "WHERE EnderecoId = @EnderecoId";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}
