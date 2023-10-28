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
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View(new UsuarioModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gravar([Bind("Id,Nome,Email,Senha,Perfil")] UsuarioModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                if (usuarioModel.Id == Guid.Empty)
                {
                    usuarioModel.Id = Guid.NewGuid();
                    await _usuarioRepository.Adicionar(usuarioModel);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    try
                    {
                        await _usuarioRepository.Atualizar(usuarioModel, usuarioModel.Id);
                    }
                    catch (Exception ex)
                    {
                        Problem("Erro ao atualizar o gênero " + ex.Message);
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(usuarioModel);
        }

        public async Task<IActionResult> Editar(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var usuarioModel = await _usuarioRepository.BuscarPorId(Guid.Parse(id));
            if (usuarioModel == null)
            {
                return NotFound();
            }
            return View("Cadastro", usuarioModel);
        }

       
        public async Task<IActionResult> Deletar(string[] ids)
        {
            try
            {
                List<Guid> list = new List<Guid>();
                for (int i = 0; i < ids.Length; i++)
                {
                    await _usuarioRepository.Apagar(Guid.Parse(ids[i]));
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
        public ActionResult Listar(FiltroUsuarioModel model, int draw)
        {
            try
            {
                List<UsuarioModel> usuarios = _usuarioRepository.BuscaPorParametros(model);
                var contador = _usuarioRepository.ContarRegistroFiltro(model);
                return new JsonResult(new { success = true, recordsFiltered = contador, recordsTotal = contador, data = usuarios, draw = draw });
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
