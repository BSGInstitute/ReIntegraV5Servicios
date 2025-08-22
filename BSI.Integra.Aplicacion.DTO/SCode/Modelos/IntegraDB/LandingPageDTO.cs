namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class LandingPageDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPEspecifico { get; set; }
        public int IdTipo { get; set; }
        public int IdFormularioSolicitud { get; set; }
        public int IdProgramaGeneral { get; set; }
        public int IdPlantilla { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public string? NombrePrograma { get; set; }
        public string EstilosCongelados { get; set; }
        public string Cabecera { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string? Url { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }



    public class LandingPageEnvio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPEspecifico { get; set; }
        public int IdTipo { get; set; }
        public int IdFormularioSolicitud { get; set; }
        public int IdProgramaGeneral { get; set; }
        public int IdPlantilla { get; set; }
        public string? NombrePrograma { get; set; }

        public int IdCategoriaOrigen { get; set; }
        public string? EstilosCongelados { get; set; }
        public string? Cabecera { get; set; }
        public string Titulo { get; set; }
        public string? Subtitulo { get; set; }
        public string? Url { get; set; }
        public string? Usuario { get; set; } = null!;
    }

    public class LandingPageCombo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

    }

    public class LandingPageC
    {
        public int Id { get; set; }
        public string NombreLandingPage { get; set; }
        public int? IdPEspecifico { get; set; }
        public int IdTipo { get; set; }
        public int IdFormularioSolicitud { get; set; }
        public int IdProgramaGeneral { get; set; }

        public int IdPlantilla { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public string? Cabecera { get; set; }
        public string Titulo { get; set; }
        public string? Subtitulo { get; set; }
        public string NombreFormulario { get; set; }
        public string? NombrePrograma { get; set; }
        public string? Url { get; set; }
    }

}
