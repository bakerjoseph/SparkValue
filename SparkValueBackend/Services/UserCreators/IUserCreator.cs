using SparkValueBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Services.UserCreators
{
    public interface IUserCreator
    {
        Task CreateUser(UserAccountModel user);
    }
}
