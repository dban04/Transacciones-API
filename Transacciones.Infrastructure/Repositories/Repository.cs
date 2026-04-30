using Microsoft.EntityFrameworkCore.Storage;
using Transacciones.Infrastructure.Data;

namespace Transacciones.Infrastructure.Repositories;

public class Repository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<T> GetByIdAsync(int id) => await _context.Set<T>().FindAsync(id);
    public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
    public void Update(T entity) => _context.Set<T>().Update(entity);
    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    public async Task<IDbContextTransaction> BeginTransactionAsync() 
            => await _context.Database.BeginTransactionAsync();

}
