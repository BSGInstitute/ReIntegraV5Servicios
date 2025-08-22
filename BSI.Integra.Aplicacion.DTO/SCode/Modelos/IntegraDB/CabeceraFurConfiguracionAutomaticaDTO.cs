namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CabeceraFurConfiguracionAutomaticaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; } = null!;
        public int? IdPeriodoProyeccion { get; set; }
        public int? IdConfiguracionProyeccionFur { get; set; } 
        public int IdEstadoProyeccionFur { get; set; }
        public int IdArea { get; set; }
        public string? Observacion { get; set; }
    }

    public class CabeceraFurConfiguracionAutomaticaFrontDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; } = null!;
        public string Observacion { get; set; } = null!;
        public int IdArea { get; set; }
        public string? Usuario { get; set; }
    }

    public class FiltroBusquedaCabeceraFCADTO
    {
        public int? IdEstadoSolicitud { get; set; } = 0;
        public int? IdArea { get; set; } = 0;
    }

    public class DetalleProyeccionFur
    {
        public int IdCabeceraConfiguracion { get; set; }
        public int IdArea { get; set; }
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> DetalleFurConfiguracionAutomatica { get; set; } = new List<FurConfiguracionAutomaticaVersionDetalleDTO>();

    }

    public class DetalleProyeccionJsonFur
    {
        public int IdCabeceraConfiguracion { get; set; }
        public int IdArea { get; set; }
        public string DetalleFurConfiguracionAutomatica { get; set; }

    }

    public class RechazoProyeccionDTO
    {
        public int Id { get; set; }
        public int IdConfiguracion { get; set; }
        public string observacion { get; set; }
    }
}
