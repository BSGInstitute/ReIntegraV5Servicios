using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoRemuneracion : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public int IdPais { get; set; }
        public int IdTableroComercialCategoriaAsesor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }
        public virtual PuestoTrabajo IdPuestoTrabajoNavigation { get; set; } = null!;
        public virtual ICollection<PuestoTrabajoRemuneracionDetalle> TPuestoTrabajoRemuneracionDetalles { get; set; }
        public virtual ICollection<PuestoTrabajoRemuneracionDetalle> PuestoTrabajoRemuneracionDetalles { get; set; }
    }
}
