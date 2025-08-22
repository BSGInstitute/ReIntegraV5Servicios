using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class MaestroPersonalGrupoAccesoTemporalDTO
    {
        public int IdPersonal { get; set; }
        public string NombreProgramaPadre { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public List<int> IdPEspecificoHijo { get; set; }
        public double Avance { get; set; }
        public double Nota { get; set; }
        public bool EvaluacionHabilitada { get; set; }
        public int CantidadPreguntaConfigurada { get; set; }
        public int CantidadCrucigramaConfigurado { get; set; }
        public int CantidadPreguntaResuelta { get; set; }
        public int CantidadCrucigramaResuelta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class MaestroPersonalAccesoTemporalDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string NombreProgramaPadre { get; set; }
        public int IdPEspecificoPadre { get; set; }
        public int IdPEspecificoHijo { get; set; }
        public double Avance { get; set; }
        public double Nota { get; set; }
        public bool EvaluacionHabilitada { get; set; }
        public int CantidadPreguntaConfigurada { get; set; }
        public int CantidadCrucigramaConfigurado { get; set; }
        public int CantidadPreguntaResuelta { get; set; }
        public int CantidadCrucigramaResuelta { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
    public class EtiquetaParametroAlumnoSinOportunidadDTO
    {
        public int IdAlumno { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdAsesor { get; set; }
    }
    public class datoPlantillaWhatsApp
    {
        public string codigo { get; set; }
        public string texto { get; set; }
    }
    public class PlantillaBaseCorreoOperacionesDTO
    {
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
    }
}
