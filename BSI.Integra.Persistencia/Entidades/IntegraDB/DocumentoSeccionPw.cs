using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DocumentoSeccionPw : BaseIntegraEntity
    {
        [StringLength(250)]
        public string Titulo { get; set; } = null!;
        public string? Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int IdDocumentoPw { get; set; }
        public int IdSeccionPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int? ZonaWeb { get; set; }
        public int? OrdenWeb { get; set; }
        public int? IdSeccionTipoDetallePw { get; set; }
        public int? NumeroFila { get; set; }
        [StringLength(5000)]
        public string? Cabecera { get; set; }
        [StringLength(5000)]
        public string? PiePagina { get; set; }
    }
}
