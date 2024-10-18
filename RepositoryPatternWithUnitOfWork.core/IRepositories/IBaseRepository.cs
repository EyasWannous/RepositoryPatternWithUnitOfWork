
using RepositoryPatternWithUnitOfWork.Core.Constants;
using System.Linq.Expressions;

namespace RepositoryPatternWithUnitOfWork.Core.IRepositories;

public interface IBaseRepository<T> where T : class
{
    T? GetById(int id);
    Task<T?> GetByIdAsync(int id);
    IEnumerable<T> GetAll();
    T? Find(Expression<Func<T, bool>> criteria, string[]? includes = null);
    IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[]? includes = null);
    IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take);
    IEnumerable<T> FindAll(
        Expression<Func<T, bool>> criteria,
        int? skip,
        int? take,
        Expression<Func<T, object>>? orderBy = null,
        string OrderByDirection = OrderBy.ASC
    );
    T Add(T entity);
    IEnumerable<T> AddRange(IEnumerable<T> entities);
    T Update(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    void Attach(T entity);
    int Count();
    int Count(Expression<Func<T, bool>> criteria);
}
