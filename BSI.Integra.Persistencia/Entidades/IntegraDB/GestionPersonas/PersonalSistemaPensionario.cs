using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalSistemaPensionario :BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdSistemaPensionario { get; set; }
        public int? IdEntidadSistemaPensionario { get; set; }
        public string CodigoAfiliado { get; set; }
        public bool Activo { get; set; }
    }
}
