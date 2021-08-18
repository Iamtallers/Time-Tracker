using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Time_Tracker.Models;

namespace Time_Tracker.Services.Account
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(string username, string password);

        Task<AuthenticatedUser> GetUserAsync();
    }
}
