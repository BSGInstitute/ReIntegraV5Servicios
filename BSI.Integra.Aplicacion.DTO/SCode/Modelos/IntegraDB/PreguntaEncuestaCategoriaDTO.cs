using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class PreguntaEncuestaCategoriaDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }


    public class PreguntaEncuestaCategoriaEntradaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; } = null!;
        public string Usuario { get; set; }
    }

    public class PreguntaCategoriaAsincronicaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        

    }

}
