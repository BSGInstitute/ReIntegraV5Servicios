using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CrucigramaProgramaCapacitacionRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 07/09/2023
    /// Versión: 1.0
    /// <summary>
    /// Gestión general de T_CrucigramaProgramaCapacitacion
    /// </summary>
    public class CrucigramaProgramaCapacitacionRepository : GenericRepository<TCrucigramaProgramaCapacitacion>, ICrucigramaProgramaCapacitacionRepository
    {
        private Mapper _mapper;

        public CrucigramaProgramaCapacitacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCrucigramaProgramaCapacitacion, CrucigramaProgramaCapacitacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCrucigramaProgramaCapacitacion MapeoEntidad(CrucigramaProgramaCapacitacion entidad)
        {
            try
            {
                TCrucigramaProgramaCapacitacion modelo = _mapper.Map<TCrucigramaProgramaCapacitacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCrucigramaProgramaCapacitacion Add(CrucigramaProgramaCapacitacion entidad)
        {
            try
            {
                var crucigramaProgramaCapacitacion = MapeoEntidad(entidad);
                base.Insert(crucigramaProgramaCapacitacion);
                return crucigramaProgramaCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCrucigramaProgramaCapacitacion Update(CrucigramaProgramaCapacitacion entidad)
        {
            try
            {
                var crucigramaProgramaCapacitacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                crucigramaProgramaCapacitacion.RowVersion = entidadExistente.RowVersion;

                base.Update(crucigramaProgramaCapacitacion);
                return crucigramaProgramaCapacitacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TCrucigramaProgramaCapacitacion> Add(IEnumerable<CrucigramaProgramaCapacitacion> listadoEntidad)
        {
            try
            {
                List<TCrucigramaProgramaCapacitacion> listado = new List<TCrucigramaProgramaCapacitacion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TCrucigramaProgramaCapacitacion> Update(IEnumerable<CrucigramaProgramaCapacitacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCrucigramaProgramaCapacitacion> listado = new List<TCrucigramaProgramaCapacitacion>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el reporte de crucigramas para su exportación en excel
        /// </summary>
        /// <returns> Lista de objetos de tipo CrucigramaProgramaCapacitacionDetalleDTO </returns>
        public IEnumerable<ReporteExcelCrucigramasDTO> ObtenerReporteCrucigramasExportacionExcel()
        {
            try
            {
                var query = "pw.SP_PW_GenerarReporteCrucigramasExcel";
                var resultado = _dapperRepository.QuerySPDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ReporteExcelCrucigramasDTO>>(resultado)!;
                }
                return new List<ReporteExcelCrucigramasDTO>();

            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCR-ORCEE-001@Error en ObtenerReporteCrucigramasExportacionExcel() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
		/// Obtiene todos los crucigramas de programa de capacitacion registrados en el sistema
		/// </summary>
		/// <returns> Lista de objetos de tipo (CrucigramaProgramaCapacitacionDTO) </returns>
		public IEnumerable<CrucigramaProgramaCapacitacionRespuestaDTO> ObtenerCrucigramasRegistrados()
        {
            try
            {
                var query = @"SELECT Id, CodigoCrucigrama, IdPGeneral, IdPEspecifico, PGeneral, IdCapitulo, IdSesion, IdTipoMarcador, ValorMarcador, CantidadFila, CantidadColumna 
                              FROM [pla].[V_TCrucicramaProgramaCapacitacion_ObtenerCrucigramasRegistrados] 
                              WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CrucigramaProgramaCapacitacionRespuestaDTO>>(resultado)!;
                }
                return new List<CrucigramaProgramaCapacitacionRespuestaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCR-OCR-002@Error en ObtenerCrucigramasRegistrados() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
		/// Obtiene respuestas asociadas a una pregunta
		/// </summary>
		/// <param name="idCrucigramaProgramaCapacitacion">Id del crucigrama del programa de capacitacion (PK de la tabla pla.T_CrucigramaProgramaCapacitacion)</param>
		/// <returns> Lista de objetos de tipo CrucigramaProgramaCapacitacionDetalleDTO </returns>
		public IEnumerable<CrucigramaProgramaCapacitacionDetalleDTO> ObtenerRespuestaCrucigramaProgramaCapacitacion(int idCrucigramaProgramaCapacitacion)
        {
            try
            {
                string query = @"SELECT Id, NumeroPalabra, Palabra, Definicion, Tipo, ColumnaInicio, FilaInicio 
                                 FROM [pla].[V_TCrucigramaProgramaCapacitacionDetalle_ObtenerRespuestasCrucigrama] 
                                 WHERE Estado = 1 AND IdCrucigramaProgramaCapacitacion = @IdCrucigramaProgramaCapacitacion";
                var resultado = _dapperRepository.QueryDapper(query, new { IdCrucigramaProgramaCapacitacion = idCrucigramaProgramaCapacitacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<CrucigramaProgramaCapacitacionDetalleDTO>>(resultado)!;
                }
                return new List<CrucigramaProgramaCapacitacionDetalleDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCR-ORXPC-003@Error en ObtenerRespuestaCrucigramaProgramaCapacitacion() {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 07/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene toda la información de la tabla T_CrucigramaProgramaCapacitacion por medio del Id
        /// </summary>
        /// <returns> Entidad - CrucigramaProgramaCapacitacion </returns>
        public CrucigramaProgramaCapacitacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT 
                                Id,
                                IdPgeneral,
                                IdPEspecifico,
                                OrdenFilaCapitulo,
                                OrdenFilaSesion,
                                CodigoCrucigrama,
                                IdTipoMarcador,
                                ValorMarcador,
                                CantidadFila,
                                CantidadColumna,
                                Estado,
                                UsuarioCreacion,
                                UsuarioModificacion,
                                FechaCreacion,
                                FechaModificacion,
                                RowVersion,
                                IdMigracion 
                            FROM pla.T_CrucigramaProgramaCapacitacion 
                            WHERE Estado = 1 AND Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != null)
                {
                    return JsonConvert.DeserializeObject<CrucigramaProgramaCapacitacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCR-OPI-004@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }
    }
}
