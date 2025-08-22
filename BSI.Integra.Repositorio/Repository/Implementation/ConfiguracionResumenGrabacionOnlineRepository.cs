
using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionResumenGrabacionOnlineRepository
    /// Autor: Jorge Gamero
    /// Fecha: 28/01/2025
    /// <summary>
    /// Gestión general de T_ConfiguracionResumenGrabacionOnline
    /// </summary>
    public class ConfiguracionResumenGrabacionOnlineRepository : GenericRepository<TConfiguracionResumenGrabacionOnline>, IConfiguracionResumenGrabacionOnlineRepository
    {
        private Mapper _mapper;

        public ConfiguracionResumenGrabacionOnlineRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionResumenGrabacionOnline, ConfiguracionResumenGrabacionOnline>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TConfiguracionResumenGrabacionOnline MapeoEntidad(ConfiguracionResumenGrabacionOnline entidad)
        {
            try
            {
                TConfiguracionResumenGrabacionOnline modelo = _mapper.Map<TConfiguracionResumenGrabacionOnline>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TConfiguracionResumenGrabacionOnline> Add(IEnumerable<ConfiguracionResumenGrabacionOnline> listadoEntidad)
        {
            try
            {
                List<TConfiguracionResumenGrabacionOnline> listado = new List<TConfiguracionResumenGrabacionOnline>();
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

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza activando registro de T_ConfiguracionResumenGrabacionOnline
        /// </summary>
        /// <returns> bool </returns>
        public bool ActualizaActivo(IEnumerable<int> listadoIds, int idPEspecificoSesion, string usuario)
        {
            try
            {
                if (listadoIds == null || !listadoIds.Any())
                {
                    return false;
                }
                var query = @"UPDATE pla.T_ConfiguracionResumenGrabacionOnline
                              SET Estado = 1, UsuarioModificacion = @usuario, FechaModificacion = GETDATE()
                              WHERE IdPEspecificoSesion = @idPEspecificoSesion
                              AND Id IN @listadoIds";
                var parametros = new { idPEspecificoSesion, listadoIds, usuario };
                var filasAfectadas = _dapperRepository.QueryDapper(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Actualiza desactivando registro de T_ConfiguracionResumenGrabacionOnline
        /// </summary>
        /// <returns> bool </returns>
        public bool ActualizaInactivo(IEnumerable<int> listadoIds, int idPEspecificoSesion, string usuario)
        {
            try
            {
                if (listadoIds == null || !listadoIds.Any())
                {
                    return false;
                }
                var query = @"UPDATE pla.T_ConfiguracionResumenGrabacionOnline
                              SET Estado = 0, UsuarioModificacion = @usuario, FechaModificacion = GETDATE()
                              WHERE IdPEspecificoSesion = @idPEspecificoSesion
                              AND Id IN @listadoIds";
                var parametros = new { idPEspecificoSesion, listadoIds, usuario };
                var filasAfectadas = _dapperRepository.QueryDapper(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 01/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene configuración para resúmenes de videos de T_ConfiguracionResumenGrabacionOnline
        /// </summary>
        /// <returns> IEnumerable<ConfiguracionResumenGrabacionOnlineDTO> </returns>
        public IEnumerable<ConfiguracionResumenGrabacionOnlineDTO> ObtenerConfiguracionResumenGrabacionOnlinePorSesion(int idPEspecificoSesion)
        {
            try
            {
                var rpta = new List<ConfiguracionResumenGrabacionOnlineDTO>();
                var query = @"SELECT
                    Id, IdPEspecificoSesion, IdResumenGrabacionOnline, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion
                    FROM pla.V_ConfiguracionResumenGrabacionOnline_Obtener
                    WHERE IdPEspecificoSesion = @IdPEspecificoSesion";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPEspecificoSesion = idPEspecificoSesion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionResumenGrabacionOnlineDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 07/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros para enviar resúmenes por correo
        /// </summary>
        /// <returns>  </returns>
        public IEnumerable<ConfiguracionResumenGrabacionesEnvioCorreoDTO> ObtenerConfiguracionResumenGrabacionesEnvioCorreo(int idPEspecificoSesion)
        {
            try
            {
                List<ConfiguracionResumenGrabacionesEnvioCorreoDTO> resumenGrabacionCorreo = new List<ConfiguracionResumenGrabacionesEnvioCorreoDTO>();
                var query = _dapperRepository.QuerySPDapper("[pla].[SP_ObtenerConfiguracionResumenGrabacionesEnvioCorreo]", new { idPEspecificoSesion });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    resumenGrabacionCorreo = JsonConvert.DeserializeObject<List<ConfiguracionResumenGrabacionesEnvioCorreoDTO>>(query);
                }
                return resumenGrabacionCorreo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 11/02/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros para enviar resúmenes por whatsApp
        /// </summary>
        /// <returns>  </returns>
        public IEnumerable<ConfiguracionResumenGrabacionesEnvioWhatsAppDTO> ObtenerConfiguracionResumenGrabacionesEnvioWhatsApp(int idPEspecificoSesion)
        {
            try
            {
                List<ConfiguracionResumenGrabacionesEnvioWhatsAppDTO> resumenGrabacionCorreo = new List<ConfiguracionResumenGrabacionesEnvioWhatsAppDTO>();
                var query = _dapperRepository.QuerySPDapper("[pla].[SP_ObtenerConfiguracionResumenGrabacionesEnvioWhatsApp]", new { idPEspecificoSesion });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    resumenGrabacionCorreo = JsonConvert.DeserializeObject<List<ConfiguracionResumenGrabacionesEnvioWhatsAppDTO>>(query);
                }
                return resumenGrabacionCorreo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 15/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id de registro en T_ProcesamientoSesionOnline filtrado por idPEspecificoSesion
        /// </summary>
        /// <returns> IEnumerable<ConfiguracionResumenGrabacionOnlineDTO> </returns>
        public IEnumerable<RegistroProcesamientoSesionOnlineDTO> ConsultaRegistroResumenPordPEspecificoSesion(int idPEspecificoSesion)
        {
            try
            {
                var rpta = new List<RegistroProcesamientoSesionOnlineDTO>();
                var query = @"SELECT
                    Id
                    FROM ia.T_ProcesamientoSesionOnline
                    WHERE IdPEspecificoSesion = @IdPEspecificoSesion AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPEspecificoSesion = idPEspecificoSesion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<RegistroProcesamientoSesionOnlineDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 15/04/2025
        /// Version: 1.0
        /// <summary>
        /// Elimina registro de tabla T_ProcesamientoSesionOnline de manera lógica por filtro de id
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminaProcesamientoSesionOnlinePorIdPEspecificoSesion(int id, string usuario)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return false;
                }
                var query = @"UPDATE ia.T_ProcesamientoSesionOnline
                              SET Estado = 0, UsuarioModificacion = @usuario, FechaModificacion = GETDATE()
                              WHERE Id = @id AND Estado = 1";
                var parametros = new { id, usuario };
                var filasAfectadas = _dapperRepository.QueryDapper(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 15/04/2025
        /// Version: 1.0
        /// <summary>
        /// Elimina registro de tabla T_ProcesamientoTipoGenerar de manera lógica por filtro de idProcesamientoSesionOnline
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminaPProcesamientoTipoGenerarPorIdPEspecificoSesion(int idProcesamientoSesionOnline, string usuario)
        {
            try
            {
                if (idProcesamientoSesionOnline == null || idProcesamientoSesionOnline == 0)
                {
                    return false;
                }
                var query = @"UPDATE ia.T_ProcesamientoTipoGenerar
                              SET Estado = 0, UsuarioModificacion = @usuario, FechaModificacion = GETDATE()
                              WHERE IdProcesamientoSesionOnline = @idProcesamientoSesionOnline AND Estado = 1";
                var parametros = new { idProcesamientoSesionOnline, usuario };
                var filasAfectadas = _dapperRepository.QueryDapper(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 15/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene campo TextoTranscripcion de tabla T_ProcesamientoSesionOnline filtrado por id
        /// </summary>
        /// <returns> string </returns>
        public string ObtenerTextoTranscripcionPorId(int id)
        {
            try
            {
                if (id == 0)
                    return null;

                var query = @"SELECT TextoTranscripcion FROM ia.T_ProcesamientoSesionOnline WHERE Id = @id";
                var parametros = new { id };

                // Este método devuelve un JSON (string)
                var resultadoJson = _dapperRepository.QueryDapper(query, parametros);

                // Deserializar el JSON
                var lista = JsonConvert.DeserializeObject<List<TextoTranscripcionDTO>>(resultadoJson);
                return lista?.FirstOrDefault()?.TextoTranscripcion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 15/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene campo TextoGuionAudio de tabla T_ProcesamientoSesionOnline filtrado por id
        /// </summary>
        /// <returns> string </returns>
        public string ObtenerTextoGuionAudioPorId(int id)
        {
            try
            {
                if (id == 0)
                    return null;

                var query = @"SELECT TextoGuionAudio FROM ia.T_ProcesamientoSesionOnline WHERE Id = @id";
                var parametros = new { id };

                // Este método devuelve un JSON (string)
                var resultadoJson = _dapperRepository.QueryDapper(query, parametros);

                // Deserializar el JSON
                var lista = JsonConvert.DeserializeObject<List<TextoGuionAudioDTO>>(resultadoJson);
                return lista?.FirstOrDefault()?.TextoGuionAudio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
