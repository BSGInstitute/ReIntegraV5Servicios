using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CertificadoTipoPrograma : BaseIntegraEntity
    {
        [StringLength(50)]
        public string NombreProgramaCertificado { get; set; } = null!;
        [StringLength(50)]
        public string? Codigo { get; set; } 
        public bool AplicaFondoDiploma { get; set; } 
        public bool AplicaSeOtorga { get; set; } 
        public bool AplicaNota { get; set; }
    }
}
