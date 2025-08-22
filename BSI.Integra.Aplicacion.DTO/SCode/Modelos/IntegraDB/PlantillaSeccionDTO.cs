namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PlantillaSeccionDTO
    {
        public int Id { get; set; }
        public int IdPlantillaV2 { get; set; }
        public int IdSeccion { get; set; }
        public string Usuario { get; set; }
        public bool Eliminado { get; set; }

        public List<PlantillaSeccionEstilo> Estilos { get; set; }
    }

    public class PlantillaSeccionEstilo
    {
        public int Id { get; set; }
        public int IdPlantillav2Seccion { get; set; }
        public int IdEstilo { get; set; }
        public string Valor { get; set; }
        public bool Eliminado { get; set; }
    }

    public class PSTodo
    {
        public int IdPlantillaSeccion { get; set; }
        public int IdPlantillaV2 { get; set; }
        public int IdSeccion { get; set; }
        public int? IdPlantillaSeccionEstilo { get; set; }
        public int? IdEstilo { get; set; }
        public string? valor { get; set; }
        public string NombreSeccion { get; set; }
        public bool EstadoTexto { get; set; }
        public string CodigoCss { get; set; }
        public string NombreEstilo { get; set; }
        public string NombreTipo { get; set; }
    }

    public class PSSeccion
    {
        public int IdPlantillaSeccion { get; set; }
        public int IdPlantillaV2 { get; set; }
        public int IdSeccion { get; set; }
        public string NombreSeccion { get; set; }
        public bool EstadoTexto { get; set; }

        public List<PSEstilo> PSEstilo { get; set; }

    }

    public class PSEstilo
    {
        public int IdPlantillaSeccionEstilo { get; set; }
        public int IdEstilo { get; set; }
        public string valor { get; set; }
        public string CodigoCss { get; set; }
        public string NombreEstilo { get; set; }
        public string NombreTipo { get; set; }
    }


}
