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
    public class FilmeController : Controller
    {
        private readonly IFilmeRepository _filmeRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IStreamingRepository _streamingRepository;
        private readonly IStreamingFilmeRepository _streamingFilmeRepository;

        public FilmeController(IFilmeRepository filmeRepository, IGeneroRepository generoRepository, IStreamingRepository streamingRepository, IStreamingFilmeRepository streamingFilmeRepository)
        {
            _filmeRepository = filmeRepository;
            _generoRepository = generoRepository;
            _streamingRepository = streamingRepository;
            _streamingFilmeRepository = streamingFilmeRepository;
        }

        [CustomAuthorizationFilter]
        public async Task<IActionResult> Index()
        {
            ViewData["Genero"] = new SelectList(await _generoRepository.BuscarTodos(), "Id", "Nome");
            return View();
        }

        [CustomAuthorizationFilter]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            FilmeModel filme = await _filmeRepository.BuscarPorId(id);
            ViewData["Genero"] = new SelectList(await _generoRepository.BuscarTodos(), "Id", "Nome");
            return View(filme);
        }

        [CustomAuthorizationFilter]
        public async Task<IActionResult> Cadastro()
        {
            ViewData["Genero"] = new SelectList(await _generoRepository.BuscarTodos(), "Id", "Nome");
            ViewData["Streming"] = new SelectList(await _streamingRepository.BuscarTodos(), "Id", "Nome");

            return View(new FilmeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GravarComentario(ClassificacaoModel classificacao)
        {

            return View(new FilmeModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gravar(FilmeModel filmeModel, Guid[] streamingIds)
        {
            if (ModelState.IsValid)
            {
                if (filmeModel.Id == Guid.Empty)
                {
                    filmeModel.Id = Guid.NewGuid();
                    filmeModel.Genero = await _generoRepository.BuscarPorId(filmeModel.IdGenero);
                    await _filmeRepository.Adicionar(filmeModel);

                    foreach (Guid id in streamingIds)
                    {
                        StreamingFilmeModel streamingFilme = new StreamingFilmeModel()
                        {
                            Id = Guid.NewGuid(),
                            Filme = filmeModel.Id,
                            Streaming = id
                        };

                        await _streamingFilmeRepository.Adicionar(streamingFilme);
                    }

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    try
                    {
                        filmeModel.Genero = await _generoRepository.BuscarPorId(filmeModel.IdGenero);

                        await _streamingFilmeRepository.Apagar(filmeModel.Id);

                        foreach (Guid id in streamingIds)
                        {
                            StreamingFilmeModel streamingFilme = new StreamingFilmeModel()
                            {
                                Id = Guid.NewGuid(),
                                Filme = filmeModel.Id,
                                Streaming = id
                            };

                            await _streamingFilmeRepository.Adicionar(streamingFilme);
                        }

                        await _filmeRepository.Atualizar(filmeModel, filmeModel.Id);
                    }
                    catch (Exception ex)
                    {
                        Problem("Erro ao atualizar o filme " + ex.Message);
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(filmeModel);
        }

        [CustomAuthorizationFilter]
        public async Task<IActionResult> Editar(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            FilmeModel filmeModel = await _filmeRepository.BuscarPorId(id);
            ViewData["Genero"] = new SelectList(await _generoRepository.BuscarTodos(), "Id", "Nome");
            ViewData["Streming"] = new SelectList(await _streamingRepository.BuscarTodos(), "Id", "Nome");

            ViewData["Vinculos"] = await _streamingFilmeRepository.BuscarPorIdFilme(filmeModel.Id);

            //if (vinculos.Count > 0)
            //{
            //    ViewData["Streming"] = vinculos;
            //}
            //else
            //{
            //    ViewData["Streming"] = new SelectList(await _streamingRepository.BuscarTodos(), "Id", "Nome");
            //}
            

            return View("Cadastro", filmeModel);
        }

        [CustomAuthorizationFilter]
        public async Task<IActionResult> Deletar(string[] ids)
        {
            try
            {
                List<Guid> list = new List<Guid>();
                for (int i = 0; i < ids.Length; i++)
                {
                    await _filmeRepository.Apagar(Guid.Parse(ids[i]));
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
        public ActionResult Listar(FiltroFilmeModel model, int draw)
        {
            try
            {
                List<FilmeModel> filmes = _filmeRepository.BuscaPorParametros(model);
                var contador = _filmeRepository.ContarRegistroFiltroAsync(model);
                return new JsonResult(new { success = true, recordsFiltered = contador, recordsTotal = contador, data = filmes, draw = draw });
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
