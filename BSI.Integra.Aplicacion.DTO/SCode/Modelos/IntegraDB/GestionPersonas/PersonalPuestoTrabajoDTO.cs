using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PersonalPuestoTrabajoDTO
    {
        public int Id { get; set; }
        public string Rol { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class PersonalJefeInmediatoDTO
    {
        public int Id { get; set; }
        public int IdJefe { get; set; }
        public string DatosJefe { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
    public class PersonalTipoAsesorDTO
    {
        public int Id { get; set; }
        public int? IdCerrador { get; set; }
        public string AsesorAsociado { get; set; }
        public bool? EsCerrador { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }

}
