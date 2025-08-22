using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class RegistroCertificadoFisicoGenerado : BaseIntegraEntity
    {
        public int? Id { get; set; }
        public int? IdSolicitudCertificadoFisico { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        public string FormatoArchivo { get; set; }

        public string NombreArchivo { get; set; }

        public DateTime? UltimaFechaGeneracion { get; set; }

        public bool Estado { get; set; }
 
        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
        public int? IdMigracion { get; set; }

    }
}
