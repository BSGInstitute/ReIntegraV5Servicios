using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.ExperianSentinel
{
    /// <summary>
    /// DTO unificado que representa la respuesta completa de Experian Sentinel
    /// independientemente del protocolo utilizado (SOAP o REST).
    /// Los nombres de campos corresponden exactamente a los usados en SentinelService.
    /// </summary>
    public class ExperianSentinelRespuestaDTO
    {
        /// <summary>Items de tipo Estandar (equivale a sdt_bsgrupo_estandar_dtos)</summary>
        public IList<SentinelSdtEstandarItemDTO> Estandar { get; set; } = new List<SentinelSdtEstandarItemDTO>();

        /// <summary>Items de reporte SBS (equivale a sdt_bsgrupo_repsbs_dtos)</summary>
        public IList<SentinelSdtRepSbsitemDTO> RepSBS { get; set; } = new List<SentinelSdtRepSbsitemDTO>();

        /// <summary>Items de linea de credito (equivale a sdt_bsgrupo_lincre_dtos)</summary>
        public IList<SentinelSdtLincreItemDTO> LinCre { get; set; } = new List<SentinelSdtLincreItemDTO>();

        /// <summary>Items de deuda vencida (equivale a sdt_bsgrupo_resven_dtos)</summary>
        public IList<SentinelSdtResVenItemDTO> ResVen { get; set; } = new List<SentinelSdtResVenItemDTO>();

        /// <summary>Informacion general (equivale a sdt_bsgrupo_infgen_dto). Puede ser null.</summary>
        public SentinelSdtInfGenDTO? InfGen { get; set; }

        /// <summary>Items de representantes legales (equivale a sdt_bsgrupo_repleg_dtos)</summary>
        public IList<SentinelRepLegItemDTO> RepLeg { get; set; } = new List<SentinelRepLegItemDTO>();

        /// <summary>Items de posicion historica (equivale a sdt_bsgrupo_poshis_dtos)</summary>
        public IList<SentinelSdtPoshisItemDTO> PosHis { get; set; } = new List<SentinelSdtPoshisItemDTO>();
    }
}
