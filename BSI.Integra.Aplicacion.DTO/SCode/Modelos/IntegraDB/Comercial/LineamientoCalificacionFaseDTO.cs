using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class LineamientoCalificacionFaseDTO
    {
        public int? Id { get; set; }
        public int IdCriterioCalificacionFaseOportunidad { get; set; }
        public int Orden { get; set; }
        public int IdCriticidadCalificacion { get; set; }
        public string NombreLineamiento { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }

    public class LineamientoCalificacionFaseEntradaDTO
    {
        public int Id { get; set; }
        public int IdCriterioCalificacionFaseOportunidad { get; set; }
        public int Orden { get; set; }
        public int IdCriticidadCalificacion { get; set; }
        public string NombreLineamiento { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
    }
}