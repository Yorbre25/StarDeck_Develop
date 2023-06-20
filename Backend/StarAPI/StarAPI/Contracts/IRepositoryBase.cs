using System.Linq.Expressions;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        T Get(int id); 
        T Get(string id); 
        List<T> GetAll();
        List<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Add(List<T> entities);
        void Delete(T entity);
        void Delete(List<T> entity);

    }
}