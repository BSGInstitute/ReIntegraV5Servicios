using System;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PublicacionAdminDTO
    {
        public int IdPublicacion { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public int CantidadLikes { get; set; }
        public bool Visible { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class UsuarioBsgTentoDTO
    {
        public int Id { get; set; }
        public string IdAspNetUser { get; set; }
        public string Nombre { get; set; }
        public int Monedas { get; set; }
        public int Puntos { get; set; }
        public int Creditos { get; set; }
        public int RachaActual { get; set; }
        public int RachaMaxima { get; set; }
        public DateTime? UltimaFechaEstudio { get; set; }
    }

    public class UsuarioBsgTentoInsertarDTO
    {
        public string IdAspNetUser { get; set; }
        public string Nombre { get; set; }
    }
}
