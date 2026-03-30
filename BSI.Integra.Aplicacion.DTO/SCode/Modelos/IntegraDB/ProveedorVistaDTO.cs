using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>
    /// DTO para la vista fin.V_ObtenerDatosProveedor
    /// Contiene campos adicionales de la vista que no están en ProveedorDTO
    /// </summary>
    public class ProveedorVistaDTO
    {
        public int Id { get; set; }
        public int IdTipoContribuyente { get; set; }
        public string? TipoContribuyente { get; set; }
        public int IdDocumentoIdentidad { get; set; }
        public string? DocumentoIdentidad { get; set; }
        [StringLength(20)]
        public string? NroDocumento { get; set; }
        public string? Proveedor { get; set; }
        [StringLength(1000)]
        public string? RazonSocial { get; set; }
        [StringLength(500)]
        public string? ApePaterno { get; set; }
        [StringLength(500)]
        public string? ApeMaterno { get; set; }
        [StringLength(500)]
        public string? Nombre1 { get; set; }
        [StringLength(500)]
        public string? Nombre2 { get; set; }
        [StringLength(500)]
        public string? Descripcion { get; set; }
        [StringLength(500)]
        public string? Direccion { get; set; }
        public int? IdPais { get; set; }
        public string? Pais { get; set; }
        public int? IdCiudad { get; set; }
        public string? Ciudad { get; set; }
        [StringLength(500)]
        public string? Telefono { get; set; }
        [StringLength(500)]
        public string? Email { get; set; }
        [StringLength(50)]
        public string? Celular1 { get; set; }
        [StringLength(50)]
        public string? Celular2 { get; set; }
        [StringLength(200)]
        public string? Contacto1 { get; set; }
        [StringLength(200)]
        public string? Contacto2 { get; set; }
        public int? IdPrestacionRegistro { get; set; }
        public string? Criterio1 { get; set; }
        public string? Criterio2 { get; set; }
        public string? Criterio3 { get; set; }
        public string? Criterio4 { get; set; }
        public string? Criterio5 { get; set; }
        public string? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public int? IdImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public int? IdPersonalAsignado { get; set; }
        [StringLength(100)]
        public string? Alias { get; set; }
        public bool? EsDocente { get; set; }
        public List<ProveedorTipoServicioDTO> ListaProveedorTipoServicio { get; set; }

        public ProveedorVistaDTO()
        {
            ListaProveedorTipoServicio = new List<ProveedorTipoServicioDTO>();
        }
    }
}
