using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LlamadaActividad : BaseIntegraEntity
    {
        
        public int? IdActividadDetalle { get; set; }
     
        public int? IdAsesor { get; set; }
  
        public int? IdLlamada { get; set; }
        public bool EstadoProgramado { get; set; }
        public string Tag { get; set; } = null!;

        public DateTime? FechaInicioLlamada { get; set; }
      
        public DateTime? FechaFinLlamada { get; set; }
      
        public int? IdAgendaTab { get; set; }
      
        public bool Estado { get; set; }
       
        public string UsuarioCreacion { get; set; } = null!;
        
        public string UsuarioModificacion { get; set; } = null!;
      
        public DateTime FechaCreacion { get; set; }
      
        public DateTime FechaModificacion { get; set; }
    
        public byte[] RowVersion { get; set; } = null!;

        public Guid? IdMigracion { get; set; }

        public virtual TActividadDetalle? IdActividadDetalleNavigation { get; set; }
    }
}

