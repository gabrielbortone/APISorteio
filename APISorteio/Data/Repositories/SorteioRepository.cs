using APISorteio.Data.Repositories.Interfaces;
using APISorteio.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APISorteio.Data.Repositories
{
    public class SorteioRepository : ISorteioRepository
    {
        private readonly string _connectionString;
        public SorteioRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> Add(Sorteio entity)
        {
            var sql = "INSERT INTO Sorteio (Titulo,Descricao,Premio,NumGanhadores, " +
                "Id_Administrador, DataFinalizacaoCadastro, DataSorteio) VALUES" +
                "(@Titulo, @Descricao, @Premio, @NumGanhadores, @Id_Administrador, @DataFinalizacaoCadastro, @DataSorteio)";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public async Task<int> Delete(int id)
        {
            var sql = "DELETE FROM Sorteio WHERE SorteioId = @Id";
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });
                return affectedRows;
            }
        }

        public async Task<Sorteio> Get(int id)
        {
            var sql = "SELECT * FROM Sorteio WHERE SorteioId = @Id";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Sorteio>(sql, new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<Sorteio>> GetAll()
        {
            var sql = "SELECT * FROM Sorteio";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<Sorteio>(sql);
                return (IQueryable<Sorteio>)result;
            }
        }

        public async Task<int> Update(Sorteio entity)
        {
            var sql = "UPDATE Sorteio SET Titulo = @Titulo, Descricao = @Descricao, " +
                "Premio = @Premio, NumGanhadores = @NumGanhadores, Id_Administrador = @Id_Administrador, " +
                "DataFinalizacaoCadastro = @DataFinalizacaoCadastro, DataSorteio = @DataSorteio" +
                "WHERE SorteioId = @SorteioId";
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }
    }
}
