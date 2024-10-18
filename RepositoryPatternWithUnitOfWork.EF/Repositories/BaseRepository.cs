using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUnitOfWork.Core.Constants;
using RepositoryPatternWithUnitOfWork.Core.IRepositories;
using System.Linq.Expressions;

namespace RepositoryPatternWithUnitOfWork.EF.Repositories;

public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
{
    protected AppDbContext _context = context;

    public T Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return entity;
    }

    public IEnumerable<T> AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        return entities;
    }

    public void Attach(T entity)
        => _context.Set<T>().Attach(entity);

    public int Count()
        => _context.Set<T>().Count();

    public int Count(Expression<Func<T, bool>> criteria)
        => _context.Set<T>().Count(criteria);

    public void Delete(T entity)
        => _context.Set<T>().Remove(entity);

    public void DeleteRange(IEnumerable<T> entities)
        => _context.Set<T>().RemoveRange(entities);

    public T? Find(Expression<Func<T, bool>> criteria, string[]? includes = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach(var include in includes)
                query = query.Include(include);

        return query.SingleOrDefault(criteria);
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, string[]? includes = null)
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return query.Where(criteria).ToList();
    }

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int skip, int take)
        => _context.Set<T>().Where(criteria).Skip(skip).Take(take).ToList();

    public IEnumerable<T> FindAll(Expression<Func<T, bool>> criteria, int? skip, int? take, Expression<Func<T, object>>? orderBy = null, string OrderByDirection = "ASC")
    {
        IQueryable<T> query = _context.Set<T>().Where(criteria);
        
        if(skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        if (orderBy != null)
        {
            if(OrderByDirection == OrderBy.ASC)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);
        }

        return query.ToList();
    }

    public IEnumerable<T> GetAll()
        => _context.Set<T>().ToList();  
    

    public T? GetById(int id)
        => _context.Set<T>().Find(id);
    

    public async Task<T?> GetByIdAsync(int id)
        => await _context.Set<T>().FindAsync(id);

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        return entity;
    }
}
