using BSI.Integra.Servicios.Helpers;
using System.Security.Claims;

namespace BSI.Integra.Servicios.Configurations
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var claimsIdentity = context.User.Identity as ClaimsIdentity;
            var registroToken = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
            //if (userId != null)
            //{
            //    // attach user to context on successful jwt validation
            //    context.Items["User"] = userService.GetById(userId.Value);
            //}
            if (registroToken.TokenValida)
            {
                context.Items["RegistroClaimToken"] = registroToken.RegistroClaimToken;
            }
            context.Items["DescripcionGeneral"] = registroToken.DescripcionGeneral;
            await _next(context);
        }
    }
}
