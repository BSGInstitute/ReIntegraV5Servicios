using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class SeccionPw : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        public int IdPlantillaPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public List<SeccionTipoDetallePw> SeccionTipoDetallePws { get; set; }
    }
}
