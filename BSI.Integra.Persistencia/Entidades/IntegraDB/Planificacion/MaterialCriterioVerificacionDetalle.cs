using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialCriterioVerificacionDetalle : BaseIntegraEntity
    {
        public int IdMaterialPespecificoDetalle { get; set; }
        public int IdMaterialCriterioVerificacion { get; set; }
        public bool EsAprobado { get; set; }
        [StringLength(50)]
        public string? UsuarioAprobacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
    }
}
