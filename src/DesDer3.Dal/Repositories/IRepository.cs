using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesDer3.Dal.Repositories;
public interface IRepository<T>: IQueryable<T>, IDisposable where T : class
{
    Task<T?> FindByIdAsync(Guid id);
    Task AddAsync(T model);
    Task UpdateAsync(T model);
    Task DeleteAsync(T model);
    Task<IEnumerable<T>> GetAllAsync();
    Task SaveAsync();
}
