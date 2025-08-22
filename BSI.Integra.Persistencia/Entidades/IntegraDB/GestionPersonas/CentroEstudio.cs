using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class CentroEstudio : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int? IdPais { get; set; }
        public int IdCiudad { get; set; }
        public int IdTipoCentroEstudio { get; set; }
    }
}
