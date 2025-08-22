using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public  class CertificadoSolicitud : BaseIntegraEntity
    {
        [StringLength(555)]
        public string? NombreBeneficioDocumento { get; set; } 
        public int NumeroSolicitud { get; set; }
    }
}
