using Newtonsoft.Json;
using Streamberry.Models;

namespace Streamberry.Tools
{
	public static class SessionManagement
	{
		public static void DeleteSession(HttpContext context)
		{
			context.Session.Remove("Streamberry.Session");
			context.Session.Clear();
		}

		public static void CreateSession(HttpContext context, UsuarioModel usuario)
		{
			context.Session.SetString("Streamberry.Session", JsonConvert.SerializeObject(usuario));
		}

		public static UsuarioModel GetSession(HttpContext context)
		{
			try
			{
				var accountString = context.Session.GetString("Streamberry.Session");

				if (string.IsNullOrEmpty(accountString))
				{
					return null;
				}

				return JsonConvert.DeserializeObject<UsuarioModel>(accountString);
			}
			catch
			{
				return null;
			}
		}
	}
}