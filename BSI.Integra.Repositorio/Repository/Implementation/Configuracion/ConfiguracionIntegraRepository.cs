using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Configuracion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Configuracion
{
    /// Repositorio: ConfiguracionIntegraRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 30/01/2024
    /// <summary>
    /// Gestión general de T_ConfiguracionIntegra
    /// </summary>
    public class ConfiguracionIntegraRepository : IConfiguracionIntegraRepository
    {
        private IDapperRepository _dapperRepository;
        public ConfiguracionIntegraRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado de validacion de ips
        /// </summary>
        /// <returns> bool => estado validacion ip </returns>
        public bool ObtenerEstadoValidacionIp()
        {
            try
            {
                var query = @"SELECT Activo AS Valor FROM conf.T_ConfiguracionIntegra WHERE Tipo='Estado' AND Clave1='EstadoValidacionIp' AND Estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var rpta = JsonConvert.DeserializeObject<BoolDTO>(resultado)!;
                    return rpta.Valor.GetValueOrDefault();
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CIR-OEVIp001@Error en ObtenerEstadoValidacionIp, {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las apis para la validacion por ips
        /// </summary>
        /// <returns> Lista de Apis </returns>
        public List<ClaveValorDTO> ObtenerApisValidacionIp()
        {   
            try
            {
                var rpta = new List<ClaveValorDTO>();
                //var query = @"SELECT Clave1 AS Clave, Valor1 AS Valor FROM conf.T_ConfiguracionIntegra WHERE Tipo='ValidacionIp' AND Estado=1 AND Activo = 1 ORDER BY OrdenPrioridad ASC";
                //var resultado = _dapperRepository.QueryDapper(query, null);
                //if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                //{
                //    rpta = JsonConvert.DeserializeObject<List<ClaveValorDTO>>(resultado)!;
                //}
                rpta.Add(new ClaveValorDTO { Clave = "ipify.org", Valor= "https://api.ipify.org?format=JSON" });
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CIR-OEVIp001@Error en ObtenerEstadoValidacionIp, {ex.Message}");
            }
        }
    }
}



