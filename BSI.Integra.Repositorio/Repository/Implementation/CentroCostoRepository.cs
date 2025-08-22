using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Api.Ads.AdWords.v201809;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CentroCostoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/06/2022
    /// <summary>
    /// Gestión general de T_CentroCosto
    /// </summary>
    public class CentroCostoRepository : GenericRepository<TCentroCosto>, ICentroCostoRepository
    {
        private Mapper _mapper;

        public CentroCostoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCentroCosto, CentroCosto>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCampaniaGeneralDetalle, CampaniaGeneralDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<TPespecifico, PEspecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TConfigurarWebinar, ConfigurarWebinar>(MemberList.None).ReverseMap();
                cfg.CreateMap<TCursoPespecifico, CursoPespecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TFeedbackGrupoPreguntaProgramaEspecifico, FeedbackGrupoPreguntaProgramaEspecifico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSolicitudAlumno, SolicitudAlumno>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSolicitudOperacionesAccesoTemporalDetalle, SolicitudOperacionesAccesoTemporalDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCentroCosto MapeoEntidad(CentroCosto entidad)
        {
            try
            {
                //Crea la entidad padre
                TCentroCosto modelo = _mapper.Map<TCentroCosto>(entidad);
                //Mapea los hijos
                if (entidad.CampaniaGeneralDetalles != null && entidad.CampaniaGeneralDetalles.Count() > 0)
                {
                    modelo.TCampaniaGeneralDetalles = _mapper.Map<ICollection<TCampaniaGeneralDetalle>>(entidad.CampaniaGeneralDetalles);
                }
                if (entidad.Pespecificos != null && entidad.Pespecificos.Count() > 0)
                {
                    modelo.TPespecificos = new List<TPespecifico>();
                    foreach (var item in entidad.Pespecificos)
                    {
                        TPespecifico modeloPespecifico = _mapper.Map<TPespecifico>(item);
                        if (item.ConfigurarWebinars != null && item.ConfigurarWebinars.Count() > 0)
                        {
                            modeloPespecifico.TConfigurarWebinars = _mapper.Map<ICollection<TConfigurarWebinar>>(item.ConfigurarWebinars);
                        }
                        if (item.CursoPespecificos != null && item.CursoPespecificos.Count() > 0)
                        {
                            modeloPespecifico.TCursoPespecificos = _mapper.Map<ICollection<TCursoPespecifico>>(item.CursoPespecificos);
                        }
                        if (item.FeedbackGrupoPreguntaProgramaEspecificos != null && item.FeedbackGrupoPreguntaProgramaEspecificos.Count() > 0)
                        {
                            modeloPespecifico.TFeedbackGrupoPreguntaProgramaEspecificos = _mapper.Map<ICollection<TFeedbackGrupoPreguntaProgramaEspecifico>>(item.FeedbackGrupoPreguntaProgramaEspecificos);
                        }
                        if (item.SolicitudAlumnos != null && item.SolicitudAlumnos.Count() > 0)
                        {
                            modeloPespecifico.TSolicitudAlumnos = _mapper.Map<ICollection<TSolicitudAlumno>>(item.SolicitudAlumnos);
                        }
                        if (item.SolicitudOperacionesAccesoTemporalDetalles != null && item.SolicitudOperacionesAccesoTemporalDetalles.Count() > 0)
                        {
                            modeloPespecifico.TSolicitudOperacionesAccesoTemporalDetalles = _mapper.Map<ICollection<TSolicitudOperacionesAccesoTemporalDetalle>>(item.SolicitudOperacionesAccesoTemporalDetalles);
                        }
                        modelo.TPespecificos.Add(modeloPespecifico);
                    }
                }
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCentroCosto Add(CentroCosto entidad)
        {
            try
            {
                var CentroCosto = MapeoEntidad(entidad);
                base.Insert(CentroCosto);
                return CentroCosto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCentroCosto Update(CentroCosto entidad)
        {
            try
            {
                var CentroCosto = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CentroCosto.RowVersion = entidadExistente.RowVersion;

                base.Update(CentroCosto);
                return CentroCosto;
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


        public IEnumerable<TCentroCosto> Add(IEnumerable<CentroCosto> listadoEntidad)
        {
            try
            {
                List<TCentroCosto> listado = new List<TCentroCosto>();
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

        public IEnumerable<TCentroCosto> Update(IEnumerable<CentroCosto> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCentroCosto> listado = new List<TCentroCosto>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de CentroCosto para mostrarse en combo.
        /// </summary>
        /// <returns> Combo de Centros Costo </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM pla.V_TCentroCosto_ParaFiltro WHERE Estado = 1";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de CentroCosto para mostrarse en combo.
        /// </summary>
        /// <returns> Combo de Centros Costo </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string query = "SELECT Id, Nombre FROM pla.V_TCentroCosto_ParaFiltro WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}", ex);
            }
        }




        /// Repositorio: CentroCostoRepositorio
        /// Autor: ,Edgar S.
        /// Fecha: 22/02/2021
        /// Version: 1.1
        /// <summary>
        /// Obtener Centro Costo Para Filtro    
        /// </summary>
        /// <param></param>
        /// <returns>Objeto(Lista):List<FiltroDTO></returns>		
        public List<FiltroDTO> ObtenerCentroCostoParaFiltro()
        {
            try
            {
                //SELECT Id, Nombre FROM pla.V_TPGeneral_Nombre WHERE Estado = 1				
                string queryCentroCosto = "SELECT Id, Nombre FROM pla.V_TCentroCosto_ParaFiltro WHERE Estado = 1";
                var reultadoCentroCosto = _dapperRepository.QueryDapper(queryCentroCosto, null);
                return JsonConvert.DeserializeObject<List<FiltroDTO>>(reultadoCentroCosto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo con antiguedad maxima de un anio basado en un Nombre Parcial.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Centro de Costo</param>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerRecientesAutocomplete(string nombreParcial)
        {
            try
            {
                List<ComboDTO> centrosCosto = new List<ComboDTO>();
                var query = @"
                    SELECT Id, Nombre
                    FROM pla.T_CentroCosto
                    WHERE Estado = 1 
                        AND Nombre LIKE CONCAT('%',@nombreParcial,'%')
                        AND FechaCreacion > @ultimoAnhio
                    ORDER By Nombre ASC";
                var ultimoAnhio = DateTime.Now.AddYears(-1);
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { nombreParcial, ultimoAnhio });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    centrosCosto = JsonConvert.DeserializeObject<List<ComboDTO>>(resultadoQuery);
                }
                return centrosCosto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 20/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="valor">Nombre Parcial de Centro de Costo</param>
        /// <returns> Lista de Combos de centro costo </returns>
        public IEnumerable<ComboDTO> ObtenerFiltroAutocomplete(string valor)
        {
            try
            {
                string query = @"SELECT Id, Nombre FROM pla.V_TCentroCosto_ParaFiltro WHERE Estado = 1 AND Nombre LIKE @Valor ORDER BY Nombre ASC";
                string resultado = _dapperRepository.QueryDapper(query, new { Valor = $"%{valor}%" });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroAutocomplete(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian.
        /// Fecha: 14/03/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial.
        /// </summary>
        /// <param name="valor">Nombre parcial de centro de costo</param>
        /// <param name="tipoProgramaCarrera">Tipo programa carrera</param>
        /// <returns> Lista de Combos de centro costo </returns>
        public IEnumerable<ComboDTO> ObtenerAutocompletePorTipoProgramaCarrera(string valor, int? tipoProgramaCarrera)
        {
            try
            {
                string query;
                if (tipoProgramaCarrera == null)
                {
                    query = @"SELECT Id, Nombre FROM [pla].[V_TCentroCosto_ParaFiltroV2] 
                                        WHERE Estado = 1 
                                            AND Nombre LIKE @Valor
                                    ORDER BY Nombre ASC";
                }
                else
                {
                    query = @"SELECT Id, Nombre FROM [pla].[V_TCentroCosto_ParaFiltroV2] 
                                        WHERE Estado = 1 
                                            AND Nombre LIKE @Valor
                                            AND IdTipoProgramaCarrera = @TipoProgramaCarrera
                                    ORDER BY Nombre ASC";
                }
                string resultado = _dapperRepository.QueryDapper(query, new { Valor = $"%{valor}%", TipoProgramaCarrera = tipoProgramaCarrera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CCR-OFAv2@Error en ObtenerFiltroAutocompleteV2(): {ex.Message}", ex);
            }
        }
        /// Autor: Daniel Huaita
        /// Fecha: 16/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id y Nombre de Centros de Costo basado en un Nombre Parcial que estan en estado lanzamiento y ejecucion.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Centro de Costo</param>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerAutocompleteCentroCosto(string nombreParcial)
        {
            try
            {
                List<ComboDTO> centrosCosto = new List<ComboDTO>();
                var query = @"
                    SELECT 
                        CC.Id, 
                        CC.Nombre
                    FROM pla.T_CentroCosto AS CC
                    INNER JOIN pla.T_PEspecifico AS PS ON PS.IdCentroCosto = CC.id AND PS.EstadoP IN ('Lanzamiento','Ejecucion')
                    WHERE CC.Estado = 1
                        AND CC.Nombre LIKE CONCAT('%',@nombreParcial,'%')
                    ORDER By CC.Nombre ASC";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { nombreParcial });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    centrosCosto = JsonConvert.DeserializeObject<List<ComboDTO>>(resultadoQuery);
                }
                return centrosCosto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 17/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_CentroCosto relacionado al identificador.
        /// </summary>
        /// <param name="idCentroCosto">Id del CentroCosto</param>
        /// <returns> CentroCosto </returns>
        public CentroCosto ObtenerPorId(int idCentroCosto)
        {
            try
            {
                CentroCosto rpta = new CentroCosto();
                var query = @"
                    SELECT
                        Id,
	                    IdArea,
				        IdSubArea,
				        IdPGeneral AS IdPgeneral,
				        Nombre,
				        Codigo,
				        IdAreaCC AS IdAreaCc,
				        ISMTotales AS Ismtotales,
				        ICPFTotales AS Icpftotales,
				        Estado,
				        UsuarioCreacion,
				        UsuarioModificacion,
				        FechaCreacion,
				        FechaModificacion,
				        RowVersion,
				        IdMigracion       
                    FROM pla.T_CentroCosto
                    WHERE
	                    Estado = 1 AND Id = @idCentroCosto";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<CentroCosto>(resultado);
                    return rpta;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener información de centro de Costo AutoComplete
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de Centro de Costo</param>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerAutocompleteConPGeneral(string nombreParcial)
        {
            try
            {
                List<ComboDTO> centrosCosto = new List<ComboDTO>();
                var query = @"
                    SELECT Id,Nombre
                    FROM pla.V_TCentroCosto_ParaFiltroV2
                    WHERE Nombre LIKE CONCAT('%',@nombreParcial,'%') AND Estado = 1
                    ORDER BY Nombre ASC";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { nombreParcial });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    centrosCosto = JsonConvert.DeserializeObject<List<ComboDTO>>(resultadoQuery)!;
                }
                return centrosCosto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 31/08/2022
        /// Version: 1.0
        /// <summary>
		/// Obtiene Valor para etiquetas po centro costo
		/// </summary>
		/// <param name="idCentroCosto"></param>
		/// <returns></returns>
        public PlantillaCentroCostoDTO ObtenerCentroCostoParaPEspecifico(int idCentroCosto)
        {
            try
            {
                var centrosCosto = new PlantillaCentroCostoDTO();
                var query = @"Select IdCentroCosto,	NombrePartner, NombrePEspecifico 
                            from pla.T_CentroCosto_OptenerPlantillaPatner 
                            Where IdCentroCosto=@IdCentroCosto ";



                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idCentroCosto });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    centrosCosto = JsonConvert.DeserializeObject<PlantillaCentroCostoDTO>(resultadoQuery);
                }
                return centrosCosto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 22/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de centro de costos para filtro por Asesores
        /// </summary>
        /// <param name="listaAsesores">Ids del, o de los, asesor(es)</param>
        /// <returns> Lista objeto DTO : List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerCentroCostoPorAsesores(List<int> listaAsesores)
        {
            try
            {
                List<ComboDTO> centroCosto = new List<ComboDTO>();
                string query = "SELECT Id,Nombre FROM pla.V_ObtenerCentroCostoPorOportunidades WHERE IdPersonal IN @ListaAsesores AND id IS NOT NULL AND Estado=1";
                var respuestaQuery = _dapperRepository.QueryDapper(query, new { ListaAsesores = listaAsesores });
                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                {
                    centroCosto = JsonConvert.DeserializeObject<List<ComboDTO>>(respuestaQuery);
                }
                return centroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/10/2022
        /// Version: 1.0
        /// <summary>
		/// Obtiene Valor para etiquetas po centro costo
		/// </summary>
		/// <param name="idCentroCosto"></param>
		/// <returns></returns>
		public PlantillaCentroCostoDTO ObtenerCentroCostoParaPlantillaWhatsApp(int idCentroCosto)
        {
            try
            {
                PlantillaCentroCostoDTO resultado = new PlantillaCentroCostoDTO();
                var query = @"
                            SELECT 
                                IdCentroCosto, NombrePartner, NombrePEspecifico,NombrePgeneral 
                            FROM 
                                pla.T_CentroCosto_OptenerPlantillaWhatsapp 
                            WHERE 
                                IdCentroCosto = @IdCentroCosto";
                var centroCosto = _dapperRepository.FirstOrDefault(query, new { IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(centroCosto) && centroCosto != "null")
                {
                    resultado = JsonConvert.DeserializeObject<PlantillaCentroCostoDTO>(centroCosto)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
		/// Fecha: 04/11/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene centro de costo padre y centro de costo individual
		/// </summary>
		/// <param></param>
		/// <returns>List<CentroCostoPadreCentroCostoIndividualDTO></returns>
		public List<CentroCostoPadreCentroCostoIndividualDTO> ObtenerCentroCostoPadreCentroCostoIndividual()
        {
            try
            {
                List<CentroCostoPadreCentroCostoIndividualDTO> respuesta = new List<CentroCostoPadreCentroCostoIndividualDTO>();
                var query = @"SELECT 
                                IdCentroCosto, IdProgramaEspecifico, CentroCosto, ProgramaEspecifico, EstadoProgramaEspecifico, Tipo 
                            FROM pla.V_TPEspecifico_ObtenerPEspecificoIndividualPadre
                            WHERE 
                                Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<CentroCostoPadreCentroCostoIndividualDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos del centro de costo por IdProgramaEspecifico
        /// </summary>
        /// <param name="idPEspecifico"> Id de  PEspecifico </param>
        /// <returns> List<DatosCentroCostoDTO> </returns>
        public List<DatosCentroCostoDTO> ObtenerDatosCentroCostos(int idPEspecifico)
        {
            try
            {
                List<DatosCentroCostoDTO> datosCentroCostos = new List<DatosCentroCostoDTO>();
                var _query = "SELECT CodigoBanco, CentroCosto, Tipo, Categoria FROM pla.V_DatosCentroCostoPorIdPEspecifico WHERE IdPEspecifico = @idPEspecifico AND EstadoProgramaEspecifico = 1 AND EstadoCentroCosto = 1";
                var datosCentroCostoDB = _dapperRepository.QueryDapper(_query, new { idPEspecifico });
                if (!string.IsNullOrEmpty(datosCentroCostoDB) && !datosCentroCostoDB.Contains("[]"))
                {
                    datosCentroCostos = JsonConvert.DeserializeObject<List<DatosCentroCostoDTO>>(datosCentroCostoDB);
                }
                return datosCentroCostos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 03-10-2021
        /// Version: 1.0
        /// <summary>
        /// Permite obterner el Centro de Costo por el nombre de Campaña.
        /// </summary>
        /// <param name="Nombre"> Nombre de la Campaña </param>
        /// <returns>Objeto</returns> 
        public CentroCostoCampaniaDTO ObtenerCentroCostoPorCampania(string Nombre)
        {
            try
            {
                string query = "SELECT IdCentroCosto,CentroCosto, Codigo, Campania FROM [mkt].[V_ObtenerCentroCostoPorCampania] WHERE  Codigo LIKE CONCAT('%',@Nombre) AND Estado = 1";
                var centroCostoCampaniaAdwsDB = _dapperRepository.FirstOrDefault(query, new { Nombre = Nombre });
                if (centroCostoCampaniaAdwsDB != "null")
                {
                    return JsonConvert.DeserializeObject<CentroCostoCampaniaDTO>(centroCostoCampaniaAdwsDB);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 22/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obterner el Centro de Costo por IdConjuntoAnuncio y NombreCampania
        /// </summary>
        /// <param name="IdConjuntoAnuncio">IdConjuntoAnuncio </param>
        /// <param name="NombreCampania">Nombre Campaña </param>
        /// <returns></returns> 
        public CentroCostoCampaniaDTO ObtenerCentroCostoPorNombreIdConjuntoAnuncio(int IdConjuntoAnuncio, string NombreCampania)
        {
            try
            {
                string query = "SELECT IdCentroCosto,CentroCosto, Codigo, Campania FROM [mkt].[V_ObtenerCentroCampaniaPorIdConjuntoAnuncioYNombreCampania] WHERE  IdConjuntoAnuncio=@IdConjuntoAnuncio AND Codigo LIKE CONCAT('%',@NombreCampania) AND Estado = 1";
                var centroCostoCampaniaAdwsDB = _dapperRepository.FirstOrDefault(query, new { IdConjuntoAnuncio = IdConjuntoAnuncio, NombreCampania = NombreCampania });
                if (centroCostoCampaniaAdwsDB != "null")
                {
                    return JsonConvert.DeserializeObject<CentroCostoCampaniaDTO>(centroCostoCampaniaAdwsDB);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
		/// Obtiene una lista de CentroCosto dado un NombreParcial con Estado=1
		/// </summary>
		/// <param name="nombreCentroCosto"></param>
		/// <returns></returns>
		public List<CentroCostoPEspecificoDTO> ObtenerListaCentrosCostoPorNombre(string nombreCentroCosto)
        {
            try
            {
                List<CentroCostoPEspecificoDTO> centroCosto = new List<CentroCostoPEspecificoDTO>();
                var _query = "SELECT Id as IdCentroCosto, Nombre as NombreCentroCosto FROM pla.T_CentroCosto WHERE Estado=1 AND Nombre LIKE '%" + nombreCentroCosto + "%'";
                var centroCostoDB = _dapperRepository.QueryDapper(_query, new { nombreCentroCosto });
                if (!string.IsNullOrEmpty(centroCostoDB) && !centroCostoDB.Contains("[]"))
                {
                    centroCosto = JsonConvert.DeserializeObject<List<CentroCostoPEspecificoDTO>>(centroCostoDB);
                }
                return centroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public PlantillaCentroCostoDTO ObtenerRemplazoPlantilla(int IdPgeneral)
        {
            try
            {
                var query = "SELECT NombrePartner, NombrePgeneral FROM pla.V_Pgeneral_OptenerPlantillaWhatsapp Where IdPgeneral = @IdPgeneral";
                var CentroCosto = _dapperRepository.FirstOrDefault(query, new { IdPgeneral });
                return JsonConvert.DeserializeObject<PlantillaCentroCostoDTO>(CentroCosto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Centro Costo para filtro por tipo
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public IEnumerable<CentroCostoPEspecificoFiltroDTO> ObtenerFiltroPorTipo(bool aplicaTipo)
        {
            try
            {
                string codicionTipo = aplicaTipo ? "AND Tipo = 1" : string.Empty;
                string query = $@"
                    SELECT DISTINCT
	                    IdCentroCosto AS Id, 
						CentroCosto AS Nombre, 
						IdPEspecifico
                    FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                    WHERE
	                    Estado = 1
	                    AND RowNumber = 1 {codicionTipo}";
                string resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<CentroCostoPEspecificoFiltroDTO>>(resultado)!;
                return new List<CentroCostoPEspecificoFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroPorTipo(): {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Centro Costo para filtro por tipo
        /// </summary>
        /// <param></param>
        /// <returns>Objeto</returns>
        public async Task<IEnumerable<CentroCostoPEspecificoFiltroDTO>> ObtenerFiltroPorTipoAsync(bool aplicaTipo)
        {
            try
            {
                string codicionTipo = aplicaTipo ? "AND Tipo = 1" : string.Empty;
                string query = $@"
                    SELECT DISTINCT
	                    IdCentroCosto AS Id, 
						CentroCosto AS Nombre, 
						IdPEspecifico
                    FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
                    WHERE
	                    Estado = 1
	                    AND RowNumber = 1 {codicionTipo}";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<CentroCostoPEspecificoFiltroDTO>>(resultado)!;
                return new List<CentroCostoPEspecificoFiltroDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltroPorTipoAsync(): {ex.Message}");
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 21/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de CentroCosto para mostrarse en combo.
        /// </summary>
        /// <returns> List<CentroCostoComboDTO> </returns>
        public List<ComboDTO> ObtenerCentroCostoWebinar()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM pla.V_TCentroCosto_Webinar WHERE Estado= 1 ORDER BY Id DESC;";
                var CentroCosto = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ComboDTO>>(CentroCosto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// <summary>
		/// Retorna datos de centro de costo para programa especifico
		/// </summary>
		/// <param name="codigo"></param>
		/// <param name="condicion"></param>
		/// <param name="anio"></param>
		/// <param name="nombreCiudad"></param>
		/// <returns>Lista de centros de costo</returns>
		public IEnumerable<CentroCostoDTO> ObtenerCentroCostoParaPEspecifico(string codigo, string condicion, string anio, string nombreCiudad)
        {
            try
            {
                var query = $@"
					SELECT
						Id,
						IdArea,
						IdSubArea,
						IdPgeneral,
						Nombre,
						Codigo,
						IdAreaCc,
						Ismtotales,
						Icpftotales
					FROM pla.V_TCentroCosto_ObtenerDatos 
					WHERE Estado =1 
						AND Nombre LIKE @Codigo
						AND Nombre LIKE @Anio
						AND Nombre LIKE @nombreCiudad
						AND {condicion}";
                var resultado = _dapperRepository.QueryDapper(query, new { Codigo = $"%{codigo}%", Anio = $"%{anio}%", NombreCiudad = $"%{nombreCiudad}%" });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                    return JsonConvert.DeserializeObject<IEnumerable<CentroCostoDTO>>(resultado)!;
                return new List<CentroCostoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCentroCostoParaPEspecifico: {ex.Message}", ex);
            }
        }
        public string? ObtenerUltimoCentroCostoPorCodigo(string codigo)
        {
            try
            {
                string query = "SELECT Codigo AS Valor FROM pla.V_TCentroCosto_ObtenerDatos WHERE Estado=1 AND Codigo LIKE @Codigo ORDER BY Codigo DESC;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Codigo = $"%{codigo}%" });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<StringDTO>(resultado)!;
                    return rpta.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerUltimoCentroCostoPorCodigo: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/05/2023
        /// <summary>
		/// Retorna datos de centro de costo para programa especifico
		/// </summary>
		/// <returns>Lista de centros de costo</returns>
		public int ObtenerUltimoIdCentroCosto()
        {
            try
            {
                IntDTO rpta = new();
                var query = "SELECT TOP 1 id AS Valor FROM pla.T_CentroCosto ORDER BY id DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;

                return rpta.Valor!.Value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerUltimoIdCentroCosto: {ex.Message}", ex);
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 29/05/2023
        /// <summary>
        /// Retorna datos de centro de costo para programa especifico
        /// </summary>
        /// <returns>Lista de centros de costo</returns>

        public List<CentroCostoDTO> Obtener()
        {
            try
            {
                var rpta = new List<CentroCostoDTO>();
                var query = "SELECT id, IdArea, IdSubArea, IdPGeneral, Nombre, Codigo, IdAreaCC, ISMTotales, ICPFTotales FROM pla.T_CentroCosto WHERE Estado=1 AND DATEPART(YEAR, FechaCreacion) > DATEPART(YEAR, GETDATE()) - 3  ORDER BY id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null" && resultado != "[]")
                    rpta = JsonConvert.DeserializeObject<List<CentroCostoDTO>>(resultado)!;

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Obtener el centro de costo: {ex.Message}", ex);
            }
        }

        /// Autor: Gretel Canasa
        /// Fecha: 29/05/2023
        /// <summary>
        /// Retorna datos de centro de costo para programa especifico
        /// </summary>
        /// <returns>Lista de centros de costo</returns>

        public List<CentroCostoUsuariosDTO> ObtenerCcDatosUsuarios()
        {
            try
            {
                var rpta = new List<CentroCostoUsuariosDTO>();
                var query = "SELECT  Id, Nombre, Codigo, FechaCreacion, UsuarioCreacion, FechaModificacion, UsuarioModificacion FROM pla.T_CentroCosto WHERE Estado=1 ORDER BY id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null" && resultado != "[]")
                    rpta = JsonConvert.DeserializeObject<List<CentroCostoUsuariosDTO>>(resultado)!;

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Obtener el centro de costo: {ex.Message}", ex);
            }
        }
        /// Autor: Gretel Canasa
        /// Fecha: 29/05/2023
        /// <summary>
        /// Retorna datos de centro de costo para programa especifico
        /// </summary>
        /// <returns>Lista de centros de costo</returns>

        public CentroCostoMasAdicionalesDTO ObtenerMasAdicionales(int id)
        {
            try
            {
                var rpta = new CentroCostoMasAdicionalesDTO();
                var query = "pla.SP_ObtenerCentroCostoMasAdicionalesPorId";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdCentroCosto = id });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null" && resultado != "[]")
                    rpta = JsonConvert.DeserializeObject<CentroCostoMasAdicionalesDTO>(resultado)!;

                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en Obtener el centro de costo: {ex.Message}", ex);
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 21/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de CentroCosto padres para mostrarse en combo.
        /// </summary>
        /// <returns> List<CentroCostoProgramaEspecificoFiltroDTO> </returns>

        public List<CentroCostoProgramaEspecificoFiltroDTO> ObtenerCentroCostoPadres(int? tipo)
        {
            try
            {
                var query = "";
                List<CentroCostoProgramaEspecificoFiltroDTO> Listado = new List<CentroCostoProgramaEspecificoFiltroDTO>();
                if (tipo.HasValue)
                {
                    query = $@"SELECT DISTINCT IdCentroCosto AS Id, CentroCosto AS Nombre, IdPEspecifico
					FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
					WHERE Estado = 1 AND RowNumber = 1 AND Tipo = 1;";
                }
                else
                {
                    query = $@"SELECT DISTINCT IdCentroCosto AS Id, CentroCosto AS Nombre, IdPEspecifico
					FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
					WHERE Estado = 1 AND RowNumber = 1
					";
                }
                var res = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    Listado = JsonConvert.DeserializeObject<List<CentroCostoProgramaEspecificoFiltroDTO>>(res);
                }
                return Listado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez 
		/// Fecha:31/01/2024
		/// Version: 1
		/// <summary>
		/// Obtiene CentroCosto dado un nombre
		/// </summary>
		/// <param name="nombreCentroCosto">Nombre del Centro de costo</param>
		/// <returns>Retorna Objeto CentroCostoPEspecificoDTO con Informacion de Centro de costo</returns>
		public CentroCostoPEspecificoDTO ObtenerCentrosCostoPorNombre(string nombreCentroCosto)
        {
            try
            {
                CentroCostoPEspecificoDTO centroCosto = new CentroCostoPEspecificoDTO();
                var query = "SELECT Id as IdCentroCosto, Nombre as NombreCentroCosto FROM pla.T_CentroCosto WHERE Estado=1 AND Nombre = @nombreCentroCosto";
                var centroCostoDB = _dapperRepository.FirstOrDefault(query, new { nombreCentroCosto });
                centroCosto = JsonConvert.DeserializeObject<CentroCostoPEspecificoDTO>(centroCostoDB);

                return centroCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 08/02/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de CentroCosto en lanzamiento y por ejecucion
        /// </summary>
        /// <returns> List<CentroCostoComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCentroCostoAgenda()
        {
            try
            {
                List<ComboDTO> rpta = new();
                var query = @"SELECT CC.Id AS Id, CC.Nombre
	                            FROM pla.T_PEspecifico AS PE
	                            INNER JOIN pla.T_CentroCosto AS CC ON CC.ID = PE.IdCentroCosto
		                            AND CC.Estado = 1
	                            WHERE PE.Estado = 1 AND EstadoPId IN (3, 5)
		                            AND ISNULL(PE.EsEspecial, 0) <> 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Obtener información de centro de Costo AutoComplete
        /// </summary>
        /// <param name="valor"> valores de búsqueda </param>
        /// <returns> Lista de registros de Centros de Costo Registrados </returns>
        /// <returns> Objeto DTO: List<CentroCostoFiltroAutocompleteDTO> </returns>	
        public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoCompleteInstituto(string valor)
        {
            try
            {
                List<CentroCostoFiltroAutocompleteDTO> centrosCostoAutocompleteFiltro = new List<CentroCostoFiltroAutocompleteDTO>();
                string queryCentroCostoFiltro = string.Empty;
                queryCentroCostoFiltro = "SELECT Id, Nombre from pla.[V_TCentroCosto_ParaFiltroV2] WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1 AND IdTipoProgramaCarrera =2 ORDER By Nombre ASC";
                var CentroCostoDB = _dapperRepository.QueryDapper(queryCentroCostoFiltro, new { valor });
                if (!string.IsNullOrEmpty(CentroCostoDB) && !CentroCostoDB.Contains("[]"))
                {
                    centrosCostoAutocompleteFiltro = JsonConvert.DeserializeObject<List<CentroCostoFiltroAutocompleteDTO>>(CentroCostoDB);
                }
                return centrosCostoAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtener información de centro de Costo AutoComplete
        /// </summary>
        /// <param name="valor"> valores de búsqueda </param>
        /// <returns> Lista de registros de Centros de Costo Registrados </returns>
        /// <returns> Objeto DTO: List<CentroCostoFiltroAutocompleteDTO> </returns>		
        public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor)
        {
            try
            {
                List<CentroCostoFiltroAutocompleteDTO> centrosCostoAutocompleteFiltro = new List<CentroCostoFiltroAutocompleteDTO>();
                string queryCentroCostoFiltro = string.Empty;
                queryCentroCostoFiltro = "SELECT Id, Nombre from pla.V_TCentroCosto_ParaFiltro WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By Nombre ASC";
                var CentroCostoDB = _dapperRepository.QueryDapper(queryCentroCostoFiltro, new { valor });
                if (!string.IsNullOrEmpty(CentroCostoDB) && !CentroCostoDB.Contains("[]"))
                {
                    centrosCostoAutocompleteFiltro = JsonConvert.DeserializeObject<List<CentroCostoFiltroAutocompleteDTO>>(CentroCostoDB);
                }
                return centrosCostoAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int? ObtenerIdPorNombre(string nombre)
        {
            try
            {
                string query = "SELECT Id as Valor FROM pla.T_CentroCosto WHERE Estado = 1 AND Nombre LIKE @Nombre";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Nombre = nombre });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<IntDTO>(resultado)!;
                    return rpta.Valor;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}