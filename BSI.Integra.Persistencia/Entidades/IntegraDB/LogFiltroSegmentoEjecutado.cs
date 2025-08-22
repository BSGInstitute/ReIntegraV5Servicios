using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LogFiltroSegmentoEjecutado : BaseIntegraEntity
    {
        public readonly bool TieneErrores;

        public int Id { get; set; }

        public int IdFiltroSegmento { get; set; }

        public int IdCentroCosto { get; set; }
  
        public int IdTipoDato { get; set; }

        public int IdOrigen { get; set; }

        public int IdFaseOportunidad { get; set; }

        public int TotalOportunidadesCreadas { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;
    
        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public byte[] RowVersion { get; set; } = null!;
 
        public int? IdMigracion { get; set; }
    }
}
