using Microsoft.AspNetCore.Mvc.Filters;

namespace BSI.Integra.Servicios.Configurations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtActionFilter : Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
    {
        //IAuthenticationFilter
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            return;
            //var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            //if (allowAnonymous)
            //    return;

            //var registroClaimToken = (RegistroClaimTokenDTO)context.HttpContext.Items["RegistroClaimToken"];
            //var descripcionGeneral = (string)context.HttpContext.Items["DescripcionGeneral"];
            //if (registroClaimToken == null)
            //{
            //    throw new UnauthorizedAccessRequestException(descripcionGeneral);
            //}
        }
    }
}
