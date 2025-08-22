using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ModeloGeneralCategoriaDato : BaseIntegraEntity
    {
        public int IdModeloGeneral { get; set; }
        public int IdAsociado { get; set; }
        public string Nombre { get; set; }
        public double Valor { get; set; }
        public int IdCategoriaDato { get; set; }
    }
}
