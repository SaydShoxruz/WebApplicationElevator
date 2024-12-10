﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplicationElevator.Context;
using WebApplicationElevator.Models;

namespace WebApplicationElevator.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private ElevatorRequest elevatorRequest = new ElevatorRequest();
    private readonly AppDbContext _dbContext;
    protected DbSet<TEntity> DbSet;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entry = await DbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        => await DbSet.FirstOrDefaultAsync(expression);

    public async Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity, bool>> expression)
    {
        if (expression == null)
            return DbSet;
        return DbSet.Where(expression);
    }
    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = DbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        try
        {
            DbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
