using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class SeguimientoAlumnoComentario : BaseIntegraEntity
    {
        public int? IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int IdSeguimientoAlumnoCategoria { get; set; }
        public int IdPersonal { get; set; }
        public int IdOportunidad { get; set; }
        public string Comentario { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public int? IdMigracion { get; set; }
    }
}
