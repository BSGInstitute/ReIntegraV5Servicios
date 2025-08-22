using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Servicios.Builder
{
    public class BuilderOrquestaSentinel_SDT_BSGrupo_RepSBSItemDTO
    {
        public static SentinelSdtRepSbsitemDTO builderEntityDTO(SentinelServicio.SDT_IC_RepSBSSDT_IC_RepSBSItem entity)
        {
            SentinelSdtRepSbsitemDTO rpta = new SentinelSdtRepSbsitemDTO();

            rpta.TipoDoc = entity.TipoDocCPT;
            rpta.NroDoc = entity.NroDocCPT;
            rpta.NombreRazonSocial = entity.NomRazSocEnt;
            rpta.Calificacion = entity.Calificacion;
            rpta.MontoDeuda = Convert.ToDecimal(entity.MontoDeuda);
            rpta.DiasVencidos = entity.DiasVencidos;
            rpta.FechaReporte = entity.FechaReporte;
            return rpta;
        }

        public static IList<SentinelSdtRepSbsitemDTO> builderListEntityDTO(IEnumerable<SentinelServicio.SDT_IC_RepSBSSDT_IC_RepSBSItem> listaInput)
        {
            var listOutput = new List<SentinelSdtRepSbsitemDTO>();
            foreach (SentinelServicio.SDT_IC_RepSBSSDT_IC_RepSBSItem entityPO in listaInput)
            {
                listOutput.Add(builderEntityDTO(entityPO));
            }
            return listOutput;
        }
    }
}
