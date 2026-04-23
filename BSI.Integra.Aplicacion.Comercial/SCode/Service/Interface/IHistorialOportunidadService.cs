using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    /// Interface: IHistorialOportunidadService
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Contrato del servicio de historial de oportunidades por alumno.
    /// </summary>
    public interface IHistorialOportunidadService
    {
        HistorialOportunidadAlumnoDTO ObtenerHistorialPorIdAlumno(int idAlumno);
    }
}
