# ── Stage 1: Restore & Build ──────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy .csproj files first — Docker caches this layer until any .csproj changes.
# PruebasUnitarias is excluded (targets net8.0, not needed in production image).
COPY BSI.Integra.Servicios/BSI.Integra.Servicios.csproj                                   BSI.Integra.Servicios/
COPY BSI.Integra.Aplicacion.Base/BSI.Integra.Aplicacion.Base.csproj                       BSI.Integra.Aplicacion.Base/
COPY BSI.Integra.Aplicacion.DTO/BSI.Integra.Aplicacion.DTO.csproj                         BSI.Integra.Aplicacion.DTO/
COPY BSI.Integra.Aplicacion.Transversal/BSI.Integra.Aplicacion.Transversal.csproj         BSI.Integra.Aplicacion.Transversal/
COPY BSI.Integra.Aplicacion.Servicios/BSI.Integra.Aplicacion.Servicios.csproj             BSI.Integra.Aplicacion.Servicios/
COPY BSI.Integra.Aplicacion.Comercial/BSI.Integra.Aplicacion.Comercial.csproj             BSI.Integra.Aplicacion.Comercial/
COPY BSI.Integra.Aplicacion.Finanzas/BSI.Integra.Aplicacion.Finanzas.csproj               BSI.Integra.Aplicacion.Finanzas/
COPY BSI.Integra.Aplicacion.GestionPersonas/BSI.Integra.Aplicacion.GestionPersonas.csproj BSI.Integra.Aplicacion.GestionPersonas/
COPY BSI.Integra.Aplicacion.Interaccion/BSI.Integra.Aplicacion.Interaccion.csproj         BSI.Integra.Aplicacion.Interaccion/
COPY BSI.Integra.Aplicacion.Marketing/BSI.Integra.Aplicacion.Marketing.csproj             BSI.Integra.Aplicacion.Marketing/
COPY BSI.Integra.Aplicacion.Operaciones/BSI.Integra.Aplicacion.Operaciones.csproj         BSI.Integra.Aplicacion.Operaciones/
COPY BSI.Integra.Aplicacion.Planificacion/BSI.Integra.Aplicacion.Planificacion.csproj     BSI.Integra.Aplicacion.Planificacion/
COPY BSI.Integra.Persistencia/BSI.Integra.Persistencia.csproj                             BSI.Integra.Persistencia/
COPY BSI.Integra.Repositorio/BSI.Integra.Repositorio.csproj                               BSI.Integra.Repositorio/

RUN dotnet restore BSI.Integra.Servicios/BSI.Integra.Servicios.csproj

# Copy full source and publish (Release, self-contained=false → uses runtime image)
COPY . .

RUN dotnet publish BSI.Integra.Servicios/BSI.Integra.Servicios.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ── Stage 2: Runtime ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runner
WORKDIR /app

RUN apk add --no-cache icu-libs

# Non-root user for security hardening
RUN addgroup --system --gid 1001 appgroup \
    && adduser --system --uid 1001 --ingroup appgroup appuser

COPY --from=build --chown=appuser:appgroup /app/publish .

USER appuser

# Only expose HTTP. UseHttpsRedirection is a no-op when ASPNETCORE_URLS has no HTTPS port.
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# Swagger is available when ASPNETCORE_ENVIRONMENT=Development (set in docker-compose).
HEALTHCHECK --interval=30s --timeout=5s --start-period=40s --retries=3 \
    CMD wget -qO- http://localhost:80/swagger/index.html || exit 1

ENTRYPOINT ["dotnet", "BSI.Integra.Servicios.dll"]
