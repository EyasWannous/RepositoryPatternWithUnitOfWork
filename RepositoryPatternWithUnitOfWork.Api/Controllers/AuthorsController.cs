using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternWithUnitOfWork.Core;
using RepositoryPatternWithUnitOfWork.Core.IRepositories;
using RepositoryPatternWithUnitOfWork.Core.Models;

namespace RepositoryPatternWithUnitOfWork.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController(IUnitOfWork unitOfWork) : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    
    [HttpGet]
    public IActionResult GetAll()
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var authors = _unitOfWork.Authors.GetAll();

        return Ok(authors);
    }


    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var author = _unitOfWork.Authors.GetById(id);
        if (author == null)
            return NotFound();

        return Ok(author);
    }


    [HttpGet("async/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var author = await _unitOfWork.Authors.GetByIdAsync(id);
        if (author == null)
            return NotFound();

        return Ok(author);
    }


    [HttpGet("{name}")]
    public IActionResult GetByName([FromRoute] string name)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var author = _unitOfWork.Authors.Find(author 
            => author.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)
        );
        if (author == null)
            return NotFound();

        return Ok(author);
    }
    

}
