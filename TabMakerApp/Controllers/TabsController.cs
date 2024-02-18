using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TabMakerApp.Data;
using TabMakerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace TabMakerApp.Controllers
{
    public class TabsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; // userManager object for accessing current user id

        public TabsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Tabs
        public async Task<IActionResult> Index()
        {
            /*var currentUserId = _userManager.GetUserId(this.User);*/
    
            // get Tabs belonging to user matching current user's username
            var applicationDbContext = _context.Tabs.Include(p => p.ApplicationUser)
                                                .Where(a => a.ApplicationUser.UserName == User.Identity.Name);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Tabs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tab = await _context.Tabs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tab == null)
            {
                return NotFound();
            }

            return View(tab);
        }

        // GET: Tabs/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tabs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,TabContent,Description,Created,Updated")] Tab tab)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            tab.UserId = user.Id;

            // remove modelstate UserId checking as it does not recorgnise tab.UserId
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                _context.Add(tab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tab);
        }

        // GET: Tabs/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tab = await _context.Tabs.FindAsync(id);
            if (tab == null)
            {
                return NotFound();
            }
            return View(tab);
        }

        // POST: Tabs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author,TabContent,Description,Created,Updated")] Tab tab)
        {
            if (id != tab.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TabExists(tab.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tab);
        }

        // GET: Tabs/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tab = await _context.Tabs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tab == null)
            {
                return NotFound();
            }

            return View(tab);
        }

        // POST: Tabs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tab = await _context.Tabs.FindAsync(id);
            if (tab != null)
            {
                _context.Tabs.Remove(tab);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TabExists(int id)
        {
            return _context.Tabs.Any(e => e.Id == id);
        }
    }
}
