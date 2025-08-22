using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class MaterialPespecificoDetalleDTO
    {
        public int? Id { get; set; }
        public int IdMaterialPespecifico { get; set; }
        public int IdMaterialVersion { get; set; }
        public int IdMaterialEstado { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlArchivo { get; set; }
        public DateTime? FechaSubida { get; set; }
        public string ComentarioSubida { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdFur { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public string UsuarioAprobacion { get; set; }
        public string UsuarioSubida { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public int? IdEstadoRegistroMaterial { get; set; }
        public string UsuarioEnvio { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public int? IdMaterialTipo { get; set; }
    }
    public class MaterialPEspecificoDetalleCriterioDTO
    {
        public int IdMaterialPEspecificoDetalle { get; set; }
        public int IdMaterialPEspecifico { get; set; }
        public int IdMaterialAccion { get; set; }
        public int IdMaterialVersion { get; set; }
    }
    public class SubirMaterialPEspecificoDetalleDTO
    {
        public int Id { get; set; }
        public string ComentarioSubida { get; set; }
        public IList<IFormFile> Files { get; set; }
    }
    public class MaterialPEspecificoDetalleFurDTO
    {
        public int IdMaterialPEspecificoDetalle { get; set; }
        public int IdFur { get; set; }
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }
        public string Monto { get; set; }
        public double Cantidad { get; set; }
        public string NombrePlural { get; set; }
        public string Simbolo { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
    }

    public class AprobarRechazarRegistroEntregaMaterialDTO
	{
		public int IdMaterialPEspecificoDetalle { get; set; }
		public Dictionary<int, bool> ClaveValor { get; set; }
		public int EstadoRegistroMaterial { get; set; }
		public string Usuario { get; set; }
	}

}
