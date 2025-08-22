
using System;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProgramaGeneralMaterialEstudioAdicionalEntidadDTO
    {
        public int IdPGeneral { get; set; }
        public List<int>? IdsPEspecificos { get; set; }
        public List<ProgramaGeneralMaterialRegistroDTO> MaterialRegistro { get; set; }
    }
    public class ProgramaGeneralMaterialRegistroDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string NombreArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public string EnlaceArchivo { get; set; }
    }
    public class ProgramaGeneralMaterialAgrupadoDTO
    {
        public List<ProgramaGeneralMaterialRegistroDTO> MaterialAdicional { get; set; }
        public int[] ListaEspecifico { get; set; }
    }
}
    