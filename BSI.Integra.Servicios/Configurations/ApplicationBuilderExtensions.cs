namespace BSI.Integra.Servicios.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        public static IApplicationBuilder AddGlobalJwtHandler(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<JwtMiddleware>();
            return applicationBuilder;
        }
        //public static IApplicationBuilder AddGlobalJwtHandler(this IApplicationBuilder applicationBuilder) => applicationBuilder.UseMiddleware<JwtMiddleware>();
    }
}
