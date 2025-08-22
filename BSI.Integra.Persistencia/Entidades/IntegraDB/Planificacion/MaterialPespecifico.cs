using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialPespecifico : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public int IdMaterialTipo { get; set; }
        public int GrupoEdicion { get; set; }
        public int GrupoEdicionOrden { get; set; }
        public int? IdFur { get; set; }
        public List<MaterialPespecificoDetalle> MaterialPespecificoDetalles { get; set; }
    }
}
