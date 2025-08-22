using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class SeguimientoAlumnoCategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
        public bool AplicaModalidadOnline { get; set; }
        public bool AplicaModalidadAonline { get; set; }
        public bool AplicaModalidadPresencial { get; set; }
        public int IdTipoSeguimientoAlumnoCategoria { get; set; }
        public int? IdSeguimientoAlumnoDetalle { get; set; }
    }
}
