using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Servicios.Builder
{
    public class BuilderOrquestaSentinel_SDT_BSGrupo_ResVenItemDTO
    {
        public static SentinelSdtResVenItemDTO builderEntityDTO(SentinelServicio.SDT_IC_ResVenSDT_IC_ResVenItem entity)
        {
            SentinelSdtResVenItemDTO rpta = new SentinelSdtResVenItemDTO();

            rpta.TipoDocumento = entity.TipoDocumento;
            rpta.NroDocumento = entity.NroDocumento;
            rpta.CantidadDocs = entity.CantidadDocs;
            rpta.Fuente = entity.Fuente;
            rpta.Entidad = entity.Entidad;
            rpta.Monto = Convert.ToDecimal(entity.Monto);
            rpta.Cantidad = entity.Cantidad;
            if (!entity.DiasVencidos.Contains("/") && !entity.DiasVencidos.Equals(""))
                rpta.DiasVencidos = Convert.ToInt32(entity.DiasVencidos);

            return rpta;
        }

        public static IList<SentinelSdtResVenItemDTO> builderListEntityDTO(IEnumerable<SentinelServicio.SDT_IC_ResVenSDT_IC_ResVenItem> listaInput)
        {
            var listOutput = new List<SentinelSdtResVenItemDTO>();
            foreach (SentinelServicio.SDT_IC_ResVenSDT_IC_ResVenItem entityPO in listaInput)
            {
                listOutput.Add(builderEntityDTO(entityPO));
            }
            return listOutput;
        }
    }
}
