
using System;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class MaterialAdicionalAulaVirtualDTO
    {
        public int Id { get; set; }
        public string NombreConfiguracion { get; set; }
        public int IdPgeneral { get; set; }
        public string? Nombre { get; set; }
        public bool? EsOnline { get; set; }
    }
    public class MaterialAdicionalAulaVirtualRegistroDTO
    {
        public int Id { get; set; }
        public int IdMaterialAdicionalAulaVirtual { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public bool SoloLectura { get; set; }
    }
    public class MaterialAdicionalAulaVirtualPespecificoDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int? IdMaterialAdicionalAulaVirtual { get; set; }
    }
    public class MaterialAdicionalAulaVirtualDetalleDTO
    {
        public MaterialAdicionalAulaVirtualDTO MaterialAdicional { get; set; }
        public IEnumerable<MaterialAdicionalAulaVirtualRegistroDTO> MaterialAdicionalRegistro {  get; set; }
        public IEnumerable<int> ProgramaEspecifico { get; set; }
    }
    public class MaterialAdicionalAulaVirtualEntidadDTO
    {
        public int Id { get; set; }
        public string NombreConfiguracion { get; set; }
        public int IdPGeneral { get; set; }
        public int[] IdsPespecifico { get; set; }
        public bool EsOnline { get; set; }
        public List<MaterialAdicionalAulaVirtualRegistroDTO> MaterialAdicional { get; set; }
    }
}
