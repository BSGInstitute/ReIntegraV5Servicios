using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: ILinkedinConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de páginas de LinkedIn.
    /// CRUD de la configuración (nombre, enlace, total opiniones).
    /// </summary>
    public interface ILinkedinConfiguracionService
    {
        /// <summary>Obtiene la configuración activa de la página de LinkedIn.</summary>
        LinkedinConfiguracion Obtener();
        /// <summary>Inserta una configuración de página y persiste los cambios.</summary>
        LinkedinConfiguracion Add(LinkedinConfiguracion entidad);
        /// <summary>Actualiza una configuración de página y persiste los cambios.</summary>
        LinkedinConfiguracion Update(LinkedinConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de página por su Id.</summary>
        bool Delete(int id, string usuario);
    }
}
