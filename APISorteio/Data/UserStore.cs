using APISorteio.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APISorteio.Data
{
    public class UserStore : 
        IUserStore<Administrador>, 
        IUserEmailStore<Administrador>, 
        IUserPhoneNumberStore<Administrador>,
        IUserTwoFactorStore<Administrador>,
        IUserPasswordStore<Administrador> 
    {
        private readonly string _connectionString;
        public UserStore(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IdentityResult> CreateAsync(Administrador user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                user.Id = await connection.QuerySingleAsync<int>($@"INSERT INTO [ApplicationUser] ([UserName], [NormalizedUserName], [Email],
                    [NormalizedEmail], [EmailConfirmed], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled])
                    VALUES (@{nameof(Administrador.UserName)}, @{nameof(Administrador.NormalizedUserName)}, @{nameof(Administrador.Email)},
                    @{nameof(Administrador.NormalizedEmail)}, @{nameof(Administrador.EmailConfirmed)}, @{nameof(Administrador.PasswordHash)},
                    @{nameof(Administrador.PhoneNumber)}, @{nameof(Administrador.PhoneNumberConfirmed)}, @{nameof(Administrador.TwoFactorEnabled)});
                    SELECT CAST(SCOPE_IDENTITY() as int)", user);
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(Administrador user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($"DELETE FROM [ApplicationUser] WHERE [Id] = @{nameof(Administrador.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public async Task<Administrador> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<Administrador>($@"SELECT * FROM [ApplicationUser]
                    WHERE [Id] = @{nameof(userId)}", new { userId });
            }
        }

        public async Task<Administrador> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<Administrador>($@"SELECT * FROM [ApplicationUser]
                    WHERE [NormalizedUserName] = @{nameof(normalizedUserName)}", new { normalizedUserName });
            }
        }

        public Task<string> GetNormalizedUserNameAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(Administrador user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(Administrador user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(Administrador user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync($@"UPDATE [ApplicationUser] SET
                    [UserName] = @{nameof(Administrador.UserName)},
                    [NormalizedUserName] = @{nameof(Administrador.NormalizedUserName)},
                    [Email] = @{nameof(Administrador.Email)},
                    [NormalizedEmail] = @{nameof(Administrador.NormalizedEmail)},
                    [EmailConfirmed] = @{nameof(Administrador.EmailConfirmed)},
                    [PasswordHash] = @{nameof(Administrador.PasswordHash)},
                    [PhoneNumber] = @{nameof(Administrador.PhoneNumber)},
                    [PhoneNumberConfirmed] = @{nameof(Administrador.PhoneNumberConfirmed)},
                    [TwoFactorEnabled] = @{nameof(Administrador.TwoFactorEnabled)}
                    WHERE [Id] = @{nameof(Administrador.Id)}", user);
            }

            return IdentityResult.Success;
        }

        public Task SetEmailAsync(Administrador user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(Administrador user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<Administrador> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                return await connection.QuerySingleOrDefaultAsync<Administrador>($@"SELECT * FROM [ApplicationUser]
                    WHERE [NormalizedEmail] = @{nameof(normalizedEmail)}", new { normalizedEmail });
            }
        }

        public Task<string> GetNormalizedEmailAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(Administrador user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        public Task SetPhoneNumberAsync(Administrador user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(Administrador user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public Task SetTwoFactorEnabledAsync(Administrador user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPasswordHashAsync(Administrador user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Administrador user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public async Task AddToRoleAsync(Administrador user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var normalizedName = roleName.ToUpper();
                var roleId = await connection.ExecuteScalarAsync<int?>($"SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @{nameof(normalizedName)}", new { normalizedName });
                if (!roleId.HasValue)
                    roleId = await connection.ExecuteAsync($"INSERT INTO [ApplicationRole]([Name], [NormalizedName]) VALUES(@{nameof(roleName)}, @{nameof(normalizedName)})",
                        new { roleName, normalizedName });

                await connection.ExecuteAsync($"IF NOT EXISTS(SELECT 1 FROM [ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}) " +
                    $"INSERT INTO [ApplicationUserRole]([UserId], [RoleId]) VALUES(@userId, @{nameof(roleId)})",
                    new { userId = user.Id, roleId });
            }
        }

        public async Task RemoveFromRoleAsync(Administrador user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (!roleId.HasValue)
                    await connection.ExecuteAsync($"DELETE FROM [ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}", new { userId = user.Id, roleId });
            }
        }

        public async Task<IList<string>> GetRolesAsync(Administrador user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);
                var queryResults = await connection.QueryAsync<string>("SELECT r.[Name] FROM [ApplicationRole] r INNER JOIN [ApplicationUserRole] ur ON ur.[RoleId] = r.Id " +
                    "WHERE ur.UserId = @userId", new { userId = user.Id });

                return queryResults.ToList();
            }
        }

        public async Task<bool> IsInRoleAsync(Administrador user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var roleId = await connection.ExecuteScalarAsync<int?>("SELECT [Id] FROM [ApplicationRole] WHERE [NormalizedName] = @normalizedName", new { normalizedName = roleName.ToUpper() });
                if (roleId == default(int)) return false;
                var matchingRoles = await connection.ExecuteScalarAsync<int>($"SELECT COUNT(*) FROM [ApplicationUserRole] WHERE [UserId] = @userId AND [RoleId] = @{nameof(roleId)}",
                    new { userId = user.Id, roleId });

                return matchingRoles > 0;
            }
        }

        public async Task<IList<Administrador>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var connection = new SqlConnection(_connectionString))
            {
                var queryResults = await connection.QueryAsync<Administrador>("SELECT u.* FROM [ApplicationUser] u " +
                    "INNER JOIN [ApplicationUserRole] ur ON ur.[UserId] = u.[Id] INNER JOIN [ApplicationRole] r ON r.[Id] = ur.[RoleId] WHERE r.[NormalizedName] = @normalizedName",
                    new { normalizedName = roleName.ToUpper() });

                return queryResults.ToList();
            }
        }

        public void Dispose()
        {
            // Nothing to dispose.
        }

    }
}
