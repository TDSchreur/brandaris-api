using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Command<TEntity> : ICommand<TEntity>
        where TEntity : class, IEntity
    {
        private readonly DbContext _dataContext;
        private readonly DbSet<TEntity> _entities;

        public Command(DbContext dataContext)
        {
            _dataContext = dataContext;
            _entities = dataContext.Set<TEntity>();
        }

        public void Add(params TEntity[] persons) => _entities.AddRange(persons);

        public Task<int> SaveChangesAsync(CancellationToken token = default) => _dataContext.SaveChangesAsync(token);
    }
}
