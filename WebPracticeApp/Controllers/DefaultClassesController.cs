#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPracticeApp.Data;
using WebPracticeApp.Models;

namespace WebPracticeApp.Controllers
{
    public class DefaultClassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DefaultClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DefaultClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.DefaultClass.ToListAsync());
        }
        // GET: DefaultClasses/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        // PoST: DefaultClasses/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.DefaultClass.Where( j => j.MainText.Contains(SearchPhrase)).ToListAsync());
    }
        
        // GET: DefaultClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultClass = await _context.DefaultClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defaultClass == null)
            {
                return NotFound();
            }

            return View(defaultClass);
        }

        [Microsoft.AspNetCore.Authorization.Authorize]
        // GET: DefaultClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DefaultClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Microsoft.AspNetCore.Authorization.Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MainText,SecondaryText")] DefaultClass defaultClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(defaultClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(defaultClass);
        }

        // GET: DefaultClasses/Edit/5
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultClass = await _context.DefaultClass.FindAsync(id);
            if (defaultClass == null)
            {
                return NotFound();
            }
            return View(defaultClass);
        }

        // POST: DefaultClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Microsoft.AspNetCore.Authorization.Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MainText,SecondaryText")] DefaultClass defaultClass)
        {
            if (id != defaultClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(defaultClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefaultClassExists(defaultClass.Id))
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
            return View(defaultClass);
        }

        // GET: DefaultClasses/Delete/5
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defaultClass = await _context.DefaultClass
                .FirstOrDefaultAsync(m => m.Id == id);
            if (defaultClass == null)
            {
                return NotFound();
            }

            return View(defaultClass);
        }

        // POST: DefaultClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var defaultClass = await _context.DefaultClass.FindAsync(id);
            _context.DefaultClass.Remove(defaultClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DefaultClassExists(int id)
        {
            return _context.DefaultClass.Any(e => e.Id == id);
        }
    }
}
