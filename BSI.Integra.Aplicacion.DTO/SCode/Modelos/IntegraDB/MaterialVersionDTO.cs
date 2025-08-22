using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MaterialVersionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
    }
    public class AprobarMaterialVersionDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class GrabacionCelularCorporativoDTO
    {
        public int? Id { get; set; }
        public string? NumeroDestino { get; set; } = null!;
        public Nullable<DateTime> FechaGrabacion { get; set; } = null!;
        public string? Url { get; set; } = null!;
        public string? Area { get; set; } = null!;

        //insert
        public IFormFile? File { get; set; }
        public Nullable<int> IdLlamada { get; set; }
        public string? NombreArchivo { get; set; }
        public Nullable<int> DuracionContesto { get; set; }
        public Nullable<int> NroBytes { get; set; }
        public Nullable<int> IdArea { get; set; }


    }

}
