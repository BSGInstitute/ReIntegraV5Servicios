using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: IGlassdoorConfiguracionRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de cuentas de empleador en Glassdoor.
    /// CRUD básico para gestionar la configuración de la cuenta (rating general, total evaluaciones).
    /// API pública descontinuada en 2023 — captura manual.
    /// </summary>
    public interface IGlassdoorConfiguracionRepository : IGenericRepository<TGlassdoorConfiguracion>
    {
        /// <summary>Inserta una configuración de cuenta y retorna el modelo persistido.</summary>
        TGlassdoorConfiguracion Add(GlassdoorConfiguracion entidad);
        /// <summary>Actualiza una configuración de cuenta con control de concurrencia.</summary>
        TGlassdoorConfiguracion Update(GlassdoorConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de cuenta.</summary>
        bool Delete(int id, string usuario);
    }
}
