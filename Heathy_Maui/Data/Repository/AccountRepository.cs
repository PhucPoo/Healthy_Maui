
using HealthManagement_MAUI.Models.Entities;
using Heathy_Maui.Data;
using Heathy_Maui.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HealthManagement_MAUI.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(ILogger<AccountRepository> logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task UpdateAsync(Account Account)
        {
            _context.Accounts.Update(Account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account?> GetAccountByUsernameAsync(string Username)
        {
            try
            {
                return await _context.Accounts
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Username == Username);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            try
            {
                return await _context.Accounts
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Id == id && u.isDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            try
            {
                return await _context.Accounts
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Email == email && u.isDeleted == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ValidateCredentialsAsync(string Username, string password)
        {
            try
            {
                var Account = await GetAccountByUsernameAsync(Username);
                if (Account == null) return false;
                return BCrypt.Net.BCrypt.Verify(password, Account.Password);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Account>> GetAllAccountsAsync()
        {
            try
            {
                var Accounts = await _context.Accounts
                    .Include(u => u.Role)
                    .Where(u => !u.isDeleted)
                    .ToListAsync();
                _logger.LogInformation("All Accounts count: {Count}", Accounts.Count);
                foreach (var u in Accounts)
                {
                    _logger.LogInformation("Account: {Username}, Password Hash: {Hash}", u.Username, u.Password);
                }
                return Accounts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> CreateAccountAsync(Account Account)
        {
            try
            {
                Account.Password = BCrypt.Net.BCrypt.HashPassword(Account.Password);
                Account.CreatedAt = DateTime.Now;
                Account.isDeleted = true;

                _context.Accounts.Add(Account);
                await _context.SaveChangesAsync();

                return Account;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account?> UpdateAccountAsync(Account Account)
        {
            try
            {
                var existingAccount = await _context.Accounts.FindAsync(Account.Id);
                if (existingAccount == null) return null;

                existingAccount.Email = Account.Email;
                existingAccount.RoleId = Account.RoleId;
                existingAccount.UpdatedAt = DateTime.Now;

                if (!string.IsNullOrEmpty(Account.Password) && !BCrypt.Net.BCrypt.Verify(Account.Password, existingAccount.Password))
                {
                    existingAccount.Password = BCrypt.Net.BCrypt.HashPassword(Account.Password);
                }

                await _context.SaveChangesAsync();
                return existingAccount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            try
            {
                var Account = await _context.Accounts.FindAsync(id);
                if (Account == null) return false;

                Account.isDeleted = false;
                Account.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AccountExistsAsync(string Username)
        {
            try
            {
                return await _context.Accounts.AnyAsync(u => u.Username == Username);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            try
            {
                return await _context.Accounts.AnyAsync(u => u.Email == email);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Account?> GetAccountByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
        public async Task<Account> RegisterAsync(Account account)
        {
            try
            {
                if (await AccountExistsAsync(account.Username))
                    throw new Exception("Username already exists");

                if (await EmailExistsAsync(account.Email))
                    throw new Exception("Email already exists");

                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);

                account.CreatedAt = DateTime.Now;
                account.isDeleted = false;

               
                account.RoleId = 2;

                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();

                _logger.LogInformation("New account registered: {Username}", account.Username);

                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while registering account");
                throw;
            }
        }

    }
}
