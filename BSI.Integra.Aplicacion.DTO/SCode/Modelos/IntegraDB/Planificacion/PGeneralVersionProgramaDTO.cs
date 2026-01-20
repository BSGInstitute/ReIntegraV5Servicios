using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PGeneralVersionProgramaDTO
    {
        public int? Id { get; set; }
        public int IdPgeneral { get; set; }
        public int? IdVersionPrograma { get; set; }
        public int? Duracion { get; set; }
    }
    public class PGeneralVersionProgramaDetalleDTO
    {
        public int? Id { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPgeneralVersionPrograma { get; set; }
        public int? IdVersionPrograma { get; set; }
        public string NombreVersion { get; set; }
        public int? Duracion { get; set; }
        public int? CreditoDisponibleTutorVirtual { get; set; }
        public int? CantidadWebinarAsignado { get; set; }
        public int? CantidadMesAccesoAdicionalWebinar { get; set; }
    }
    public class UpdateOnlyVersionProgramaDTO
    {
        public int IdPgeneral { get; set; }
        public List<PGeneralVersionProgramaDetalleDTO> versiones { get; set; } = [];
    }
    public class ResultadoPVersionDTO
    {
        public bool Estado { get; set; }
        public string Mensaje { get; set; }
    }
}
