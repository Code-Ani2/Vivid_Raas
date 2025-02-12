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
    public class ContentTypesController : Controller
    {
        private readonly MovieReviewContext _context;

        public ContentTypesController(MovieReviewContext context)
        {
            _context = context;
        }

        // GET: ContentTypes
        public async Task<IActionResult> Index()
        {
              return _context.ContentTypes != null ? 
                          View(await _context.ContentTypes.ToListAsync()) :
                          Problem("Entity set 'MovieReviewContext.ContentTypes'  is null.");
        }

        // GET: ContentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ContentTypes == null)
            {
                return NotFound();
            }

            var contentType = await _context.ContentTypes
                .FirstOrDefaultAsync(m => m.ContentTypeId == id);
            if (contentType == null)
            {
                return NotFound();
            }

            return View(contentType);
        }

        // GET: ContentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentTypeId,ContentName")] ContentType contentType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contentType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contentType);
        }

        // GET: ContentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ContentTypes == null)
            {
                return NotFound();
            }

            var contentType = await _context.ContentTypes.FindAsync(id);
            if (contentType == null)
            {
                return NotFound();
            }
            return View(contentType);
        }

        // POST: ContentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentTypeId,ContentName")] ContentType contentType)
        {
            if (id != contentType.ContentTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentTypeExists(contentType.ContentTypeId))
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
            return View(contentType);
        }

        // GET: ContentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ContentTypes == null)
            {
                return NotFound();
            }

            var contentType = await _context.ContentTypes
                .FirstOrDefaultAsync(m => m.ContentTypeId == id);
            if (contentType == null)
            {
                return NotFound();
            }

            return View(contentType);
        }

        // POST: ContentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ContentTypes == null)
            {
                return Problem("Entity set 'MovieReviewContext.ContentTypes'  is null.");
            }
            var contentType = await _context.ContentTypes.FindAsync(id);
            if (contentType != null)
            {
                _context.ContentTypes.Remove(contentType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentTypeExists(int id)
        {
          return (_context.ContentTypes?.Any(e => e.ContentTypeId == id)).GetValueOrDefault();
        }
    }
}
