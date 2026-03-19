using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProveedorDTO
    {
        public int Id { get; set; }
        public int IdTipoContribuyente { get; set; }
        public int IdDocumentoIdentidad { get; set; }
        [StringLength(20)]
        public string NroDocIdentidad { get; set; } = null!;
        [StringLength(1000)]
        public string RazonSocial { get; set; } = null!;
        [StringLength(500)]
        public string Nombre1 { get; set; } = null!;
        [StringLength(500)]
        public string Nombre2 { get; set; } = null!;
        [StringLength(500)]
        public string ApePaterno { get; set; } = null!;
        [StringLength(500)]
        public string ApeMaterno { get; set; } = null!;
        [StringLength(500)]
        public string Direccion { get; set; } = null!;
        [StringLength(500)]
        public string Descripcion { get; set; } = null!;
        public int? IdCiudad { get; set; }
        [StringLength(500)]
        public string? Telefono { get; set; }
        [StringLength(500)]
        public string Email { get; set; } = null!;
        [StringLength(50)]
        public string? Celular1 { get; set; }
        [StringLength(50)]
        public string? Celular2 { get; set; }
        [StringLength(200)]
        public string? Contacto1 { get; set; }
        [StringLength(200)]
        public string? Contacto2 { get; set; }
        public int? IdPrestacionRegistro { get; set; }
        public bool? EsPersonaValida { get; set; }
        public int? IdTipoImpuestoPreferido { get; set; }
        public int? IdRetencionPreferido { get; set; }
        public int? IdDetraccionPreferido { get; set; }
        public int? IdPersonalAsignado { get; set; }
        [StringLength(100)]
        public string? Alias { get; set; }
        public bool? EsDocente { get; set; }
        public List<ProveedorTipoServicioDTO> ListaProveedorTipoServicio { get; set; }
        public ProveedorDTO()
        {
            ListaProveedorTipoServicio = new List<ProveedorTipoServicioDTO>();
        }
    }
    public class ProveedorComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class FiltroRucProveedorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ruc { get; set; }
    }

    public class ProveedorProductoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Simbolo { get; set; }
        public string NombreMoneda { get; set; }
        public decimal Precio { get; set; }
        public int IdProducto { get; set; }
        public string Presentacion { get; set; }
        public int IdHistorico { get; set; }
        public int Version { get; set; }
    }
    public class CadenaStringDTO
    {
        public string Cadena1 { get; set; }
        public string Cadena2 { get; set; }
    }

    public class ProveedorWEnvioDTO
    {
        public int Id { get; set; }
        public int IdTipoContribuyente { get; set; }
        public int IdDocumentoIdentidad { get; set; }
        public string NroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public string ApePaterno { get; set; }
        public string ApeMaterno { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public int? IdCiudad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public string Contacto1 { get; set; }
        public string Contacto2 { get; set; }
        public string Alias { get; set; }
        public string UsuarioModificacion { get; set; }
        public int? IdImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? idPrestacionRegistro { get; set; }
        public bool? esDocente { get; set; }
        public List<ProveedorTipoServicioDTO> ListaProveedorTipoServicio { get; set; }
        public ProveedorWEnvioDTO()
        {
            ListaProveedorTipoServicio = new List<ProveedorTipoServicioDTO>();
        }

    }

    public class FiltroConvocatoriaPersonalDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdTipoServicio { get; set; }
        public bool EstadoPTS { get; set; }
        public bool EstadoP { get; set; }
        public string RazonSocial { get; set; }
    }

    public class ProveedorDocenteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdCiudad { get; set; }
        public string? Telefono { get; set; }
        public string Email { get; set; } = null!;
        public string? Celular1 { get; set; }
        public bool? EsPersonaValida { get; set; }
        public string? Alias { get; set; }
        public bool? EsDocente { get; set; }
    }

}
