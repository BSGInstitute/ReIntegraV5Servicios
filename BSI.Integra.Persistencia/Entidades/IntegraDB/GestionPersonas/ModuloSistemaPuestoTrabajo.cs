using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class ModuloSistemaPuestoTrabajo : BaseIntegraEntity
    {
        public int IdPuestoTrabajo { get; set; }
        public int IdModuloSistema { get; set; }
    }
}
