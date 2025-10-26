using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);  
        }

        public bool Exists(int d)
        {
            return _context.Set<T>().Any(x => x.Id == d);   
        }

        public async Task<T>? GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>>? ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }



        // specification
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await this.ApplySpecifications(spec).FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await this.ApplySpecifications(spec).ToListAsync();
        }


        // projection
        public async Task<TResult> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync(); // do i need any changes here
        }
        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec)
        {
            return await this.ApplySpecifications(spec).ToListAsync();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }


        private IQueryable<T> ApplySpecifications(ISpecification<T> specifications)
        {
            return SpecificatonEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specifications);
        }

        private IQueryable<TResult> ApplySpecifications<TResult>(ISpecification<T,TResult> specifications)
        {
            return SpecificatonEvaluator<T>.GetQuery<T,TResult>(_context.Set<T>().AsQueryable(), specifications);
        }
    }
}
