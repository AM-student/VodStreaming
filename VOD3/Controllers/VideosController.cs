using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VodStreaming;
using VodStreaming.Areas.Identity.Data;
using VodStreaming.Models;

namespace VodStreaming.Controllers
{
    public class VideosController : Controller
    {
        private readonly VodStreamingDataContext _context;

        public VideosController(VodStreamingDataContext context)
        {
            _context = context;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            var VodStreamingDataContext = _context.videos.Include(v => v.Category);
            return View(await VodStreamingDataContext.ToListAsync());
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.videos == null)
            {
                return NotFound();
            }

            var video = await _context.videos
                .Include(v => v.Category)
                .FirstOrDefaultAsync(m => m.VideoID == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [DisableRequestSizeLimit]
        // GET: Videos/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.categories, "CategoryID", "CategoryID");
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [DisableRequestSizeLimit]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Video video, IFormFile formFile)
        {
            try
            {
                string fileName = formFile.FileName;
                fileName = Path.GetFileName(fileName);
                string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\vids", fileName);
                video.FilePath = Path.Combine("\\vids", fileName);
                var stream = new FileStream(uploadpath, FileMode.Create);
                formFile.CopyToAsync(stream);
                ViewBag.Message = "File uploaded successfully.";
                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.Message = "Error while uploading the files.";
            }
            ViewData["CategoryID"] = new SelectList(_context.categories, "CategoryID", "CategoryID");
            return View();

        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.videos == null)
            {
                return NotFound();
            }

            var video = await _context.videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.categories, "CategoryID", "CategoryID", video.CategoryID);
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoID,Name,ThumbnailPath,FilePath,CreatedAt,Description,CategoryID")] Video video)
        {
            if (id != video.VideoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(video);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.VideoID))
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
            ViewData["CategoryID"] = new SelectList(_context.categories, "CategoryID", "CategoryID", video.CategoryID);
            return View(video);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.videos == null)
            {
                return NotFound();
            }

            var video = await _context.videos
                .Include(v => v.Category)
                .FirstOrDefaultAsync(m => m.VideoID == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.videos == null)
            {
                return Problem("Entity set 'VodStreamingDataContext.videos'  is null.");
            }
            var video = await _context.videos.FindAsync(id);
            if (video != null)
            {
                _context.videos.Remove(video);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoExists(int id)
        {
          return _context.videos.Any(e => e.VideoID == id);
        }
    }
}
