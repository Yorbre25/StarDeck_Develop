using Contracts;
using Microsoft.EntityFrameworkCore;
using StarAPI.Context;
using System.Linq.Expressions;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected StarDeckContext RepositoryContext { get; set; } 
        public RepositoryBase(StarDeckContext repositoryContext) 
        {
            RepositoryContext = repositoryContext; 
        }
        public List<T> GetAll() => RepositoryContext.Set<T>().ToList();
        public List<T> FindByCondition(Expression<Func<T, bool>> expression) => 
            RepositoryContext.Set<T>().Where(expression).ToList();
        public void Add(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Add(List<T> entities) => RepositoryContext.Set<T>().AddRange(entities);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
        public void Delete(List<T> entity) => RepositoryContext.Set<T>().RemoveRange(entity);

        public T Get(int id)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            Expression property = Expression.Property(parameter, "id");
            ConstantExpression idValue = Expression.Constant(id, typeof(int));
            Expression condition = Expression.Equal(property, idValue);
            Expression<Func<T, bool>> lambdaExpression = Expression.Lambda<Func<T, bool>>(condition, parameter);
            return RepositoryContext.Set<T>().FirstOrDefault(lambdaExpression);
        }
        public T Get(string id)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
            Expression property = Expression.Property(parameter, "id");
            ConstantExpression idValue = Expression.Constant(id, typeof(string));
            Expression condition = Expression.Equal(property, idValue);
            Expression<Func<T, bool>> lambdaExpression = Expression.Lambda<Func<T, bool>>(condition, parameter);
            return RepositoryContext.Set<T>().FirstOrDefault(lambdaExpression);
        }

    }
}