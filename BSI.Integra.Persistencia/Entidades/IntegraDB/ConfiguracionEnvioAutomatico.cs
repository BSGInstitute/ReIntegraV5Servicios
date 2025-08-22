using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionEnvioAutomatico : BaseIntegraEntity
    {
        public int? IdEstadoInicial { get; set; }
        public int? IdSubEstadoInicial { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdSubEstadoDestino { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
