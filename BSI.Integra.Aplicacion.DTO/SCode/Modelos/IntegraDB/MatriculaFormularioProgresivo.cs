using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MatriculaFormularioProgresivoDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdProgressiveProfilingCodigoDescuentoCorreo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
    public class InformacionCupoDescuentoDTO
    {
        public int Id { get; set; }
        public int PorcentajeDescuento { get; set; }
        public string CodigoDescuento { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaVencimiento { get; set; }

    }
}
