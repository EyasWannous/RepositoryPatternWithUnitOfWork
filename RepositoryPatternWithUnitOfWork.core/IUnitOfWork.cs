
using RepositoryPatternWithUnitOfWork.Core.IRepositories;
using RepositoryPatternWithUnitOfWork.Core.Models;

namespace RepositoryPatternWithUnitOfWork.Core;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Author> Authors { get; }
    IBooksRepository Books { get; }

    int Complete();

}
