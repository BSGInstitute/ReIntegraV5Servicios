using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConjuntoListaResultado : BaseIntegraEntity
    {


        public int IdAlumno { get; set; }
  
        public int IdConjuntoListaDetalle { get; set; }
   
        public bool? EsVentaCruzada { get; set; }

        public int NroEjecucion { get; set; }
    
        public bool Activo { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;
     
        public DateTime FechaCreacion { get; set; }
   
        public DateTime FechaModificacion { get; set; }
     
        public byte[] RowVersion { get; set; } = null!;

        public int? IdMigracion { get; set; }
   
        public int? IdOportunidad { get; set; }

    }
}
