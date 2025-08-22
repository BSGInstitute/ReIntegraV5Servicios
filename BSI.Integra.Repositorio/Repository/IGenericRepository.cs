using BSI.Integra.Aplicacion.Base;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseIntegraEntity
    {
        bool Insert(TEntity entidad);
        bool Update(TEntity entidad);
        bool Delete(int id, string usuario);

        bool Insert(IEnumerable<TEntity> list);
        bool Update(IEnumerable<TEntity> list);
        bool Delete(IEnumerable<int> list, string usuario);

        bool Exist(int id);
        bool Exist(Expression<Func<TEntity, bool>> filter);

        TEntity FirstById(int id);
        TEntity FirstBy(Expression<Func<TEntity, bool>> filter);
        TType FirstBy<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)
                where TType : class;
        TType FirstByEstadoFalse<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)

            where TType : class;

        IEnumerable<TEntity> GetBy(Expression<Func<TEntity, bool>> filter);
        IQueryable<TEntity> GetByQuery(Expression<Func<TEntity, bool>> filter);
        ICollection<TType> GetBy<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)
            where TType : class;
        IQueryable<TType> GetByQuery<TType>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TType>> select)
            where TType : class;
        IQueryable<TEntity> GetAll();
        IEnumerable<TEntity> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEntity, bool>>> filters,
            Expression<Func<TEntity, KProperty>> orderBy, bool ascending);

        IQueryable<TEntity> GetFilteredQuery<KProperty>(IEnumerable<Expression<Func<TEntity, bool>>> filters,
            Expression<Func<TEntity, KProperty>> orderBy, bool ascending);

        decimal GetMaxDecimal(Func<TEntity, decimal> columnSelector);
        int GetMaxInt(Func<TEntity, int> columnSelector);
    }
}
