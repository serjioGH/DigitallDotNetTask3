﻿namespace Cloth.Persistence.Ef.Repositories;

using Cloth.Application.Interfaces.Repositories;
using Cloth.Domain.Entities;
using Cloth.Domain.Exceptions;
using Cloth.Persistence.Ef.Context;
using global::Persistence.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class ClothRepository : GenericRepository<Cloth>, IClothRepository
{
    protected readonly ClothInventoryDbContext _dbContext;

    public ClothRepository(ClothInventoryDbContext dbContext, ILogger<ClothRepository> logger) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Cloth>> GetAllCloths()
    {
        try
        {
            var result = await _dbContext.Cloths
                 .Include(p => p.Brand)
                 .Include(p => p.ClothSizes).ThenInclude(cs => cs.Size)
                 .Include(p => p.ClothGroups).ThenInclude(cg => cg.Group)
                 .Where(p => p.IsDeleted == false)
                 .ToListAsync();

            return result;
        }
        catch (ArgumentNullException ex)
        {
            throw new ItemNotFoundException($"Retrieving all cloths resulted in an error.", ex);
        }
        catch (Exception)
        {
            throw new ItemNotFoundException($"Error retrieving all items.");
        }
    }

    public async Task<Cloth> GetClothById(Guid clothId)
    {
        try
        {
            var result = await _dbContext.Cloths.Where(p => p.Id == clothId)
                .Include(p => p.Brand)
                .Include(p => p.ClothGroups).ThenInclude(cg => cg.Group)
                .Include(p => p.ClothSizes).ThenInclude(cs => cs.Size)
                .SingleAsync();
            return result;
        }
        catch (ArgumentNullException ex)
        {
            throw new ItemNotFoundException($"Retrieving Cloth resulted in an error.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new ItemNotFoundException($"Retrieving Cloth resulted in an error.", ex);
        }
        catch (Exception)
        {
            throw new ItemNotFoundException($"Retrieving Cloth resulted in an error.");
        }
    }
}