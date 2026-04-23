using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: IGlassdoorConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de cuentas de empleador en Glassdoor.
    /// CRUD de la configuración (rating general, total evaluaciones, URL perfil, EmployerId).
    /// API pública descontinuada en 2023 — captura manual.
    /// </summary>
    public interface IGlassdoorConfiguracionService
    {
        /// <summary>Obtiene la configuración activa de la cuenta de empleador en Glassdoor.</summary>
        GlassdoorConfiguracion Obtener();
        /// <summary>Inserta una configuración de cuenta y persiste los cambios.</summary>
        GlassdoorConfiguracion Add(GlassdoorConfiguracion entidad);
        /// <summary>Actualiza una configuración de cuenta y persiste los cambios.</summary>
        GlassdoorConfiguracion Update(GlassdoorConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de cuenta por su Id.</summary>
        bool Delete(int id, string usuario);
    }
}
