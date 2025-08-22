using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Configurations
{
    public class JwtExpirationValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var claimsIndentity = context.HttpContext.User.Identity as ClaimsIdentity;
            var claimExpira = claimsIndentity!.Claims.Where(x => x.Type == "Expira").FirstOrDefault();

            string mensaje = string.Empty;

            if (claimExpira == null)
            {
                mensaje = "Usuario invalido.";
            }
            else if (string.IsNullOrEmpty(claimExpira.Value))
            {
                mensaje = "El codigo de sesión no es el correcto o ha sido alterado.";
            }
            else
            {
                try
                {
                    //if (DateTime.UtcNow > Convert.ToDateTime(claimExpira.Value))
                    if (DateTime.UtcNow > DateTime.UtcNow.AddDays(15))
                    {
                        mensaje = "La sesión ha expirado, vuelve a iniciar sesión.";
                    }
                }
                catch
                {
                    mensaje = "Fecha expiracion invalida";
                }
            }
            if (!string.IsNullOrEmpty(mensaje))
            {
                context.Result = new UnauthorizedObjectResult(new { error = mensaje, status = HttpStatusCode.Unauthorized });
            }
            base.OnActionExecuting(context);
        }
    }
}
