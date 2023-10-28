using Microsoft.AspNetCore.Mvc;
using Streamberry.Models;
using Streamberry.Models.Filtros;
using Streamberry.Repository;
using Streamberry.Repository.Interfaces;
using Streamberry.Tools;

namespace Streamberry.Controllers
{
    public class StreamingController : Controller
    {
        private readonly IStreamingRepository _streamingRepository;

        public StreamingController(IStreamingRepository streamingRepository)
        {
            _streamingRepository = streamingRepository;
        }

        public  IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastro()
        {
            return View(new StreamingModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Gravar([Bind("Id,Nome")] StreamingModel streamingModel)
        {
            if (ModelState.IsValid)
            {
                if (streamingModel.Id == Guid.Empty)
                {
                    streamingModel.Id = Guid.NewGuid();
                    await _streamingRepository.Adicionar(streamingModel);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    try
                    {
                        await _streamingRepository.Atualizar(streamingModel, streamingModel.Id);
                    }
                    catch (Exception ex)
                    {
                        Problem("Erro ao atualizar o gênero " + ex.Message);
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(streamingModel);
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var streamingModel = await _streamingRepository.BuscarPorId(id);
            if (streamingModel == null)
            {
                return NotFound();
            }
            return View("Cadastro", streamingModel);
        }

        [CustomAuthorizationFilter]
        public async Task<IActionResult> Deletar(string[] ids)
        {
            try
            {
                List<Guid> list = new List<Guid>();
                for (int i = 0; i < ids.Length; i++)
                {
                    await _streamingRepository.Apagar(Guid.Parse(ids[i]));
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
        public ActionResult Listar(FiltroStreamingModel model, int draw)
        {
            try
            {
                List<StreamingModel> streamings = _streamingRepository.BuscaPorParametros(model);
                var contador = _streamingRepository.ContarRegistroFiltro(model);
                return new JsonResult(new { success = true, recordsFiltered = contador, recordsTotal = contador, data = streamings, draw = draw });
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
