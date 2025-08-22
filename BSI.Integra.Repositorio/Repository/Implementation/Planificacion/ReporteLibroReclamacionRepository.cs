using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ReporteLibroReclamacionRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 27/04/2023
    /// <summary>
    /// Gestión general de Reporte Libro de Reclamaciones
    /// </summary>
    public class ReporteLibroReclamacionRepository : IReporteLibroReclamacionRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteLibroReclamacionRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
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
                IEnumerable<StringDTO> items = new List<StringDTO>();
                var query = @"
                            SELECT DISTINCT 
                                CONCAT(Nombre, ' ', Apellido) AS Valor
                            FROM 
                                [40.76.216.5].integraDB_PortalWeb.dbo.T_LibroReclamacion 
                            WHERE 
                                Nombre LIKE @Nombre";
                var lista = _dapperRepository.QueryDapper(query, new { Nombre = $"%{nombre}%" });
                if (!string.IsNullOrEmpty(lista) && !lista.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<IEnumerable<StringDTO>>(lista)!;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerListaNombreReclamo()", ex);
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
                List<StringDTO> rpta = new List<StringDTO>();
                var query = @"
                            SELECT DISTINCT 
                                DNI AS Valor
                            FROM 
                                [40.76.216.5].integraDB_PortalWeb.dbo.T_LibroReclamacion 
                            WHERE 
                                DNI LIKE @Dni";
                var resultado = _dapperRepository.QueryDapper(query, new { Dni = $"%{dni}%" });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<StringDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerListaDniReclamo()", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 27/04/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtener el reporte de libro de reclamaciones segun el filtro ingresado
        /// </summary>
        /// <param name="filtroReporte">filtro para la seleccion del reporte por fechainicio, fechafin, nombre y dni</param>
        /// <returns> Lista del reporte libro de reclamaciones - List<ReporteLibroReclamacionDTO> </returns>
        public List<ReporteLibroReclamacionDTO> GenerarReporteLibroReclamacion(ReporteLibroReclamacionFiltroDTO filtroReporte)
        {
            try
            {
                List<ReporteLibroReclamacionDTO> reporteDescargaMaterial = new List<ReporteLibroReclamacionDTO>();
                var query = _dapperRepository.QuerySPDapper("[mkt].[SP_ReporteLibroReclamacion]", new { filtroReporte.FechaInicio, filtroReporte.FechaFin, filtroReporte.Nombre, filtroReporte.Dni });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteDescargaMaterial = JsonConvert.DeserializeObject<List<ReporteLibroReclamacionDTO>>(query)!;
                }
                return reporteDescargaMaterial;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método GenerarReporteLibroReclamacion()", ex);
            }
        }
    }
}
