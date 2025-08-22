using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalRemuneracionDTO
    {
        public int Id { get; set; } 
        public int IdPersonal { get; set; }
        public int? IdTipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinanciera { get; set; }
        public string? NumeroCuenta { get; set; }

        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }

    public class DatosPersonalRemuneracionDTO
    {
        public int? IdTipoPagoRemuneracion { get; set; }
        public int? IdEntidadFinanciera { get; set; }
        public string? NumeroCuenta { get; set; }
        public bool EsModificado { get; set; }
        public bool Activo { get; set; }

    }

}
