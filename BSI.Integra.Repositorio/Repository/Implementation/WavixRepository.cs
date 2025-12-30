using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.Wolkbox;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    public class WavixRepository : IWavixRepository
    {

        private IDapperRepository _dapperRepository;

        public WavixRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor:Joseph Llanque
        /// Fecha: 21/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Configuracion wavix por personal
        /// </summary>
        /// <returns> WavixPersonalDTO </returns>
        public WavixPersonalDTO? GetUserAccess(int idPersonal)
        {
            try
            {
                var query = @"
                   SELECT  Id,
                            IdPersonal,
                            IdWavixAPICredencial,
                            IdSipTrunk,
                            UrlServer,
                            VersionWavix
                    FROM conf.T_PersonalWavix
                    WHERE IdPersonal = @IdPersonal
                      AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<WavixPersonalDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 21/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Configuracion wavix por personal
        /// </summary>
        /// <returns> WavixPersonalDTO </returns>
        public List<NumeroAsesorWavixDTO>? GetNumberByUser(int idPersonal)
        {
            try
            {
                var query = @"
                   SELECT
	                    IdPersonal,
	                    NombreAsesor,
                        IdSipTrunk,
                        UrlServer,
                        IdPais,
                        Numero,
                        Predeterminado
                    FROM [conf].[V_ObtenerNumeroConfiguradoAsesorWavix]
                    WHERE IdPersonal = @IdPersonal";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<NumeroAsesorWavixDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor:Joseph Llanque
        /// Fecha: 21/10/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene Configuracion wavix por personal
        /// </summary>
        /// <returns> WavixPersonalDTO </returns>
        /// 
        public EstadoLlamadaDTO? ObtenerEstadoUltimaLlamada(int idPersonal, int idOportunidad, int idActividadDetalle, int idAlumno, int nroIntentoLlamada)
        {

            try
            {
                var rpta = new EstadoLlamadaDTO();

                var query = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenerEstadoLlamadaWavix", new
                {
                    IdPersonal = idPersonal,
                    IdOportunidad = idOportunidad,
                    IdActividadDetalle = idActividadDetalle,
                    IdAlumno = idAlumno,
                    NroIntentoLlamada = nroIntentoLlamada
                });
                if (!string.IsNullOrEmpty(query) && query != "null")
                {
                    rpta = JsonConvert.DeserializeObject<EstadoLlamadaDTO>(query)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Obtiene el API Key de Wavix asociado al personal
        /// </summary>
        /// <param name="idPersonal">ID del personal</param>
        /// <returns>API Key en texto plano</returns>
        public string ObtenerApiKeyPorPersonal(int idPersonal)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPFirstOrDefault("conf.SP_ObtenerApiKeyPorPersonal", new { IdPersonal = idPersonal });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultado);
                    return (string)obj.ApiKey;
                }

                throw new Exception($"No se encontró API Key activa para el personal: {idPersonal}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener API Key por personal: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Guarda el token diario en la base de datos
        /// TODO: Cuando esté listo el SP, cambiar por: _dapperRepository.QuerySPFirstOrDefault("conf.SP_GuardarTokenWavix", params)
        /// </summary>
        public int GuardarTokenDiario(int idPersonalWavix, string tokenUuid, string token, DateTime fechaExpiracion, string usuario)
        {

            try
            {
                var resultado = _dapperRepository.QuerySPDapper("conf.SP_GuardarTokenWavix", new
                {
                    IdPersonalWavix = idPersonalWavix,
                    TokenUuid = tokenUuid,
                    Token = token,
                    FechaExpiracion = fechaExpiracion,
                    Usuario = usuario
                });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var obj = JsonConvert.DeserializeObject<dynamic>(resultado);
                    return (int)obj.Id;
                }

                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar token diario: {ex.Message}", ex);
            }
        }
        public string ObtenerTokenActivo(int idPersonal)
        {
            try {

                var resultado = _dapperRepository.QuerySPDapper("conf.ObtenerTokenActivo", new { IdPersonal = idPersonal });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<string>(resultado); ;
                }

                throw new Exception($"No se encontro un token del dia para del personal  {idPersonal}");

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public TokenVigenteDTO ObtenerTokenVigente(int idPersonalWavix)
        {
            try {
                var query = @"SELECT TOP 1 * FROM conf.T_WavixTokenDiario 
                WHERE IdPersonalWavix = @idPersonalwavix AND EstaActivo = 1  AND FechaExpiracion > GETDATE() ";

                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonalwavix = idPersonalWavix });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TokenVigenteDTO>(resultado);
                }
                return null;
            } 
            catch (Exception ex) {
                throw ex;
            }
        }


    }

}
