using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VividRasV2.Models;

namespace VividRasV2.Controllers
{
    public class TelevisionDramasController : Controller
    {
        private readonly MovieReviewContext _context;

        public TelevisionDramasController(MovieReviewContext context)
        {
            _context = context;
        }

        // GET: TelevisionDramas
        public async Task<IActionResult> Index()
        {
            var movieReviewContext = _context.TelevisionDramas.Include(t => t.ContentType).Include(t => t.Genre).Include(t => t.Lang);
            return View(await movieReviewContext.ToListAsync());
        }

        // GET: TelevisionDramas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TelevisionDramas == null)
            {
                return NotFound();
            }

            var televisionDrama = await _context.TelevisionDramas
                .Include(t => t.ContentType)
                .Include(t => t.Genre)
                .Include(t => t.Lang)
                .FirstOrDefaultAsync(m => m.TvdramasId == id);
            if (televisionDrama == null)
            {
                return NotFound();
            }

            return View(televisionDrama);
        }

        // GET: TelevisionDramas/Create
        public IActionResult Create()
        {
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId");
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId");
            return View();
        }

        // POST: TelevisionDramas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TvdramasId,GenreId,LangId,ContentTypeId,DramasName,StartingYear")] TelevisionDrama televisionDrama)
        {
            if (ModelState.IsValid)
            {
                _context.Add(televisionDrama);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", televisionDrama.ContentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", televisionDrama.GenreId);
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId", televisionDrama.LangId);
            return View(televisionDrama);
        }

        // GET: TelevisionDramas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TelevisionDramas == null)
            {
                return NotFound();
            }

            var televisionDrama = await _context.TelevisionDramas.FindAsync(id);
            if (televisionDrama == null)
            {
                return NotFound();
            }
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", televisionDrama.ContentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", televisionDrama.GenreId);
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId", televisionDrama.LangId);
            return View(televisionDrama);
        }

        // POST: TelevisionDramas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TvdramasId,GenreId,LangId,ContentTypeId,DramasName,StartingYear")] TelevisionDrama televisionDrama)
        {
            if (id != televisionDrama.TvdramasId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(televisionDrama);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelevisionDramaExists(televisionDrama.TvdramasId))
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
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", televisionDrama.ContentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", televisionDrama.GenreId);
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId", televisionDrama.LangId);
            return View(televisionDrama);
        }

        // GET: TelevisionDramas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TelevisionDramas == null)
            {
                return NotFound();
            }

            var televisionDrama = await _context.TelevisionDramas
                .Include(t => t.ContentType)
                .Include(t => t.Genre)
                .Include(t => t.Lang)
                .FirstOrDefaultAsync(m => m.TvdramasId == id);
            if (televisionDrama == null)
            {
                return NotFound();
            }

            return View(televisionDrama);
        }

        // POST: TelevisionDramas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TelevisionDramas == null)
            {
                return Problem("Entity set 'MovieReviewContext.TelevisionDramas'  is null.");
            }
            var televisionDrama = await _context.TelevisionDramas.FindAsync(id);
            if (televisionDrama != null)
            {
                _context.TelevisionDramas.Remove(televisionDrama);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelevisionDramaExists(int id)
        {
          return (_context.TelevisionDramas?.Any(e => e.TvdramasId == id)).GetValueOrDefault();
        }
    }
}
