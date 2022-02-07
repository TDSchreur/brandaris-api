﻿using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Brandaris.Data;
using Microsoft.EntityFrameworkCore;

namespace Brandaris.DataAccess;

public class Command<TEntity> : ICommand<TEntity>
    where TEntity : class, IEntity
{
    private readonly DataContext _dataContext;
    private readonly DbSet<TEntity> _entities;

    public Command(DataContext dataContext)
    {
        _dataContext = dataContext;
        _entities = dataContext.Set<TEntity>();
    }

    public void Add(params TEntity[] entity) => _entities.AddRange(entity);

    public void Remove(TEntity entity) => _entities.Remove(entity);

    public Task<int> SaveChangesAsync(CancellationToken token = default) => _dataContext.SaveChangesAsync(token);

    public void Update<TProperty>(TEntity entity, params Expression<Func<TEntity, TProperty>>[] propertyExpressions)
    {
        _entities.Attach(entity);

        foreach (Expression<Func<TEntity, TProperty>> propertyExpression in propertyExpressions)
        {
            _dataContext.Entry(entity).Property(propertyExpression).IsModified = true;
        }
    }
}
