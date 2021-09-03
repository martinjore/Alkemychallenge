using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alkemy.Models;
using Alkemy.Models.AppDBcontext;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Alkemy
{
    public class MoviesController : Controller
    {
        private readonly AppDBcontext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MoviesController(AppDBcontext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var appDBcontext = _context.Movies.Include(m => m.character).Include(m => m.genres);
            return View(await appDBcontext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .Include(m => m.character)
                .Include(m => m.genres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["PersAsocId"] = new SelectList(_context.Characters, "Id", "Id");
            ViewData["GeneroAsocId"] = new SelectList(_context.Genres, "Id", "Id");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imagen,ImagenFile,Titulo,Fecha_Creacion,Calificacion,PersAsocId,GeneroAsocId")] Movies movies)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(movies.ImagenFile.FileName);
                string extension = Path.GetExtension(movies.ImagenFile.FileName);
                movies.Imagen = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await movies.ImagenFile.CopyToAsync(fileStream);
                }
                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersAsocId"] = new SelectList(_context.Characters, "Id", "Id", movies.PersAsocId);
            ViewData["GeneroAsocId"] = new SelectList(_context.Genres, "Id", "Id", movies.GeneroAsocId);
            return View(movies);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }
            ViewData["PersAsocId"] = new SelectList(_context.Characters, "Id", "Id", movies.PersAsocId);
            ViewData["GeneroAsocId"] = new SelectList(_context.Genres, "Id", "Id", movies.GeneroAsocId);
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imagen,Titulo,Fecha_Creacion,Calificacion,PersAsocId,GeneroAsocId")] Movies movies)
        {
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
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
            ViewData["PersAsocId"] = new SelectList(_context.Characters, "Id", "Id", movies.PersAsocId);
            ViewData["GeneroAsocId"] = new SelectList(_context.Genres, "Id", "Id", movies.GeneroAsocId);
            return View(movies);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .Include(m => m.character)
                .Include(m => m.genres)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movies = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
