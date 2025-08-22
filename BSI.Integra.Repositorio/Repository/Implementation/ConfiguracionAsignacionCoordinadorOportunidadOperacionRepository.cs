using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionAsignacionCoordinadorOportunidadOperaciones
    /// </summary>
    public class ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository : GenericRepository<TConfiguracionAsignacionCoordinadorOportunidadOperacione>, IConfiguracionAsignacionCoordinadorOportunidadOperacionRepository
    {
        private Mapper _mapper;

        public ConfiguracionAsignacionCoordinadorOportunidadOperacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionAsignacionCoordinadorOportunidadOperacione, ConfiguracionAsignacionCoordinadorOportunidadOperacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TConfiguracionAsignacionCoordinadorOportunidadOperacione MapeoEntidad(ConfiguracionAsignacionCoordinadorOportunidadOperacion entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionAsignacionCoordinadorOportunidadOperacione modelo = _mapper.Map<TConfiguracionAsignacionCoordinadorOportunidadOperacione>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionAsignacionCoordinadorOportunidadOperacione Add(ConfiguracionAsignacionCoordinadorOportunidadOperacion entidad)
        {
            try
            {
                var ConfiguracionAsignacionCoordinadorOportunidadOperacion = MapeoEntidad(entidad);
                base.Insert(ConfiguracionAsignacionCoordinadorOportunidadOperacion);
                return ConfiguracionAsignacionCoordinadorOportunidadOperacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionAsignacionCoordinadorOportunidadOperacione Update(ConfiguracionAsignacionCoordinadorOportunidadOperacion entidad)
        {
            try
            {
                var ConfiguracionAsignacionCoordinadorOportunidadOperacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionAsignacionCoordinadorOportunidadOperacion.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionAsignacionCoordinadorOportunidadOperacion);
                return ConfiguracionAsignacionCoordinadorOportunidadOperacion;
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


        public IEnumerable<TConfiguracionAsignacionCoordinadorOportunidadOperacione> Add(IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacion> listadoEntidad)
        {
            try
            {
                List<TConfiguracionAsignacionCoordinadorOportunidadOperacione> listado = new List<TConfiguracionAsignacionCoordinadorOportunidadOperacione>();
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

        public IEnumerable<TConfiguracionAsignacionCoordinadorOportunidadOperacione> Update(IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionAsignacionCoordinadorOportunidadOperacione> listado = new List<TConfiguracionAsignacionCoordinadorOportunidadOperacione>();
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
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuracion por PEspecifico
        /// </summary>
        /// <param name="idPEspecifico">Id del Programa Especifico</param>
        /// <returnsList<ConfiguracionCoordinadoraCentroCostoDTO></returns>
        public List<ConfiguracionCoordinadorCentroCostoDTO> ObtenerPorIdPEspecifico(int idPespecifico)
        {
            try
            {
                List<ConfiguracionCoordinadorCentroCostoDTO> configuracionCoordinador = new List<ConfiguracionCoordinadorCentroCostoDTO>();

                var query = @"SELECT DISTINCT 
                                IdPersonal,
                                UsuarioPersonal,
                                IdEstadoMatricula,
                                IdSubEstadoMatricula
                            FROM ope.V_TConfiguracionAsignacionCoordinadorOportunidadOperaciones_ObtenerConfiguracionPorCentroCosto 
                            WHERE Estado = 1 AND IdPEspecifico = @IdPEspecifico";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPespecifico = idPespecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    configuracionCoordinador = JsonConvert.DeserializeObject<List<ConfiguracionCoordinadorCentroCostoDTO>>(resultado)!;
                }
                else
                {
                    query = @"SELECT DISTINCT 
                                    IdPersonal, 
                                    UsuarioPersonal,
                                    IdEstadoMatricula,
                                    IdSubEstadoMatricula
                                FROM ope.V_TConfiguracionAsignacionCoordinadorOportunidadOperaciones_ObtenerConfiguracionPorCentroCosto
                                WHERE Estado = 1 AND IdPEspecificoHijo = @IdPEspecifico";
                    resultado = _dapperRepository.QueryDapper(query, new { IdPespecifico = idPespecifico });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    {
                        configuracionCoordinador = JsonConvert.DeserializeObject<List<ConfiguracionCoordinadorCentroCostoDTO>>(resultado)!;
                    }
                }
                return configuracionCoordinador;
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
        /// Obtener Configuracion Coordinadores
        /// </summary>
        /// <param></param>
        /// <returns>List<ConfiguracionCentroCostoCoordinadorDTO></returns>
        public List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerConfiguracionCoordinadores()
        {
            try
            {
                var query = @"SELECT 
                                Id, IdPersonal, Personal,IdEstadoMatricula,EstadoMatricula,IdSubEstadoMatricula,SubEstadoMatricula, IdCentroCosto, 
                                IdProgramaEspecifico, CentroCosto, ProgramaEspecifico, EstadoProgramaEspecifico, Tipo, FechaCreacion, EsAsignado 
                            FROM 
                                [pla].[V_TConfiguracionAsignacionCoordinadorOportunidadOperaciones_ObtenerConfiguracion] 
                            WHERE 
                                EsAsignado = 1 ORDER BY FechaCreacion DESC";
                var respuesta = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ConfiguracionCentroCostoCoordinadorDTO>>(respuesta)!;
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
		/// Obtiene todos los centros de costo sin asignacion de coordinadora
		/// </summary>
		/// <param></param>
		/// <returns>List<ConfiguracionCentroCostoCoordinadorDTO></returns>
		public List<ConfiguracionCentroCostoCoordinadorDTO> ObtenerCentroCostoSigAsignacion()
        {
            try
            {
                var query = @"SELECT 
                                Id, IdPersonal, IdCentroCosto, IdProgramaEspecifico, CentroCosto, ProgramaEspecifico, EstadoProgramaEspecifico, 
                                Tipo, FechaCreacion, EsAsignado 
                            FROM 
                                [pla].[V_TConfiguracionAsignacionCoordinadorOportunidadOperaciones_ObtenerConfiguracion] 
                            WHERE 
                                EsAsignado = 0 AND  CentroCosto NOT LIKE '%WEBINAR%' ORDER BY FechaCreacion DESC";
                var respuesta = _dapperRepository.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<ConfiguracionCentroCostoCoordinadorDTO>>(respuesta)!;
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
		/// Obtiene Centro de Costos Hijo
		/// </summary>
		/// <param name="idCentroCosto">Id de Centro Costo</param>
		/// <returnsList<CentroCostoHijoDTO></returns>
		public List<CentroCostoHijoDTO> ObtenerCentroCostoHijos(int idCentroCosto)
        {
            try
            {
                List<CentroCostoHijoDTO> centroCostoHijo = new List<CentroCostoHijoDTO>();
                var query = @"SELECT
                                IdCentroCosto, PEspecificoPadreId, IdCentroCostoHijo, PEspecificoHijoId	
                            FROM 
                                [pla].[V_TCentroCosto_ObtenerCentroCostoHijo] 
                            WHERE 
                                Estado = 1 AND IdCentroCosto = @IdCentroCosto";
                var respuesta = _dapperRepository.QueryDapper(query, new { IdCentroCosto = idCentroCosto });
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    centroCostoHijo = JsonConvert.DeserializeObject<List<CentroCostoHijoDTO>>(respuesta)!;
                }
                return centroCostoHijo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 04/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Personal asociado al identificador.
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> List<PersonalDTO> </returns>
        public IEnumerable<ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO> ObtenerPorIdPersonal(int idPersonal)
        {
            try
            {
                List<ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO> respuesta = new List<ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO>();
                var query = @"SELECT 
                                Id, IdPersonal, IdCentroCosto, IdCentroCostoHijo, IdMigracion, IdEstadoMatricula, IdSubEstadoMatricula 
                            FROM 
                                ope.T_ConfiguracionAsignacionCoordinadorOportunidadOperaciones
                            WHERE 
                                Estado = 1 AND IdPersonal = @IdPersonal";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ConfiguracionAsignacionCoordinadorOportunidadOperacionDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
