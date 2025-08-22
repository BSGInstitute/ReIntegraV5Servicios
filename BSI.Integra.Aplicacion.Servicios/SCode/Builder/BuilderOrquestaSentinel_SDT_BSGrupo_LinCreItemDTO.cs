using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Servicios.Builder
{
    public class BuilderOrquestaSentinel_SDT_BSGrupo_LinCreItemDTO
    {
        public static SentinelSdtLincreItemDTO builderEntityDTO(SentinelServicio.SDT_IC_LinCreItem entity)
        {
            SentinelSdtLincreItemDTO rpta = new SentinelSdtLincreItemDTO();

            rpta.TipoDocumento = entity.TipDoc;
            rpta.NumeroDocumento = entity.NumDoc;
            rpta.TipoCuenta = entity.TipoCuenta;
            rpta.LineaCredito = Convert.ToDecimal(entity.LinCred);
            rpta.LineaCreditoNoUtil = Convert.ToDecimal(entity.LinNoUtil);
            rpta.LineaUtil = Convert.ToDecimal(entity.LinUtil);
            rpta.CnsEntNomRazLn = entity.CnsEntNomRazLN;

            return rpta;
        }

        public static IList<SentinelSdtLincreItemDTO> builderListEntityDTO(IEnumerable<SentinelServicio.SDT_IC_LinCreItem> listaInput)
        {
            var listOutput = new List<SentinelSdtLincreItemDTO>();
            foreach (SentinelServicio.SDT_IC_LinCreItem entityPO in listaInput)
            {
                listOutput.Add(builderEntityDTO(entityPO));
            }
            return listOutput;
        }
    }
}
