
using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUnitOfWork.Core.IRepositories;
using RepositoryPatternWithUnitOfWork.EF.Repositories;
using RepositoryPatternWithUnitOfWork.EF;
using Microsoft.Extensions.DependencyInjection;
using RepositoryPatternWithUnitOfWork.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlBuilder => sqlBuilder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
    )
);

//builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();

var app = builder.Build();  

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
