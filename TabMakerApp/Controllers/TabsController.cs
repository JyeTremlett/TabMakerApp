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
using Microsoft.IdentityModel.Tokens;
using System.Security.Policy;

namespace TabMakerApp.Controllers
{
    public class TabsController : Controller
    {

        public const string SessionKeyTabTitle = "_Title";
        public const string SessionKeyDescription = "_Description";
        public const string SessionKeyTabContent = "_TabContent";

        private readonly ApplicationDbContext _context;

        // userManager object for accessing current user id
        private readonly UserManager<ApplicationUser> _userManager;

        public TabsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Tabs
        public async Task<IActionResult> Index()
        {
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
        public IActionResult Create()
        {
            // if Session data for SessionKeyTabTitle is set, retrieve prepopulated fields
            if (!HttpContext.Session.GetString(SessionKeyTabTitle).IsNullOrEmpty())
            {
                Tab prepopulatedTab = new Tab()
                {
                    Name = HttpContext.Session.GetString(SessionKeyTabTitle),
                    Description = HttpContext.Session.GetString(SessionKeyDescription),
                    TabContent = HttpContext.Session.GetString(SessionKeyTabContent)
                };
                HttpContext.Session.SetString(SessionKeyTabTitle, ""); // reset sessiond data to empty after populating
                return View(prepopulatedTab);
            }
            return View();
        }

        // POST: Tabs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,TabContent,Description,Created,Updated")] Tab tab)
        {
            // if user is authenticated, save the tab data and redirect to login page
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                saveTabToSession(tab);
                return Redirect("/Identity/Account/Login?ReturnUrl=%2FTabs%2FCreate");
            }

            // get current user and set new tab's userId and author
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;

            tab.UserId = user.Id;
            tab.Author = user.UserName;

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
            /*saveTabToSession()*/
            RedirectToAction("Login", "Account");

            if (id != tab.Id)
            {
                return NotFound();
            }

            // get current user id
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            tab.UserId = user.Id;

            // remove modelstate UserId checking as it does not recorgnise tab.UserId
            ModelState.Remove("UserId");
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

        public void saveTabToSession(Tab tab)
        {
            HttpContext.Session.SetString(SessionKeyDescription, tab.Description);
            HttpContext.Session.SetString(SessionKeyTabContent, tab.TabContent);
            HttpContext.Session.SetString(SessionKeyTabTitle, tab.Name);
        }
    }
}
