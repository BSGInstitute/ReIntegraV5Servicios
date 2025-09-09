using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: SesionConfigurarVideoRepository
    /// Autor: Gilmer Qm
    /// Fecha: 13/07/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_SesionConfigurarVideo
    /// </summary>
    public class SesionConfigurarVideoRepository : GenericRepository<TSesionConfigurarVideo>, ISesionConfigurarVideoRepository
    {
        private Mapper _mapper;

        public SesionConfigurarVideoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSesionConfigurarVideo, SesionConfigurarVideo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSesionConfigurarVideo MapeoEntidad(SesionConfigurarVideo entidad)
        {
            try
            {
                //crea la entidad padre
                TSesionConfigurarVideo perfilScoringCargo = _mapper.Map<TSesionConfigurarVideo>(entidad);

                return perfilScoringCargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSesionConfigurarVideo Add(SesionConfigurarVideo entidad)
        {
            try
            {
                var perfilScoringCargo = MapeoEntidad(entidad);
                base.Insert(perfilScoringCargo);
                return perfilScoringCargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSesionConfigurarVideo Update(SesionConfigurarVideo entidad)
        {
            try
            {
                var perfilScoringCargo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilScoringCargo.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilScoringCargo);
                return perfilScoringCargo;
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

        public IEnumerable<TSesionConfigurarVideo> Add(IEnumerable<SesionConfigurarVideo> listadoEntidad)
        {
            try
            {
                List<TSesionConfigurarVideo> listado = new List<TSesionConfigurarVideo>();
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

        public IEnumerable<TSesionConfigurarVideo> Update(IEnumerable<SesionConfigurarVideo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSesionConfigurarVideo> listado = new List<TSesionConfigurarVideo>();
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
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Version: 1.0
        /// <summary>
        /// Actualiza el IdConfigurarVideoPrograma de SesionConfigurarVideo
        /// </summary>
        /// <param name="idConfiguracionAntiguo"> (PK de la tabla pla.T_ConfigurarVideoPrograma) Antiguo </param>
        /// <param name="idConfiguracionNuevo"> (PK de la tabla pla.T_ConfigurarVideoPrograma) Nuevo </param>
        /// <returns> </returns>
        public void ActualizarPadreSesionConfiguracionVideo(int idConfiguracionAntiguo, int idConfiguracionNuevo)
        {
            try
            {
                var query = _dapperRepository.QuerySPFirstOrDefault("[pla].[SP_ActualizarSesionConfiguracionVideo]", new { IdConfigurarVideoProgramaAnterior = idConfiguracionAntiguo, IdConfigurarVideoProgramaNuevo = idConfiguracionNuevo });

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de registros de sesion de la configuracion de video
        /// </summary>
        /// <param name="idConfigurarVideoPrograma">ID de la configuracion del video del programa (PK de la tabla pla.T_ConfigurarVideoPrograma)</param>
        /// <returns>Retorna una lista de objetos (RegistroSesionConfigurarVideoBO)</returns>
        public List<SesionConfigurarVideoDTO> ObtenerPorIdConfigurarVideoPrograma(int idConfigurarVideoPrograma)
        {
            string query = @"SELECT Id,
                                   IdConfigurarVideoPrograma,
                                   Minuto,
                                   IdTipoVista,
                                   NroDiapositiva,
                                   IdEvaluacion,
                                   ISNULL(ConLogoVideo, 0) AS ConLogoVideo,
                                   ISNULL(ConLogoDiapositiva, 0) AS ConLogoDiapositiva
                            FROM pla.T_SesionConfigurarVideo
                            WHERE Estado = 1
                                  AND IdConfigurarVideoPrograma = @IdConfigurarVideoPrograma;";
            string queryDb = _dapperRepository.QueryDapper(query, new { IdConfigurarVideoPrograma = idConfigurarVideoPrograma });
            if (!string.IsNullOrEmpty(queryDb) && !queryDb.Equals("[]"))
                return JsonConvert.DeserializeObject<List<SesionConfigurarVideoDTO>>(queryDb);
            return new List<SesionConfigurarVideoDTO>();
        }
        /// Autor: Gilmer Qm
        /// Fecha: 13/07/2023
        /// Version: 1.0
        /// <summary>
        /// Eliminacion lógica Sesiones Configuracion Video segun Id Programa General
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>        
        /// <param name="usuario"> Usuario autor de Integra </param>        
        /// <returns> IntDTO </returns>
        public IntDTO EliminarSesionesConfiguracionVideo(int idPGeneral, string usuario)
        {
            try
            {
                var query = _dapperRepository.QuerySPFirstOrDefault("[pla].[SP_EliminarConfiguracionesVideoProgramaGeneral]", new { IdProgramaGeneral = idPGeneral, Usuario = usuario });
                var rpta = JsonConvert.DeserializeObject<IntDTO>(query);
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 17/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <param name="id"> (PK) </param>             
        /// <returns> SesionConfigurarVideo </returns>
        public SesionConfigurarVideo ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT  Id,
                                    IdConfigurarVideoPrograma,
                                    Minuto,
                                    IdTipoVista,
                                    NroDiapositiva,
                                    IdEvaluacion,
                                    Estado,
                                    FechaCreacion,
                                    FechaModificacion,
                                    UsuarioCreacion,
                                    UsuarioModificacion,
                                    RowVersion,
                                    IdMigracion,
                                    ConLogoVideo,
                                    ConLogoDiapositiva
                            FROM pla.T_SesionConfigurarVideo
                            WHERE Estado = 1
                                  AND Id = @Id;";
                string queryDb = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(queryDb) && !queryDb.Equals("null"))
                    return JsonConvert.DeserializeObject<SesionConfigurarVideo>(queryDb);
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 01/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros asociados al IdPGeneral (T_ConfigurarVideoPrograma)   
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>             
        /// <returns> IEnumerable<InformacionSesionConfigurarVideoDTO> </returns>
        public IEnumerable<InformacionSesionConfigurarVideoDTO> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = @"SELECT Id,
                                       IdConfigurarVideoPrograma,
                                       IdPGeneral,
                                       Minuto,
                                       IdTipoVista,
                                       NroDiapositiva,
                                       ConLogoVideo,
                                       ConLogoDiapositiva
                                FROM [pla].[V_ListadoSesionConfigurarVideoPorPGeneral]
                                WHERE IdPGeneral = @idPGeneral;";
                var queryDb = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(queryDb) && !queryDb.Equals("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<InformacionSesionConfigurarVideoDTO>>(queryDb);
                else
                    return new List<InformacionSesionConfigurarVideoDTO>();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public bool ActualizarDescargaReproduccionVideo(ActualizarDescargaReproduccionDTO dto, string usuario)
        {
            try
            {
                var query = "pla.SP_ConfigurarVideoPrograma_ActualizarReproduccionDescarga";
                var parametros = new
                {
                    IdPgeneral = dto.IdPgeneral,
                    ReproduccionVideo = dto.ReproduccionVideo,
                    DescargaVideo = dto.DescargaVideo,
                    UsuarioModificacion = usuario
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarFormularioRegularizado() {ex.Message}", ex);
            }

        }



        public ConfigurarConteodeVideosPorTipo ObtenerConteosdeVideosTipo(int idPGeneral)
        {
            try
            {
                ConfigurarConteodeVideosPorTipo rpta = new ConfigurarConteodeVideosPorTipo();
                var query = @"
                    SELECT
	                   IdPGeneral,
                       CantidadDescarga_Brightcove,
                       CantidadDescarga_Vimeo,
                       CantidadReproduccion_Brightcove,
                       CantidadReproduccion_Vimeo
                    FROM pla.V_ObtenerConfiguracionVideoMasivoPGeneral where IdPGeneral = @idPGeneral";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPGeneral = idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<ConfigurarConteodeVideosPorTipo>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
