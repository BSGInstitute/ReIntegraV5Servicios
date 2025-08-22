using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp
{

    public class CampaniaGeneralEnvioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdCategoriaOrigen { get; set; }
        public int? NroMaximoSegmentos { get; set; }
        public int? IdCategoriaObjetoFiltro { get; set; }
        public int? CantidadPeriodoSinCorreo { get; set; }
        public int? IdProbabilidadRegistroPw { get; set; }
        public int? IdTipoAsociacion { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public bool? IncluyeWhatsapp { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; } = null!;
        public string? UsuarioModificacion { get; set; } = null!;
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
