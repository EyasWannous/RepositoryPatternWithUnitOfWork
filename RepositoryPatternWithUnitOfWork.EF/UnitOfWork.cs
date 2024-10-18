
using Microsoft.EntityFrameworkCore.Internal;
using RepositoryPatternWithUnitOfWork.Core;
using RepositoryPatternWithUnitOfWork.Core.IRepositories;
using RepositoryPatternWithUnitOfWork.Core.Models;
using RepositoryPatternWithUnitOfWork.EF.Repositories;

namespace RepositoryPatternWithUnitOfWork.EF;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public IBaseRepository<Author> Authors { get; } = new BaseRepository<Author>(context);

    public IBooksRepository Books { get; } = new BooksRepository(context);

    public int Complete() => _context.SaveChanges();

    public void Dispose() => _context.Dispose();
}
