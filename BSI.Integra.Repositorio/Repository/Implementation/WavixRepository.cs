using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.Wolkbox;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
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
    public class WavixRepository :IWavixRepository
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
                            IdSipTrunk,
                            UrlServer 
                    FROM conf.T_PersonalWavix
                    WHERE IdPersonal = @IdPersonal";
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
        public IEnumerable<NumeroAsesorWavixDTO>? GetConfigurationTrunks()
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
                    FROM [conf].[V_ObtenerNumeroConfiguradoAsesorWavix]";
                var resultado = _dapperRepository.QueryDapper(query, new { });
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
                //throw new Exception($"#OR-ORCAA-001@Error en ObtenerReporteControlActividadesAgenda: {ex.Message}", ex);
            }



        }
    }
}
