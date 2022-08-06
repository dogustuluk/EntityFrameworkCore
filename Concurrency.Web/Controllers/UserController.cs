using Concurrency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concurrency.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> List()
        {
            return View(await _context.Users.ToListAsync());
        }
    }
}
