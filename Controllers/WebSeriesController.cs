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
    public class WebSeriesController : Controller
    {
        private readonly MovieReviewContext _context;

        public WebSeriesController(MovieReviewContext context)
        {
            _context = context;
        }

        // GET: WebSeries
        public async Task<IActionResult> Index()
        {
            var movieReviewContext = _context.WebSeries.Include(w => w.ContentType).Include(w => w.Genre).Include(w => w.Lang);
            return View(await movieReviewContext.ToListAsync());
        }

        // GET: WebSeries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WebSeries == null)
            {
                return NotFound();
            }

            var webSeries = await _context.WebSeries
                .Include(w => w.ContentType)
                .Include(w => w.Genre)
                .Include(w => w.Lang)
                .FirstOrDefaultAsync(m => m.WebSeriesId == id);
            if (webSeries == null)
            {
                return NotFound();
            }

            return View(webSeries);
        }

        // GET: WebSeries/Create
        public IActionResult Create()
        {
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId");
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId");
            return View();
        }

        // POST: WebSeries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WebSeriesId,GenreId,LangId,ContentTypeId,WebSeriesName,ReleaseYear,NoOfSeasons,NoOfEpisodes")] WebSeries webSeries)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webSeries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", webSeries.ContentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", webSeries.GenreId);
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId", webSeries.LangId);
            return View(webSeries);
        }

        // GET: WebSeries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WebSeries == null)
            {
                return NotFound();
            }

            var webSeries = await _context.WebSeries.FindAsync(id);
            if (webSeries == null)
            {
                return NotFound();
            }
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", webSeries.ContentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", webSeries.GenreId);
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId", webSeries.LangId);
            return View(webSeries);
        }

        // POST: WebSeries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WebSeriesId,GenreId,LangId,ContentTypeId,WebSeriesName,ReleaseYear,NoOfSeasons,NoOfEpisodes")] WebSeries webSeries)
        {
            if (id != webSeries.WebSeriesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webSeries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebSeriesExists(webSeries.WebSeriesId))
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
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", webSeries.ContentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", webSeries.GenreId);
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId", webSeries.LangId);
            return View(webSeries);
        }

        // GET: WebSeries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WebSeries == null)
            {
                return NotFound();
            }

            var webSeries = await _context.WebSeries
                .Include(w => w.ContentType)
                .Include(w => w.Genre)
                .Include(w => w.Lang)
                .FirstOrDefaultAsync(m => m.WebSeriesId == id);
            if (webSeries == null)
            {
                return NotFound();
            }

            return View(webSeries);
        }

        // POST: WebSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WebSeries == null)
            {
                return Problem("Entity set 'MovieReviewContext.WebSeries'  is null.");
            }
            var webSeries = await _context.WebSeries.FindAsync(id);
            if (webSeries != null)
            {
                _context.WebSeries.Remove(webSeries);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebSeriesExists(int id)
        {
          return (_context.WebSeries?.Any(e => e.WebSeriesId == id)).GetValueOrDefault();
        }
    }
}
