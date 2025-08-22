using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FormularioProgresivoCondicionMostrarDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class FormularioProgresivoCondicionMostrarEntradaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Usuario { get; set; }
    }

}
