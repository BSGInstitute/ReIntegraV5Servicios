using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RecuperacionAutomaticoModuloSistemaDTO :BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdModuloSistema { get; set; }
        public string Tipo { get; set; } = null!;
        public bool Habilitado { get; set; }
        public bool EnvioCorreo { get; set; }
    }
}
