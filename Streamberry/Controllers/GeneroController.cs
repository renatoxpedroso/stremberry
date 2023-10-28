using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using Streamberry.Data;
using Streamberry.Models;
using Streamberry.Models.Filtros;
using Streamberry.Repository;
using Streamberry.Repository.Interfaces;
using Streamberry.Tools;

namespace Streamberry.Controllers
{
    public class GeneroController : Controller
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<IActionResult> Index()
        {
            List<GeneroModel> generos = await _generoRepository.BuscarTodos();

            return View(generos);
        }

        public IActionResult Cadastro()
        {
            return View(new GeneroModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gravar([Bind("Id,Nome")] GeneroModel generoModel)
        {
            if (ModelState.IsValid)
            {
                if (generoModel.Id == Guid.Empty)
                {
                    generoModel.Id = Guid.NewGuid();
                    await _generoRepository.Adicionar(generoModel);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    try
                    {
                        await _generoRepository.Atualizar(generoModel, generoModel.Id);
                    }
                    catch (Exception ex)
                    {
                        Problem("Erro ao atualizar o gênero " + ex.Message);
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(generoModel);
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var generoModel = await _generoRepository.BuscarPorId(id);
            if (generoModel == null)
            {
                return NotFound();
            }
            return View("Cadastro", generoModel);
        }

        public async Task<IActionResult> Deletar(string[] ids)
        {
            try
            {
                List<Guid> list = new List<Guid>();
                for (int i = 0; i < ids.Length; i++)
                {
                    await _generoRepository.Apagar(Guid.Parse(ids[i]));
                }

                return new JsonResult(new { success = true });
            }
            catch (ApplicationException ex)
            {
                return new JsonResult(new { success = false, error = ex.Message });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        [CustomAuthorizationFilter]
        public ActionResult Listar(FiltroGeneroModel model, int draw)
        {
            try
            {
                List<GeneroModel> genros = _generoRepository.BuscaPorParametros(model);
                var contador = _generoRepository.ContarRegistroFiltro(model);
                return new JsonResult(new { success = true, recordsFiltered = contador, recordsTotal = contador, data = genros, draw = draw });
            }
            catch (ApplicationException ex)
            {
                return new JsonResult(new { success = false, recordsFiltered = 0, recordsTotal = 0, data = "", draw = draw, error = ex.Message });
            }
            catch (Exception ex)
            {

                return new JsonResult(new { success = false, recordsFiltered = 0, recordsTotal = 0, data = "", draw = draw, error = ex.Message });
            }
        }
    }
}
