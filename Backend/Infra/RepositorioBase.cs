using System.Linq.Expressions;
using Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        private readonly DbContext _context;
        private DbSet<T> _dbSet;

        public RepositorioBase( BibliotecaContexto context )
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> BuscarTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> BuscarPorCodigoAsync( object id )
        {
            return await _dbSet.FindAsync( id );
        }

        public async Task<IEnumerable<T>> EncontrarAsync( Expression<Func<T, bool>> filter )
        {
            return await _dbSet.Where( filter ).ToListAsync();
        }

        public async Task<T> AdicionarAsync( T entity )
        {
            await _dbSet.AddAsync( entity );
            return entity;
        }

        public T Atualizar( T entity )
        {
            _dbSet.Update( entity );
            return entity;
        }

        public async Task ExcluirAsync( object id )
        {
            var entity = await _dbSet.FindAsync( id );
            if( entity != null )
            {
                _dbSet.Remove( entity );
            }
        }
        public IQueryable<T> GetQueryable() => _dbSet.AsQueryable<T>();

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task<int> SalvarAlteracoesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
