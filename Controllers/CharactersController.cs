using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Alkemy.Models;
using Alkemy.Models.AppDBcontext;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Alkemy
{
    public class CharactersController : Controller
    {
        private readonly AppDBcontext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CharactersController(AppDBcontext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Characters
        public async Task<IActionResult> Index()
        {
            return View(await _context.Characters.ToListAsync());
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Characters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImagenFile,Nombre,Edad,Peso,Historia")] Characters characters)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(characters.ImagenFile.FileName);
                string extension = Path.GetExtension(characters.ImagenFile.FileName);
                characters.Imagen = filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await characters.ImagenFile.CopyToAsync(fileStream);
                }
                _context.Add(characters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(characters);
        }

        // GET: Characters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters.FindAsync(id);
            if (characters == null)
            {
                return NotFound();
            }
            return View(characters);
        }

        // POST: Characters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imagen,Nombre,Edad,Peso,Historia")] Characters characters)
        {
            if (id != characters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharactersExists(characters.Id))
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
            return View(characters);
        }

        // GET: Characters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Characters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var characters = await _context.Characters.FindAsync(id);
            _context.Characters.Remove(characters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharactersExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
