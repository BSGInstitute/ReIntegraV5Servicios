using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DataCreditoDataProductoValor : BaseIntegraEntity
    {
        public int IdDataCreditoBusqueda { get; set; }
        [StringLength(30)]
        public string? Producto { get; set; }
        [StringLength(30)]
        public string? Valor1 { get; set; }
        [StringLength(30)]
        public string? Valor2 { get; set; }
        [StringLength(30)]
        public string? Valor3 { get; set; }
        [StringLength(30)]
        public string? Valor4 { get; set; }
        [StringLength(30)]
        public string? Valor5 { get; set; }
        [StringLength(30)]
        public string? Valor6 { get; set; }
        [StringLength(30)]
        public string? Valor7 { get; set; }
        [StringLength(30)]
        public string? Valor8 { get; set; }
        [StringLength(30)]
        public string? Valor9 { get; set; }
        [StringLength(30)]
        public string? Valor10 { get; set; }
        [StringLength(20)]
        public string? Valor1smlv { get; set; }
        [StringLength(30)]
        public string? Valor2smlv { get; set; }
        [StringLength(30)]
        public string? Valor3smlv { get; set; }
        [StringLength(30)]
        public string? Valor4smlv { get; set; }
        [StringLength(30)]
        public string? Valor5smlv { get; set; }
        [StringLength(30)]
        public string? Valor6smlv { get; set; }
        [StringLength(30)]
        public string? Valor7smlv { get; set; }
        [StringLength(30)]
        public string? Valor8smlv { get; set; }
        [StringLength(30)]
        public string? Valor9smlv { get; set; }
        [StringLength(30)]
        public string? Valor10smlv { get; set; }
        [StringLength(30)]
        public string? Razon1 { get; set; }
        [StringLength(30)]
        public string? Razon2 { get; set; }
        [StringLength(30)]
        public string? Razon3 { get; set; }
        [StringLength(30)]
        public string? Razon4 { get; set; }
        [StringLength(30)]
        public string? Razon5 { get; set; }
        [StringLength(30)]
        public string? Razon6 { get; set; }
        [StringLength(30)]
        public string? Razon7 { get; set; }
        [StringLength(30)]
        public string? Razon8 { get; set; }
        [StringLength(30)]
        public string? Razon9 { get; set; }
        [StringLength(30)]
        public string? Razon10 { get; set; }
        public DataCreditoBusquedum IdDataCreditoBusquedaNavigation { get; set; } = null!;
    }
}
