using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class EtapaProcesoSeleccionCalificadoRepository : GenericRepository<TEtapaProcesoSeleccionCalificado>, IEtapaProcesoSeleccionCalificadoRepository
    {
        private Mapper _mapper;
        public EtapaProcesoSeleccionCalificadoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEtapaProcesoSeleccionCalificado, EtapaProcesoSeleccionCalificado>(MemberList.None).ReverseMap();
                cfg.CreateMap<EtapaProcesoSeleccionCalificado, EtapaProcesoSeleccionCalificadoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<EtapaProcesoSeleccionCalificado, TEtapaProcesoSeleccionCalificado>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEtapaProcesoSeleccionCalificado MapeoEntidad(EtapaProcesoSeleccionCalificado entidad)
        {
            try
            {
                TEtapaProcesoSeleccionCalificado modelo = _mapper.Map<TEtapaProcesoSeleccionCalificado>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEtapaProcesoSeleccionCalificado Add(EtapaProcesoSeleccionCalificado entidad)
        {
            try
            {
                var EtapaProcesoSeleccionCalificado = MapeoEntidad(entidad);
                base.Insert(EtapaProcesoSeleccionCalificado);
                return EtapaProcesoSeleccionCalificado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEtapaProcesoSeleccionCalificado Update(EtapaProcesoSeleccionCalificado entidad)
        {
            try
            {
                var EtapaProcesoSeleccionCalificado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EtapaProcesoSeleccionCalificado.RowVersion = entidadExistente.RowVersion;

                base.Update(EtapaProcesoSeleccionCalificado);
                return EtapaProcesoSeleccionCalificado;
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
        public IEnumerable<TEtapaProcesoSeleccionCalificado> Add(IEnumerable<EtapaProcesoSeleccionCalificado> listadoEntidad)
        {
            try
            {
                List<TEtapaProcesoSeleccionCalificado> listado = new List<TEtapaProcesoSeleccionCalificado>();
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
        public IEnumerable<TEtapaProcesoSeleccionCalificado> Update(IEnumerable<EtapaProcesoSeleccionCalificado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEtapaProcesoSeleccionCalificado> listado = new List<TEtapaProcesoSeleccionCalificado>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_EtapaProcesoSeleccionCalificado por el Primary Key
        /// </summary>
        /// <returns>EtapaProcesoSeleccionCalificado o Nulo</returns>
        public EtapaProcesoSeleccionCalificado? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                    IdProcesoSeleccionEtapa,
		                    IdPostulante,
		                    EsEtapaAprobada,
		                    NotaCalculada,
		                    IdEstadoEtapaProcesoSeleccion,
		                    Estado,
		                    UsuarioCreacion,
		                    UsuarioModificacion,
		                    FechaCreacion,
		                    FechaModificacion,
		                    RowVersion,
		                    IdMigracion,
		                    EsEtapaActual,
		                    EsContactado
                    FROM gp.T_EtapaProcesoSeleccionCalificado
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<EtapaProcesoSeleccionCalificado>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 10/06/2024
        /// <summary>
        /// Obtiene los idsPostulantes de etapas calificadas proceso de seleccion 
        /// </summary>
        /// <param name="filtroReporte"> Filtro reporte postulantes </param>
		/// <returns> Lista de idsPostulantes </returns>
        public List<int> ObtenerIdsPostulanteEtapaProcesoSeleccionActual(EvaluacionPostulanteFiltroReporteDTO filtroReporte)
        {
            try
            {
                var queryFiltro = string.Empty;
                if (filtroReporte.IdsPostulantes.Count() > 0)
                {
                    queryFiltro += " AND IdPostulante IN @IdsPostulantes ";
                }
                if (filtroReporte.IdsEstadoEtapa.Count() > 0)
                {
                    queryFiltro += $" AND IdEstadoEtapaProcesoSeleccion IN @IdsEstadoEtapa ";
                }
                if (filtroReporte.IdsProcesoEtapa.Count > 0)
                {
                    queryFiltro += $" AND IdProcesoSeleccionEtapa IN @IdsProcesoEtapa ";
                }

                var query = $@"
                    SELECT
	                    IdPostulante AS Valor
                    FROM gp.V_ObtenerEtapasCalificadasActivas
                    WHERE 
	                    Estado = 1 
	                    AND EstadoPostulanteProcesoSeleccion = 1
                        {queryFiltro}
                    GROUP BY IdPostulante";
                var resultado = _dapperRepository.QueryDapper(query, new
                {
                    filtroReporte.IdsPostulantes,
                    filtroReporte.IdsEstadoEtapa,
                    filtroReporte.IdsProcesoEtapa,
                });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    var rpta = JsonConvert.DeserializeObject<List<IntDTO>>(resultado);
                    return rpta.Select(x => x.Valor!.Value).ToList();
                }
                return new List<int>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Autor: Flavio R.M.F.
        ///Fecha: 12/06/2024
        /// <summary>
        /// Obtiene información de una lista postulantes y lista de procesos de selección
        /// </summary>
        /// <param name="idsPostulante">Lista de postulantes</param>
        /// <param name="idsProcesoSeleccion"> Filtro sentencia WHERE según filtro elegido</param>
		/// <returns> Informción de Etapa y Estado de Proceso por Postlantes y Proceso de Seleccion </returns>
        /// <returns> Lista de Objeto DTO: List<ObtenerPostulantesProcesoSeleccionDTO> </returns>
        public List<EtapaCalificadaPostulanteProcesoSeleccionDTO> ObtenerPorIdsPostulanteIdsProcesoSeleccion(List<int> idsPostulante, List<int> idsProcesoSeleccion)
        {
            try
            {
                var query = @"SELECT
	                    IdPostulante,
	                    IdProcesoSeleccion,
	                    IdProcesoSeleccionEtapa,
	                    ProcesoSeleccionEtapa,
	                    NroOrden,
	                    IdEtapaProcesoSeleccionCalificado,
	                    IdEstadoEtapaProcesoSeleccion,
	                    EstadoEtapaProcesoSeleccion,
	                    EsEtapaAprobada,
	                    EsContactado,
	                    EsCalificadoPorPostulante
                    FROM gp.V_ObtenerEtapasCalificadasPostulanteProcesoSeleccion
                    WHERE IdPostulante IN @IdsPostulante 
                        AND IdProcesoSeleccion IN @IdsProcesoSeleccion
                        AND Estado = 1
                        AND EsContactado IS NOT NULL
                    ORDER BY IdPostulante, NroOrden ASC";
                var resultado = _dapperRepository.QueryDapper(query, new { IdsPostulante = idsPostulante, IdsProcesoSeleccion = idsProcesoSeleccion });
                return JsonConvert.DeserializeObject<List<EtapaCalificadaPostulanteProcesoSeleccionDTO>>(resultado)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Autor: Flavio R.M.F.
        ///Fecha: 12/06/2024
        /// <summary>
        /// Obtiene información de una lista postulantes y lista de procesos de selección
        /// </summary>
        /// <param name="idPostulante">Lista de postulantes</param>
		/// <returns> Informción de Etapa y Estado de Proceso por Postlantes y Proceso de Seleccion </returns>
        /// <returns> EtapaProcesoSeleccionCalificado </returns>
        public EtapaProcesoSeleccionCalificado? ObtenerEtapaActualPorIdPostulante(int idPostulante)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdProcesoSeleccionEtapa,
	                    IdPostulante,
	                    EsEtapaAprobada,
	                    NotaCalculada,
	                    IdEstadoEtapaProcesoSeleccion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion,
	                    EsEtapaActual,
	                    EsContactado
                    FROM gp.T_EtapaProcesoSeleccionCalificado
                    WHERE EsEtapaActual = 1
                        AND IdPostulante = @IdPostulante 
                        AND Estado=1
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPostulante = idPostulante });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<EtapaProcesoSeleccionCalificado>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EtapaProcesoSeleccionCalificado? ObtenerPorIdPostulanteIdProcesoSeleccionEtapa(int idPostulante, int idProcesoSeleccionEtapa)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdProcesoSeleccionEtapa,
	                    IdPostulante,
	                    EsEtapaAprobada,
	                    NotaCalculada,
	                    IdEstadoEtapaProcesoSeleccion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion,
	                    EsEtapaActual,
	                    EsContactado
                    FROM gp.T_EtapaProcesoSeleccionCalificado
                    WHERE
                        IdPostulante = @IdPostulante
                        AND IdProcesoSeleccionEtapa = @IdProcesoSeleccionEtapa
                        AND Estado =1
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPostulante = idPostulante, IdProcesoSeleccionEtapa = idProcesoSeleccionEtapa });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<EtapaProcesoSeleccionCalificado>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        ///Repositorio: EtapaProcesoSeleccionCalificadoRepositorio
        ///Autor: Edgar S.
        ///Fecha: 25/01/2021
        /// <summary>
        /// Obtiene Lista de Etapas de Examenes por Postulante
        /// </summary>
        /// <param name="idProcesoSeleccion"> Id de Proceso Seleccion </param>
        /// <param name="idPostulante"> Id de Postulante </param>
        /// <returns> List<EtapaExamenesPorPostulanteDTO> </returns>
        public List<EtapaExamenesPorPostulanteDTO> ObtenerListaEtapaExamenesPorPostulante(int idProcesoSeleccion, int idPostulante)
        {
            try
            {
                List<EtapaExamenesPorPostulanteDTO> rpta = new List<EtapaExamenesPorPostulanteDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdProcesoSeleccionEtapa,
	                    IdPostulante,
	                    EsEtapaAprobada,
	                    IdEstadoEtapaProcesoSeleccion,
	                    EsContactado,
	                    NroOrden,
	                    IdProcesoSeleccion,
	                    Nombre,
	                    EsCalificadoPorPostulante,
	                    Estado
                    FROM gp.V_ObtenerListaEtapaExamenesPorPostulante
                    WHERE
	                    IdProcesoSeleccion = @IdProcesoSeleccion
	                    AND IdPostulante = @IdPostulante
	                    AND Estado = 1
                    ORDER BY NroOrden ASC;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion, IdPostulante = idPostulante });
                if (!resultado.Contains("[]") && !string.IsNullOrEmpty(resultado))
                {
                    rpta = JsonConvert.DeserializeObject<List<EtapaExamenesPorPostulanteDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void ActualizarEtapaCalificada(EtapaProcesoSeleccionCalificadoActualizarDTO etapaCalificada)
        {
            try
            {
                var query = "mkt.SP_TEtapaProcesoSeleccionCalificado_Actualizar";
                var parametros = new
                {
                    Id = etapaCalificada.Id,
                    IdProcesoSeleccionEtapa = etapaCalificada.IdProcesoSeleccionEtapa,
                    IdPostulante = etapaCalificada.IdPostulante,
                    EsEtapaAprobada = etapaCalificada.EsEtapaAprobada,
                    NotaCalculada = etapaCalificada.NotaCalculada,
                    IdEstadoEtapaProcesoSeleccion = etapaCalificada.IdEstadoEtapaProcesoSeleccion,
                    EsEtapaActual = etapaCalificada.EsEtapaActual,
                    EsContactado = etapaCalificada.EsContactado,
                    UsuarioModificacion = etapaCalificada.UsuarioModificacion,
                };

                var resultado = _dapperRepository.QuerySPDapper(query, parametros);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarObjetoSerializadoCampaign() {ex.Message}", ex);
            }
        }
    }
}
