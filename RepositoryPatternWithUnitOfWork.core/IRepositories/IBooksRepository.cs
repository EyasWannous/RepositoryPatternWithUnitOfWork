using RepositoryPatternWithUnitOfWork.Core.Models;
using System.Linq.Expressions;

namespace RepositoryPatternWithUnitOfWork.Core.IRepositories;

public interface IBooksRepository : IBaseRepository<Book>
{
    IEnumerable<Book> SpecialMethod();
}
