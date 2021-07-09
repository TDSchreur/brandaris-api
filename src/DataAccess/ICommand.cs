using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ICommand<TEntity>
        where TEntity : IEntity
    {
        void Add(params TEntity[] persons);

        Task<int> SaveChanges(CancellationToken token = default);
    }
}
