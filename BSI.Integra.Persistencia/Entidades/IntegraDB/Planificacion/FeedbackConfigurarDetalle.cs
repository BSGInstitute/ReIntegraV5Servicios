using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class FeedbackConfigurarDetalle : BaseIntegraEntity
    {
        public int IdFeedbackConfigurar { get; set; }

        public int IdSexo { get; set; }

        public int Puntaje { get; set; }

        [StringLength(150)]
        public string NombreVideo { get; set; } = null!;
        public int? OrdenVideo { get; set; }

    }
}
