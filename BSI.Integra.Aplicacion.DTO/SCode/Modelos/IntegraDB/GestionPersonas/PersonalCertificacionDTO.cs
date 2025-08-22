using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalCertificacionDTO
    {
        public int Id { get; set; }
        public DateTime FechaCertificacion { get; set; }
        public string? Institucion { get; set; }
        public int IdPersonal { get; set; }
        public string Programa { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public int? IdCentroEstudio { get; set; }
    }
}
