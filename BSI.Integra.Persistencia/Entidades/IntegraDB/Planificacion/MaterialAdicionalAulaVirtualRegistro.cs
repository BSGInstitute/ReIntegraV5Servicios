using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialAdicionalAulaVirtualRegistro : BaseIntegraEntity
    {
        public int IdMaterialAdicionalAulaVirtual { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public bool? SoloLectura { get; set; }
    }
}
