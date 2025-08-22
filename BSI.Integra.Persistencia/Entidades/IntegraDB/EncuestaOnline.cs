using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EncuestaOnline : BaseIntegraEntity
    {
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? Version { get; set; }
        public int? IdTipoEncuesta { get; set; }
        public int? IdModalidadCurso { get; set; }

    }
}
