using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoCaracteristicaPersonalDTO
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int? EdadMinima { get; set; }
        public int? EdadMaxima { get; set; }
        public int IdSexo { get; set; }
        public int IdEstadoCivil { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public int Version { get; set; }
    }
}
