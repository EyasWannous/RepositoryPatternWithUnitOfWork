
using RepositoryPatternWithUnitOfWork.Core.IRepositories;
using RepositoryPatternWithUnitOfWork.Core.Models;

namespace RepositoryPatternWithUnitOfWork.EF.Repositories;

public class BooksRepository(AppDbContext context) : BaseRepository<Book>(context), IBooksRepository
{
    public IEnumerable<Book> SpecialMethod()
    {
        throw new NotImplementedException();
    }
}
