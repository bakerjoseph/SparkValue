using SparkValueDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Services.UserUpdaters
{
    public interface IUserUpdater
    {
        public Task UpdateUsersUsername(string currentUsername, string newUsername);

        public Task UpdateUsersPassword(string currentUsername, string newPassword);

        public Task UpdateUsersEmailAddress(string currentUsername, string newEmailAddress);
    }
}
