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
    public class UserController : Controller
    {
        private readonly VodStreamingDataContext _context;

        public UserController(VodStreamingDataContext context)
        {
            _context = context;
        }

        // GET: AspNetUsers
        public async Task<IActionResult> Index()
        {
              return View(await _context.AspNetUsers.ToListAsync());
        }

        // GET: AspNetUsers/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var vodStreamingUsers = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vodStreamingUsers == null)
            {
                return NotFound();
            }

            return View(vodStreamingUsers);
        }

        // GET: AspNetUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AspNetUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VodStreamingUsers vodStreamingUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vodStreamingUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vodStreamingUsers);
        }

        // GET: AspNetUsers/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var vodStreamingUsers = await _context.AspNetUsers.FindAsync(id);
            if (vodStreamingUsers == null)
            {
                return NotFound();
            }
            return View(vodStreamingUsers);
        }

        // POST: AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,  VodStreamingUsers vodStreamingUsers)
        {
            if (id != vodStreamingUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vodStreamingUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VodStreamingUsersExists(vodStreamingUsers.Id))
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
            return View(vodStreamingUsers);
        }

        // GET: AspNetUsers/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var vodStreamingUsers = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vodStreamingUsers == null)
            {
                return NotFound();
            }

            return View(vodStreamingUsers);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AspNetUsers == null)
            {
                return Problem("Entity set 'VodStreamingDataContext.AspNetUsers'  is null.");
            }
            var vodStreamingUsers = await _context.AspNetUsers.FindAsync(id);
            if (vodStreamingUsers != null)
            {
                _context.AspNetUsers.Remove(vodStreamingUsers);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VodStreamingUsersExists(string id)
        {
          return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
