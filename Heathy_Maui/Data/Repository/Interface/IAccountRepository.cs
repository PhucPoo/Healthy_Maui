using HealthManagement_MAUI.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heathy_Maui.Data.Interface
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByUsernameAsync(string Username);
        Task<Account?> GetAccountByIdAsync(int id);
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<bool> ValidateCredentialsAsync(string Username, string password);
        Task<List<Account>> GetAllAccountsAsync();
        Task<Account> CreateAccountAsync(Account account);
        Task<Account?> UpdateAccountAsync(Account account);
        Task<bool> DeleteAccountAsync(int id);
        Task<bool> AccountExistsAsync(string Accountname);
        Task<bool> EmailExistsAsync(string email);
        Task UpdateAsync(Account Account);
        Task<Account?> GetAccountByRefreshTokenAsync(string refreshToken);
    }
}
