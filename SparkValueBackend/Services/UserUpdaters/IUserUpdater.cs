using SparkValueBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Services.UserUpdaters
{
    public interface IUserUpdater
    {
        public Task UpdateUsersUsername(UserAccountModel user);

        public Task UpdateUsersPassword(UserAccountModel user);

        public Task UpdateUsersEmailAddress(UserAccountModel user);
    }
}
