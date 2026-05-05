using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: IFacebookConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de páginas de Facebook.
    /// CRUD de la configuración (identificador, nombre, token, opiniones, valoración).
    /// </summary>
    public interface IFacebookConfiguracionService
    {
        /// <summary>Obtiene todas las configuraciones activas de páginas de Facebook.</summary>
        List<FacebookConfiguracion> ObtenerTodos();
        /// <summary>Inserta una configuración de página y persiste los cambios.</summary>
        FacebookConfiguracion Add(FacebookConfiguracion entidad);
        /// <summary>Actualiza una configuración de página y persiste los cambios.</summary>
        FacebookConfiguracion Update(FacebookConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de página por su Id.</summary>
        bool Delete(int id, string usuario);
    }
}
