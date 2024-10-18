using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUnitOfWork.Core.Models;

namespace RepositoryPatternWithUnitOfWork.EF;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }

}
