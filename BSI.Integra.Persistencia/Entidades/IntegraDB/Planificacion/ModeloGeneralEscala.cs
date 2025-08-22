using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ModeloGeneralEscala : BaseIntegraEntity
    {
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public decimal ValorMaximo { get; set; }
        public decimal ValorMinimo { get; set; }
        public int IdModeloGeneral { get; set; }
        public Guid IdMigracion { get; set; }
    }
}
