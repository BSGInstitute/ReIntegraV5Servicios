namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CampoContactoDTO
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string NombreLabel { get; set; }
        public string TipoControl { get; set; } = null!;
        public int ValoresPreEstablecidos { get; set; }
        public string? Procedimiento { get; set; }

        


    }
    public class CampoContactoFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;


    }
    public class CampoContactoTodoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Procedimiento { get; set; } = null!;
        public string TipoControl { get; set; } = null!;
        public int ValoresPreEstablecidos { get; set; }
    }



}
