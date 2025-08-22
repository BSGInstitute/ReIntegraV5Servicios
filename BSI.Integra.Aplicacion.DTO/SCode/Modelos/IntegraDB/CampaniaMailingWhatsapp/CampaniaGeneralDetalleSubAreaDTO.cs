using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp
{
    public class CampaniaGeneralDetalleSubAreaDTO
    {
        public int Id { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class CampaniaGeneralDetalleSubAreaEnvioDTO
    {
        public int Id { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public bool Estado { get; set; }
        public string Usuario { get; set; } = null!;


    }

}
