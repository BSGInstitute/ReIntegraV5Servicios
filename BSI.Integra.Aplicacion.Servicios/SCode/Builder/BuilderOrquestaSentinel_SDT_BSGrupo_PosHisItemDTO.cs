using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Servicios.Builder
{
    public class BuilderOrquestaSentinel_SDT_BSGrupo_PosHisItemDTO
    {
        public static SentinelSdtPoshisItemDTO builderEntityDTO(SentinelServicio.SDT_IC_PosHisSDT_IC_PosHisItem entity)
        {
            SentinelSdtPoshisItemDTO rpta = new SentinelSdtPoshisItemDTO();

            rpta.TipoDocumento = entity.CnhCPTTDoc;
            rpta.NumeroDocumento = entity.CnhCPTNroDoc;
            rpta.FechaProceso = Convert.ToDateTime(entity.CnhFchPro);
            rpta.SemanaActual = entity.CnhSemAct;
            rpta.DescripcionSemaforo = entity.CnhSemActDes;
            rpta.Score = Convert.ToDecimal(entity.CnhScore);
            rpta.CodigoVariacion = entity.CnhVarCod;
            rpta.DescripcionVariacion = entity.CnhVarDes;
            rpta.NumeroEntidades = entity.CnhNumEnt;
            rpta.DeudaTotal = Convert.ToDecimal(entity.CnhDeutot);
            rpta.PorcentajeCalificacion = Convert.ToDecimal(entity.CnhPorCal);
            rpta.PeorCalificacion = entity.CnhPeoCal;
            rpta.PeroCalificacionDescripcion = entity.CnhPeoCalDes;
            rpta.MontoSbs = Convert.ToDecimal(entity.CnhMonVenSBS);
            rpta.ProgresoRegistro = Convert.ToDecimal(entity.CnhProReg);
            rpta.DocImpuesto = Convert.ToDecimal(entity.CnhDocImp);
            rpta.DeudaTributaria = Convert.ToDecimal(entity.CnhDeuTri);
            rpta.Afp = Convert.ToDecimal(entity.CnhAFP);
            rpta.TarjetaCredito = entity.CnhTarCre;
            rpta.CuentaCorriente = entity.CnhCtaCte;
            rpta.ReporteNegativo = entity.CnhRepNeg;
            rpta.DeudaDirecta = Convert.ToDecimal(entity.CnhDeuDir);
            rpta.DeudaIndirecta = Convert.ToDecimal(entity.CnhDeuInd);
            rpta.LineaCreditoNoUtilizada = Convert.ToDecimal(entity.CnhLinCreNoU);
            rpta.DeudaCastigada = Convert.ToDecimal(entity.CnhDeuCast);

            return rpta;
        }
        public static IList<SentinelSdtPoshisItemDTO> builderListEntityDTO(IEnumerable<SentinelServicio.SDT_IC_PosHisSDT_IC_PosHisItem> listaInput)
        {
            var listOutput = new List<SentinelSdtPoshisItemDTO>();
            foreach (SentinelServicio.SDT_IC_PosHisSDT_IC_PosHisItem entityPO in listaInput)
            {
                listOutput.Add(builderEntityDTO(entityPO));
            }
            return listOutput;
        }
    }
}
