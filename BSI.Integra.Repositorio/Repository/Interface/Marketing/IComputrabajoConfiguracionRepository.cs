using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    /// Interface: IComputrabajoConfiguracionRepository
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del repositorio de cuentas de empleador en Computrabajo.
    /// CRUD básico para gestionar la configuración de la cuenta (rating general, total evaluaciones).
    /// </summary>
    public interface IComputrabajoConfiguracionRepository : IGenericRepository<TComputrabajoConfiguracion>
    {
        /// <summary>Inserta una configuración de cuenta y retorna el modelo persistido.</summary>
        TComputrabajoConfiguracion Add(ComputrabajoConfiguracion entidad);
        /// <summary>Actualiza una configuración de cuenta con control de concurrencia.</summary>
        TComputrabajoConfiguracion Update(ComputrabajoConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de cuenta.</summary>
        bool Delete(int id, string usuario);
    }
}
