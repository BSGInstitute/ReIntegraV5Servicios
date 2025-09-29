using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{
    public class TranscripcionManualDTO
    {
        public int IdLlamada { get; set; }
        public int IdActividadDetalle { get; set; }
        public int IdPersonal { get; set; }
        public string Username { get; set; }
        public string Contacto { get; set; }
        public string Audio_Url { get; set; }
        public string Locale { get; set; }
        public string Ocurrencia { get; set; }
        public List<TranscripcionManualHistorialReprogramacionDTO> HistorialReprogramaciones { get; set; }
        public List<TranscripcionManualTransicionFaseDTO> InformacionFases { get; set; }
        public int FaseOrigen { get; set; }
        public int FaseDestino { get; set; }
    }

    public class TranscripcionManualHistorialReprogramacionDTO
    {
        public int IdLlamada { get; set; }
        public string EstadoOcurrencia { get; set; }
        public string Ocurrencia { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class TranscripcionManualTransicionFaseDTO
    {
        public int IdTransicionFaseOportunidad { get; set; }
        public TranscripcionManualFaseDTO FaseOrigen { get; set; }
        public TranscripcionManualFaseDTO FaseDestino { get; set; }
        public List<TranscripcionManualCriterioDTO> Criterios { get; set; }
    }

    public class TranscripcionManualFaseDTO
    {
        public int IdFaseOrigen { get; set; }
        public string? NombreFaseOrigen { get; set; }
        public string? CodigoFaseOrigen { get; set; }
        public int IdFaseDestino { get; set; }
        public string? NombreFaseDestino { get; set; }
        public string? CodigoFaseDestino { get; set; }  
    }

    public class TranscripcionManualCriterioDTO
    {
        public int IdCriterio { get; set; }
        public int OrdenCriterio { get; set; }
        public string NombreCriterio { get; set; }
        public List<TranscripcionManualLineamientoDTO> Lineamientos { get; set; }
    }

    public class TranscripcionManualLineamientoDTO
    {
        public int IdLineamientoCalificacionFase { get; set; }
        public int OrdenLineamiento { get; set; }
        public string NombreLineamientoCalificacionFase { get; set; }
        public TranscripcionManualCriticidadDTO Criticidad { get; set; }
    }

    public class TranscripcionManualCriticidadDTO
    {
        public int IdCriticidadCalificacion { get; set; }
        public string NombreCriticidad { get; set; }
    }
}
