using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EmpresaAutorizada : BaseIntegraEntity
    {
        [StringLength(200)]
        public string RazonSocial { get; set; } = null!;
        [StringLength(20)]
        public string? Ruc { get; set; }
        [StringLength(500)]
        public string? Direccion { get; set; }
        [StringLength(200)]
        public string? Central { get; set; }
        public bool Activo { get; set; }
        public int IdPais { get; set; }
        [StringLength(200)]
        public string? NombreComercial { get; set; }

    }
}
