using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Servicios.SCode.Service.Interface.Wolkbox
{

    public interface IWavixService
    {

        WavixPersonalDTO GetUserAccess(int idPersonal);
        Task<SipTrunkListResponseDTO> ListarSipTrunks(string apiKey, int? page = null, int? perPage = null);
        Task<SipTrunkConfigDTO> ObtenerSipTrunkPorId(string apiKey, string idSipTrunk);
        Task<GenerarTokenWidgetResponseDTO> GenerarTokenWidget(string apiKey, GenerarTokenWidgetRequestDTO request);
        Task<ConfiguracionCompletaWavixDTO> ObtenerConfiguracionCompletaWavix(int idPersonal);
        string ObtenerTokenActivo(int idPersonal);
        List<NumeroAsesorWavixDTO> GetNumberByUser(int idPersonal);
        EstadoLlamadaDTO ObtenerEstadoUltimaLlamada(int idPersonal, int idOportunidad, int idActividadDetalle, int idAlumno, int nroIntentoLlamada);

        /// <summary>
        /// Obtiene la lista de tokens activos de un personal
        /// </summary>
        List<TokenActivoListDTO> ObtenerTokensActivos(int idPersonal);

        /// <summary>
        /// Obtiene un token específico por su UUID
        /// </summary>
        TokenActivoListDTO ObtenerTokenPorUuid(string tokenUuid);

        /// <summary>
        /// Invalida (elimina) un token por su UUID
        /// </summary>
        TokenOperacionResponseDTO InvalidarToken(string tokenUuid, string usuario);

        /// <summary>
        /// Actualiza el payload de un token en la API de Wavix
        /// </summary>
        Task<TokenOperacionResponseDTO> ActualizarTokenPayload(string apiKey, string tokenUuid, object payload);
    }
}
