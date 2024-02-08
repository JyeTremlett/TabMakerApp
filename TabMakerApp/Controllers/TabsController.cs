using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TabMakerApp.Data;
using TabMakerApp.Models;

namespace TabMakerApp.Controllers
{
    public class TabsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TabsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tabs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tab.ToListAsync());
        }

        // GET: Tabs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tab = await _context.Tab
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
            return View();
        }

        // POST: Tabs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Author,TabContent,Description,Created,Updated")] Tab tab)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tab);
        }

        // GET: Tabs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tab = await _context.Tab.FindAsync(id);
            if (tab == null)
            {
                return NotFound();
            }
            return View(tab);
        }

        // POST: Tabs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tab = await _context.Tab
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tab == null)
            {
                return NotFound();
            }

            return View(tab);
        }

        // POST: Tabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tab = await _context.Tab.FindAsync(id);
            if (tab != null)
            {
                _context.Tab.Remove(tab);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TabExists(int id)
        {
            return _context.Tab.Any(e => e.Id == id);
        }
    }
}
