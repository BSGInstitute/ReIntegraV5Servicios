using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MapeoParaConsultaSqlInsertId
    {
        public String Id { get; set; }
    }
    public class CampaniaGeneralDetalle: BaseIntegraEntity
    {

        public int Id { get; set; }

        public int IdCampaniaGeneral { get; set; }

        public string Nombre { get; set; } = null!;

        public int Prioridad { get; set; }

        public string Asunto { get; set; } = null!;

        public int IdPersonal { get; set; }

        public int? IdCentroCosto { get; set; }
 
        public int? CantidadContactosMailing { get; set; }
 
        public int? CantidadContactosWhatsapp { get; set; }
 
        public bool? NoIncluyeWhatsaap { get; set; }
        public string? UrlFormulario { get; set; } = null!;

        public bool Estado { get; set; }
  
        public string UsuarioCreacion { get; set; } = null!;
   
        public string UsuarioModificacion { get; set; } = null!;
        
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }
 
        public byte[] RowVersion { get; set; } = null!;

        public int? IdMigracion { get; set; }
   
        public int? IdConjuntoAnuncio { get; set; }
  
        public bool EnEjecucion { get; set; }

    }
}
