using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// Interface: IScorePrimeraOportunidadService
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Contrato del servicio que expone el score P0 de una oportunidad.
    /// </summary>
    public interface IScorePrimeraOportunidadService
    {
        ScorePrimeraOportunidadDTO ObtenerP0PorIdOportunidad(int idOportunidad);
    }
}
