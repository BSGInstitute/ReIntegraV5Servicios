using BSI.Integra.Aplicacion.DTO.ExperianSentinel;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Builder;
using BSI.Integra.Repositorio.UnitOfWork;
using SentinelServicio;

namespace BSI.Integra.Aplicacion.Servicios.ExperianSentinel
{
    /// <summary>
    /// Implementacion SOAP del cliente Experian Sentinel.
    /// Encapsula la logica existente de WS_BSGrupoSoapPortClient y los 7 Builders.
    /// Es la implementacion por defecto cuando Sentinel:Provider = "SOAP".
    /// </summary>
    public class ExperianSentinelSoapClient : IExperianSentinelClient
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExperianSentinelSoapClient(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Consulta Experian Sentinel via SOAP y mapea los 7 arrays de respuesta
        /// al DTO unificado ExperianSentinelRespuestaDTO.
        /// </summary>
        public Task<ExperianSentinelRespuestaDTO> ConsultarAsync(string dniConsulta, string tipoDocumento)
        {
            // Obtener credenciales SOAP desde la BD
            var credenciales = _unitOfWork.SentinelRepository.ObtenerCredencial();

            WS_BSGrupoSoapPortClient clienteSoap = new WS_BSGrupoSoapPortClient();

            SDT_IC_EstandarSDT_IC_EstandarItem[] sdt_bsgrupo_estandar;
            SDT_IC_RepSBSSDT_IC_RepSBSItem[] sdt_bsgrupo_repsbs;
            SDT_IC_LinCreItem[] sdt_bsgrupo_lincre;
            SDT_IC_ResVenSDT_IC_ResVenItem[] sdt_bsgrupo_resven;
            SDT_IC_InfGen sdt_bsgrupo_infgen;
            SDT_IC_RepLegSDT_IC_RepLegItem[] sdt_bsgrupo_repleg;
            SDT_IC_PosHisSDT_IC_PosHisItem[] sdt_bsgrupo_poshis;

            clienteSoap.Execute(
                credenciales.DNI,
                credenciales.Clave,
                credenciales.Servicio!.Value,
                credenciales.TipoDocumento,
                dniConsulta,
                out sdt_bsgrupo_estandar,
                out sdt_bsgrupo_repsbs,
                out sdt_bsgrupo_lincre,
                out sdt_bsgrupo_resven,
                out sdt_bsgrupo_infgen,
                out sdt_bsgrupo_repleg,
                out sdt_bsgrupo_poshis);

            var respuesta = new ExperianSentinelRespuestaDTO
            {
                Estandar = BuilderOrquestaSentinel_SDT_BSGrupo_EstandarItemDTO
                               .builderListEntityDTO(sdt_bsgrupo_estandar.ToList()),
                RepSBS   = BuilderOrquestaSentinel_SDT_BSGrupo_RepSBSItemDTO
                               .builderListEntityDTO(sdt_bsgrupo_repsbs.ToList()),
                LinCre   = BuilderOrquestaSentinel_SDT_BSGrupo_LinCreItemDTO
                               .builderListEntityDTO(sdt_bsgrupo_lincre.ToList()),
                ResVen   = BuilderOrquestaSentinel_SDT_BSGrupo_ResVenItemDTO
                               .builderListEntityDTO(sdt_bsgrupo_resven.ToList()),
                InfGen   = BuilderOrquestaSentinel_SDT_BSGrupo_InfGenDTO
                               .builderEntityDTO(sdt_bsgrupo_infgen),
                RepLeg   = BuilderOrquestaSentinel_SDT_BSGrupo_RepLegItemDTO
                               .builderListEntityDTO(sdt_bsgrupo_repleg.ToList()),
                PosHis   = BuilderOrquestaSentinel_SDT_BSGrupo_PosHisItemDTO
                               .builderListEntityDTO(sdt_bsgrupo_poshis.ToList())
            };

            return Task.FromResult(respuesta);
        }

        public Task<object> ConsultarAsyncCrudo(string dniConsulta, string tipoDocumento)
        {
            var credenciales = _unitOfWork.SentinelRepository.ObtenerCredencial();

            WS_BSGrupoSoapPortClient clienteSoap = new WS_BSGrupoSoapPortClient();

            SDT_IC_EstandarSDT_IC_EstandarItem[] sdt_bsgrupo_estandar;
            SDT_IC_RepSBSSDT_IC_RepSBSItem[] sdt_bsgrupo_repsbs;
            SDT_IC_LinCreItem[] sdt_bsgrupo_lincre;
            SDT_IC_ResVenSDT_IC_ResVenItem[] sdt_bsgrupo_resven;
            SDT_IC_InfGen sdt_bsgrupo_infgen;
            SDT_IC_RepLegSDT_IC_RepLegItem[] sdt_bsgrupo_repleg;
            SDT_IC_PosHisSDT_IC_PosHisItem[] sdt_bsgrupo_poshis;

            clienteSoap.Execute(
                credenciales.DNI,
                credenciales.Clave,
                credenciales.Servicio!.Value,
                credenciales.TipoDocumento,
                dniConsulta,
                out sdt_bsgrupo_estandar,
                out sdt_bsgrupo_repsbs,
                out sdt_bsgrupo_lincre,
                out sdt_bsgrupo_resven,
                out sdt_bsgrupo_infgen,
                out sdt_bsgrupo_repleg,
                out sdt_bsgrupo_poshis);

            object crudo = new
            {
                Estandar = sdt_bsgrupo_estandar,
                RepSBS   = sdt_bsgrupo_repsbs,
                LinCre   = sdt_bsgrupo_lincre,
                ResVen   = sdt_bsgrupo_resven,
                InfGen   = sdt_bsgrupo_infgen,
                RepLeg   = sdt_bsgrupo_repleg,
                PosHis   = sdt_bsgrupo_poshis
            };

            return Task.FromResult(crudo);
        }
    }
}
