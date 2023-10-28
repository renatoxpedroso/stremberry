using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Streamberry.Data;
using Streamberry.Models;

namespace Streamberry.Controllers
{
    public class ClassificacaoController : Controller
    {
        private readonly StreamberryDbContexto _context;

        public ClassificacaoController(StreamberryDbContexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Classificacao == null)
            {
                return NotFound();
            }

            return View();
        }

        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comentario,Nota,IdUsuario")] ClassificacaoModel classificacao)
        {
            if (ModelState.IsValid)
            {
                classificacao.Id = Guid.NewGuid();
                _context.Add(classificacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(classificacao);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Classificacao == null)
            {
                return NotFound();
            }

            var classificacao = await _context.Classificacao.FindAsync(id);
            if (classificacao == null)
            {
                return NotFound();
            }

            return View(classificacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Comentario,Nota,IdUsuario")] ClassificacaoModel classificacao)
        {
            if (id != classificacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classificacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassificacaoExists(classificacao.Id))
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
            return View(classificacao);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Classificacao == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Classificacao == null)
            {
                return Problem("Entity set 'Contexto.Classificacao'  is null.");
            }
            var classificacao = await _context.Classificacao.FindAsync(id);
            if (classificacao != null)
            {
                _context.Classificacao.Remove(classificacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassificacaoExists(Guid id)
        {
          return (_context.Classificacao?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
