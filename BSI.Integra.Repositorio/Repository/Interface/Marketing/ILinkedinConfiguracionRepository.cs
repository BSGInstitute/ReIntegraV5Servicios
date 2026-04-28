using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: ILinkedinConfiguracionRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de páginas de LinkedIn.
    /// CRUD básico para gestionar la configuración de las páginas (nombre, enlace, total opiniones).
    /// </summary>
    public interface ILinkedinConfiguracionRepository : IGenericRepository<TLinkedinConfiguracion>
    {
        /// <summary>Inserta una configuración de página y retorna el modelo persistido.</summary>
        TLinkedinConfiguracion Add(LinkedinConfiguracion entidad);
        /// <summary>Actualiza una configuración de página con control de concurrencia.</summary>
        TLinkedinConfiguracion Update(LinkedinConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de página.</summary>
        bool Delete(int id, string usuario);
    }
}
