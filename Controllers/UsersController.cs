
using DatingApp.Data;
using DatingApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        return Ok(_context.Users.ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<AppUser> GetUser(int id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<AppUser> CreateUser(
        AppUser user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return CreatedAtAction(
            nameof(GetUser),
            new { id = user.Id },
            user
        );
    }
}
