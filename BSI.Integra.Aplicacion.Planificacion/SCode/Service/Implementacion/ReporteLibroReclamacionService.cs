using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ReporteLibroReclamacionService
    /// Autor: Jonathan Caipo
    /// Fecha: 29/04/2023
    /// Version 1.0
    /// <summary>
    /// Gestión general del Reporte Problemas Aula Virtual
    /// </summary>
    public class ReporteLibroReclamacionService : IReporteLibroReclamacionService
    {
        private IUnitOfWork _unitOfWork;

        public ReporteLibroReclamacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 27/04/2023
        /// Versión: 1.0
        /// <summary>
        /// obtener los nombres de la personas segun la coincidencia del parametro recibido
        /// </summary>
        /// <param name="nombre">parte del nombre para buscar coincidencias</param>
        /// <returns> Lista de los nombres - List<StringDTO> </returns>
        public IEnumerable<StringDTO> ObtenerListaNombreReclamo(string nombre)
        {
            try
            {
                return _unitOfWork.ReporteLibroReclamacionRepository.ObtenerListaNombreReclamo(nombre);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 27/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener los dni de las personas segun la coincidencia del parametro recibido
        /// </summary>
        /// <param name="dni">parte del dni para buscar coincidencias</param>
        /// <returns> Lista de los dnis - List<StringDTO> </returns>
        public IEnumerable<StringDTO> ObtenerListaDniReclamo(string dni)
        {
            try
            {
                return _unitOfWork.ReporteLibroReclamacionRepository.ObtenerListaDniReclamo(dni);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 27/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener el reporte de libro de reclamaciones según el filtro ingresado
        /// </summary>
        /// <param name="dni">parte del dni para buscar coincidencias</param>
        /// <returns> Lista de los dnis - List<StringDTO> </returns>
        public List<ReporteLibroReclamacionDTO> GenerarReporteLibroReclamacion(ReporteLibroReclamacionFiltroDTO filtroReporte)
        {
            try
            {
                string nombre = "%%", dni = "%%";
                if (filtroReporte.Nombre != null && filtroReporte.Nombre != "")
                    nombre = "%" + filtroReporte.Nombre + "%";
                else
                    filtroReporte.Nombre = "%%";
                if (filtroReporte.Dni != null && filtroReporte.Dni != "") 
                    dni = "%" + filtroReporte.Dni + "%";
                else
                    filtroReporte.Dni = "%%";

                filtroReporte.FechaInicio = new DateTime(filtroReporte.FechaInicio.Year, filtroReporte.FechaInicio.Month, filtroReporte.FechaInicio.Day, 0, 0, 0);
                filtroReporte.FechaFin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);

                return _unitOfWork.ReporteLibroReclamacionRepository.GenerarReporteLibroReclamacion(filtroReporte);
            }
            catch
            {
                throw;
            }
        }
    }
}
