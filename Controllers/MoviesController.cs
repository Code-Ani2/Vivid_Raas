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
    public class MoviesController : Controller
    {
        private readonly MovieReviewContext _context;

        public MoviesController(MovieReviewContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movieReviewContext = _context.Movies.Include(m => m.ContentType).Include(m => m.Genre).Include(m => m.Lang);
            return View(await movieReviewContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.ContentType)
                .Include(m => m.Genre)
                .Include(m => m.Lang)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId");
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId");
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,LangId,GenreId,ContentTypeId,MovieName,ReleaseYear,Duration")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", movie.ContentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId", movie.LangId);
            return View(movie);
        }

       // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", movie.ContentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId", movie.LangId);
            return View(movie);
        }

        //POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,LangId,GenreId,ContentTypeId,MovieName,ReleaseYear,Duration")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
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
            ViewData["ContentTypeId"] = new SelectList(_context.ContentTypes, "ContentTypeId", "ContentTypeId", movie.ContentTypeId);
            ViewData["GenreId"] = new SelectList(_context.Genres, "GenreId", "GenreId", movie.GenreId);
            ViewData["LangId"] = new SelectList(_context.Languages, "LangId", "LangId", movie.LangId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Movies == null)
        //    {
        //        return NotFound();
        //    }

        //    var movie = await _context.Movies
        //        .Include(m => m.ContentType)
        //        .Include(m => m.Genre)
        //        .Include(m => m.Lang)
        //        .FirstOrDefaultAsync(m => m.MovieId == id);
        //    if (movie == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(movie);
        //}

        //// POST: Movies/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Movies == null)
        //    {
        //        return Problem("Entity set 'MovieReviewContext.Movies'  is null.");
        //    }
        //    var movie = await _context.Movies.FindAsync(id);
        //    if (movie != null)
        //    {
        //        _context.Movies.Remove(movie);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool MovieExists(int id)
        {
          return (_context.Movies?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}
