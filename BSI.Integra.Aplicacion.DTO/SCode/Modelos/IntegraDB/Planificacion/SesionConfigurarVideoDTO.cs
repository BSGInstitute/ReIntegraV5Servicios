using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class SesionConfigurarVideoDTO
    {
        public int? Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int? NroDiapositiva { get; set; }
        public int? IdEvaluacion { get; set; }
        public bool? ConLogoVideo { get; set; }
        public bool? ConLogoDiapositiva { get; set; }
    }
    public class InformacionSesionConfigurarVideoDTO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int IdPGeneral { get; set; }
        public int Minuto { get; set; }
        public int IdTipoVista { get; set; }
        public int NroDiapositiva { get; set; }
        public bool ConLogoVideo { get; set; }
        public bool ConLogoDiapositiva { get; set; }
    }
}
