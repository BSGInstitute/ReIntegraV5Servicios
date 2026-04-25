using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RemarketingEmbudoHistoricoDTO : BaseIntegraEntity
    {
        public int IdRemarketingEmbudoNivel { get; set; }
        public int IdAlumno { get; set; }
        public DateTime FechaClasificacion { get; set; }
    }
    public class OportunidadRemarketingEmbudoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public int FaseOportunidadAnterior { get; set; }
        public string CodigoFaseOportunidadAnterior { get; set; }
        public int FaseOportunidadActual { get; set; }
        public string CodigoFaseOportunidadActual { get; set; }
        public string ClasificacionProbabilidad { get; set; }
        public DateTime? FechaCreacionOportunidad { get; set; }
        public DateTime? FechaCambioOportunidad { get; set; }
        public DateTime? FechaRegistroEnvioWhatsapp { get; set; }
        public int IdCentroCosto { get; set; }
        public string Correo { get; set; }
        public DateTime? UltimaInteraccionProgresivo { get; set; }
    }
    public class OportunidadCompletaDTO : OportunidadUltimoCambioDTO
    {
        public int OcurrenciasEjecutadas { get; set; }
        public DateTime? UltimaInteraccionPortal { get; set; }
        public int CentroCostoRegistrado { get; set; }
        public DateTime? FechaUltimoWhatsapp { get; set; }
        public int LlamadasEfectivas { get; set; }
        public string Score { get; set; }
    }
    public class RemarketingEmbudoNivelDescripcionDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }
    public class RemarketingEmbudoNivelLlamadaEfectivaDTO
    {
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public int IdActividadDetalle { get; set; }
    }
    public class RemarketingEmbudoNivelLlamadaEfectivaAgrupadoDTO
    {
        public int IdAlumno { get; set; }
        public int LlamadasEfectivas { get; set; }
    }
    public class RemarketingEmbudoEsquemaNivelDTO
    {
        public int IdNivel { get; set;}
        public string Nivel { get; set; }
        public int IdEsquema { get; set; }
        public string Esquema { get; set; }
        public int IdRemarketingEmbudoEsquema { get; set; }
    }
    public class RemarketingEmbudoNivelInteraccionProgresivoDTO
    {
        public string Correo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
    public class OportunidadScoreDTO
    {
        public int IdOportunidad { get; set; }
        public decimal ScoreNumerico { get; set; }
        public string ScoreTextual { get; set; }
        public DateTime FechaProcesamiento { get; set; }
    }

    public class OportunidadScoreCompletoDTO
    {
        public int TotalRegistros { get; set; }
        public List<OportunidadScoreDTO> Scores { get; set; } = new List<OportunidadScoreDTO>();
    }
    public class InteracccionPortalUltimaInteraccionDTO
    {
        public int IdAlumno { get; set; }
        public DateTime FechaUltimaInteraccion { get; set; }
    }
    public class ActividadEjecutadaReporteDTO
    {
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public int NumeroOcurrenciasEjecutadas { get; set; }
    }
    public class AlumnoCentroCostoRegistroDTO
    {
        public int IdAlumno { get; set; }
        public int OportunidadCantidad { get; set; }
    }
    public class WhatsappUltimoMensajeEnviadoDTO
    {
        public int IdAlumno { get; set; }
        public DateTime WhatsappMensajeFechaEnvio { get; set; }
    }
    public class OportunidadUltimoCambioDTO
    {
        public int IdOportunidad { get; set; }
        public int IdAlumno { get; set; }
        public string FaseOportunidadActual { get; set; }
        public string ClasificacionProbabilidad { get; set; }
        public DateTime FechaCreacionOportunidad { get; set; }
        public int CantidadOportunidad { get; set; }
    }
}
