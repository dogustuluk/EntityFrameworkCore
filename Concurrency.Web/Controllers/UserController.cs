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

        public async Task<IActionResult> Update(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));
            }
            catch (DbUpdateConcurrencyException exception)
            {
                var exceptionEntry = exception.Entries.First();

                var currentUser = exceptionEntry.Entity as User;

                var databaseValue = exceptionEntry.GetDatabaseValues();

                var clientValues = exceptionEntry.CurrentValues;

                if (databaseValue == null)
                {
                    ModelState.AddModelError(string.Empty, "Bu kullanıcı başka bir admin tarafından silindi");
                }
                else
                {
                    var databaseUser = databaseValue.ToObject() as User;
                    ModelState.AddModelError(string.Empty, "Bu kullanıcı admin tarafından güncellenmiştir");
                    ModelState.AddModelError(string.Empty, $"Güncellenen Değer: Username:{databaseUser.Username}, Phone:{databaseUser.Phone}, Birthday:{databaseUser.Birthday}");
                }

                return View(user);
            }

            
        }
    }
}
