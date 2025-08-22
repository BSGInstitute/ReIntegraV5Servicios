using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LandingPage : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdPEspecifico { get; set; }
        public int IdTipo { get; set; }
        public int IdFormularioSolicitud { get; set; }
        public int IdProgramaGeneral { get; set; }
        public int IdPlantilla { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public string EstilosCongelados { get; set; }
        public string? Cabecera { get; set; }
        public string Titulo { get; set; }
        public string? Subtitulo { get; set; }
        public string Url { get; set; }
        public string? Uauario { get; set; }

    }
}
