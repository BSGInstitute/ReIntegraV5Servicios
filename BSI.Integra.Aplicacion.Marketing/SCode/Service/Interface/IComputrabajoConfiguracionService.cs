using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: IComputrabajoConfiguracionService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Contrato del servicio de dominio para la gestión de cuentas de empleador en Computrabajo.
    /// CRUD de la configuración (rating general, total evaluaciones, URL perfil).
    /// </summary>
    public interface IComputrabajoConfiguracionService
    {
        /// <summary>Obtiene la configuración activa de la cuenta de empleador en Computrabajo.</summary>
        ComputrabajoConfiguracion Obtener();
        /// <summary>Inserta una configuración de cuenta y persiste los cambios.</summary>
        ComputrabajoConfiguracion Add(ComputrabajoConfiguracion entidad);
        /// <summary>Actualiza una configuración de cuenta y persiste los cambios.</summary>
        ComputrabajoConfiguracion Update(ComputrabajoConfiguracion entidad);
        /// <summary>Elimina lógicamente una configuración de cuenta por su Id.</summary>
        bool Delete(int id, string usuario);
    }
}
