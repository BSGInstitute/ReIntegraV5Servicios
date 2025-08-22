using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Servicios.Builder
{
    public class BuilderOrquestaSentinel_SDT_BSGrupo_EstandarItemDTO
    {
        public static SentinelSdtEstandarItemDTO builderEntityDTO(SentinelServicio.SDT_IC_EstandarSDT_IC_EstandarItem entity)
        {


            //Mapper.CreateMap<SentinelService.SDT_IC_EstandarSDT_IC_EstandarItem, SentinelSdtEstandarItemDTO>()
            //        .IgnoreAllPropertiesWithAnInaccessibleSetter();
            //    var rpta = Mapper.Map<SentinelService.SDT_IC_EstandarSDT_IC_EstandarItem, SentinelSdtEstandarItemDTO>(entity);
            string pattern = "^([0][1-9]|[1-9]|[12][0-9]|3[01])(\\/|-)([0][1-9]|[1-9]|[1][0-2])\\2(\\d{4})";
            SentinelSdtEstandarItemDTO rpta = new SentinelSdtEstandarItemDTO();


            rpta.TipoDocumento = entity.TipoDocumento;
            rpta.Documento = entity.Documento;
            rpta.RazonSocial = entity.RazonSocial;
            try
            {
                rpta.FechaProceso = entity.FechaProceso;
            }
            catch (Exception e)
            {
                throw new Exception("Error en SentinelSdtEstandarItem - FechaProceso " + e.Message);
            }

            rpta.Semaforos = entity.Semaforos;
            rpta.Score = entity.Score;
            rpta.NroBancos = entity.NroBancos;
            rpta.DeudaTotal = entity.DeudaTotal;
            rpta.VencidoBanco = entity.VencidoBanco;
            rpta.Calificativo = entity.Calificativo;
            rpta.Veces24m = entity.Veces24m;
            rpta.SemanaActual = entity.SemaActual;
            rpta.SemanaPrevio = entity.SemaPrevio;
            rpta.SemanaPeorMejor = entity.SemaPeorMejor;
            rpta.Documento2 = entity.Documento2;
            rpta.EstadoDomicilio = entity.EstDomic;
            rpta.CondicionDomicilio = entity.CondDomic;
            rpta.DeudaTributaria = entity.DeudaTributaria;
            rpta.DeudaLaboral = entity.DeudaLaboral;
            rpta.DeudaImpagable = entity.DeudaImpaga;
            rpta.DeudaProtestos = entity.DeudaProtestos;
            rpta.DeudaSbs = entity.DeudaSBS;
            rpta.CuentasTarjetas = entity.TarCtas;
            rpta.ReporteNegativo = entity.RepNeg;
            rpta.TipoActividad = entity.TipoActv;
            if (entity.FechIniActv != "")
            {
                try
                {
                    rpta.FechaInicioActividad = obtenerFecha(entity.FechIniActv, pattern);
                }
                catch (Exception e)
                {
                    throw new Exception("Error en SentinelSdtEstandarItem - FechaInicioActividad " + e.Message);
                }

                //rpta.FechaInicioActividad = Convert.ToDateTime(entity.FechIniActv);
            }
            else
            {
                rpta.FechaInicioActividad = null;
            }

            rpta.DeudaDirecta = entity.DeudaDirecta;
            rpta.DeudaIndirecta = entity.DeudaIndirecta;
            rpta.DeudaCastigada = entity.DeudaCastigada;
            rpta.LineaCreditoNoUtilizada = entity.LineaCreditoNoUti;
            rpta.TotalRiesgo = entity.TotalRiesgo;
            rpta.PeorCalificacion = entity.PeorCalificacion;
            rpta.PorcentajeCalificacionNormal = entity.PorCalNormal;
            return rpta;
        }

        public static IList<SentinelSdtEstandarItemDTO> builderListEntityDTO(IEnumerable<SentinelServicio.SDT_IC_EstandarSDT_IC_EstandarItem> listaInput)
        {
            var listOutput = new List<SentinelSdtEstandarItemDTO>();
            foreach (SentinelServicio.SDT_IC_EstandarSDT_IC_EstandarItem entityPO in listaInput)
            {
                listOutput.Add(builderEntityDTO(entityPO));
            }
            return listOutput;
        }
        private static DateTime obtenerFecha(string fechaString, string patron)
        {
            int dia = 0;
            int mes = 0;
            int año = 0;

            foreach (Match match in Regex.Matches(fechaString, patron, RegexOptions.IgnoreCase))
            {
                dia = int.Parse(match.Groups[1].Value);
                mes = int.Parse(match.Groups[3].Value);
                año = int.Parse(match.Groups[4].Value);
            }
            if (mes > 12 && dia < 13)
            {
                int aux = dia;
                dia = mes;
                mes = aux;
            }

            DateTime fecha = new DateTime(año, mes, dia);
            return fecha;
        }
    }
}
