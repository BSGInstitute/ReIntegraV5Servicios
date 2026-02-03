using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial
{
    public class CriticidadCalificacionDTO
    {
        public int Id { get; set; }
        public string NombreCriticidad { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
    public class CriticidadCalificacionEntradaDTO
    {
        public int? Id { get; set; }
        public string NombreCriticidad { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string Usuario { get; set; }
    }
}
