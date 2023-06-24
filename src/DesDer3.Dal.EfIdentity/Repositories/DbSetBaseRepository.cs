using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DesDer3.Dal.EfIdentity;
using DesDer3.Dal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesDer3.Dal.EfIdentity.Repositories;
internal class DbSetBaseRepository<T>: IRepository<T> where T : class
{
    private readonly DesDer3DbContext _context;
    private readonly DbSet<T> _set;
    public DbSetBaseRepository(DesDer3DbContext context)
    {
        _context = context;
        _set = context.Set<T>();
    }

    public async virtual Task AddAsync(T model) => await _set.AddAsync(model);
    public async virtual Task DeleteAsync(T model) => _set.Remove(model);
    public async virtual Task<T?> FindByIdAsync(Guid id) => await _set.FindAsync(id);
    public async virtual Task<IEnumerable<T>> GetAllAsync() => await _set.ToArrayAsync();
    public async virtual Task UpdateAsync(T model)
    {
        _context.Attach(model).State = EntityState.Modified;
    }
    public async virtual Task SaveAsync() => await _context.SaveChangesAsync();


    #region IQuerable implementation
    public Type ElementType => ((IQueryable)_set).ElementType;
    public Expression Expression => ((IQueryable)_set).Expression;
    public IQueryProvider Provider => ((IQueryable)_set).Provider;
    public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)_set).GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_set).GetEnumerator();
    #endregion

    #region IDisposable implementation
    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
    #endregion

}
