﻿using Cloth.Domain.Entities;
using Persistence.Abstractions.Interfaces;

namespace Cloth.Application.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetUser(string password, string username);
}