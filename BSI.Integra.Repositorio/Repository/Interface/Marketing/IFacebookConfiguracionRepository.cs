using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: IFacebookConfiguracionRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de páginas de Facebook.
    /// CRUD básico para gestionar la configuración de las páginas.
    /// </summary>
    public interface IFacebookConfiguracionRepository : IGenericRepository<TFacebookConfiguracion>
    {
        /// <summary>Inserta una configuración de página y retorna el modelo persistido.</summary>
        TFacebookConfiguracion Add(FacebookConfiguracion entidad);
        /// <summary>Actualiza una configuración de página con control de concurrencia.</summary>
        TFacebookConfiguracion Update(FacebookConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de página.</summary>
        bool Delete(int id, string usuario);
    }
}
