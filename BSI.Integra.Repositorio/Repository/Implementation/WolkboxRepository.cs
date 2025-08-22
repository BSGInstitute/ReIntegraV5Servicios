using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.Wolkbox;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: WolkboxRepository
    /// Autor: Flavio R.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_Wolkbox
    /// </summary>
    public class WolkboxRepository : IWolkboxRepository
    {
        private IDapperRepository _dapperRepository;

        public WolkboxRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// InsertarWolkboxTokenLog
        /// </summary>
        /// <returns> ResumenWolkboxDTO </returns>
        public void InsertarWolkboxTokenLog(WolkboxTokenLogDTO dto)
        {
            try
            {
                var query = "conf.SP_TWolkboxTokenLog_Insertar";
                _dapperRepository.QuerySPDapper(query, dto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// ObtenerWolkboxTokenPorIdPersonal
        /// </summary>
        /// <returns> ResumenWolkboxDTO </returns>
        public WolkboxTokenDTO? ObtenerWolkboxTokenPorIdPersonal(int idPersonal)
        {
            try
            {
                var query = @"
                    SELECT
                        TOP 1
	                    Id,
	                    IdPersonal,
	                    Token,
	                    AgentId,
	                    WolkvoxServer,
	                    Activo,
	                    Limite,
                        ContadorDia
                    FROM conf.V_TWolkboxToken
                    WHERE Estado = 1
                        AND Activo = 1
                        AND IdPersonal = @IdPersonal
                        AND EstadoReasignado = 0
                    ORDER BY PorDefecto DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPersonal = idPersonal });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<WolkboxTokenDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.
        /// Fecha: 14/05/2024
        /// Version: 1.0
        /// <summary>
        /// ObtenerWolkboxTokenPorIdPersonal
        /// </summary>
        /// <param name="idWolkboxToken">Identificador WolkboxToken</param>
        public void ReasignarTokenWolkboxPersonal(int idWolkboxToken)
        {
            try
            {
                var query = "conf.SP_ReasignarTokenWolkboxPersonal";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdWolkboxToken = idWolkboxToken });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
