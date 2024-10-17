using System.Linq.Expressions;

namespace Core.Repositorios
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<IEnumerable<T>> BuscarTodosAsync();
        Task<T> BuscarPorCodigoAsync( object id );
        Task<IEnumerable<T>> EncontrarAsync( Expression<Func<T, bool>> filter );
        Task<T> AdicionarAsync( T entity );
        T Atualizar( T entity );
        Task ExcluirAsync( object id );
        Task<int> SalvarAlteracoesAsync();
        IQueryable<T> GetQueryable();
    }
}
