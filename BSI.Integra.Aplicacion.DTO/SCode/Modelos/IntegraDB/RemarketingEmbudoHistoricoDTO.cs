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
        public string UltimoEnvio { get; set; }
        public string Correo { get; set; }
        public DateTime? UltimaInteraccionProgresivo { get; set; }
    }
    public class OportunidadCompletaDTO : OportunidadRemarketingEmbudoDTO
    {
        public int CantidadTotalOportunidades { get; set; }
        public int CantidadOportunidadesCerradas { get; set; }
        public int LlamadasEfectivas { get; set; }
        public string Score { get; set; }

        public bool TieneOportunidadesCerradas => CantidadOportunidadesCerradas > 0;
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
}
