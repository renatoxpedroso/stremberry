using Streamberry.Tools;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Streamberry.Data;
using Streamberry.Models;
using Microsoft.AspNetCore.Authentication;
using Streamberry.Repository.Interfaces;

namespace Streamberry.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        public IActionResult Index()
        {
            if (SessionManagement.GetSession(HttpContext) != null)
            {
                return RedirectToAction("Index", "Main");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Login(LoginModel login)
        {
            try
            {
                if (login == null)
                {
                    throw new ApplicationException("Body inválido!");
                }

                if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
                {
                    throw new ApplicationException("Usuário ou senha inválidos!");
                }

                //UsuarioModel usuario = _contexto.Usuario.AsNoTracking().FirstOrDefault(x => x.Email == login.UserName && x.Senha == login.Password);

                UsuarioModel usuario = _usuarioRepository.BuscarUsuarioLogin(login.Email, login.Password);

                if (usuario == null)
                {
                    throw new ApplicationException("Usuário ou senha inválidos!");
                }

                SessionManagement.CreateSession(HttpContext, usuario);

                return new JsonResult(new { success = true, msg = string.Empty });
            }
            catch (ApplicationException ex)
            {
                return new JsonResult(new { success = false, msg = ex.Message });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, msg = ex.Message });
            }
        }

        public IActionResult Logoff()
        {
            SessionManagement.DeleteSession(HttpContext);
            return RedirectToAction("Index", "Login");
        }
    }
}
