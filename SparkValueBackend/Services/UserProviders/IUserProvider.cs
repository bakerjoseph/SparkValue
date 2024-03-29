﻿using SparkValueBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Services.UserProviders
{
    public interface IUserProvider
    {
        Task<IEnumerable<UserAccountModel>> GetAllUsers();

        Task<UserAccountModel> GetUserByUsername(string username);
    }
}
