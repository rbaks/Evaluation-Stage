using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context = null;
        private readonly DbSet<T> table = null;
        public SqlRepository()
        {
            this._context = new AppDbContext();
            table = _context.Set<T>();
        }
        public SqlRepository(AppDbContext context)
        {
            this._context = context;
            table = _context.Set<T>();
        }
        public async Task DeleteAsync(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T> FindAsync(object id)
        {
            return await table.FindAsync(id);
        }

        public async Task InsertAsync(T obj)
        {
            await table.AddAsync(obj);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T obj)
        {
            await Task.Run(() =>
            {
                table.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;
            });
        }
    }
}
