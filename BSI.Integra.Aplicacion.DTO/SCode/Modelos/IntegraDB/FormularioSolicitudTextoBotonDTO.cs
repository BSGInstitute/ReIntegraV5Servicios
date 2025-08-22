namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FormularioSolicitudTextoBotonDTO
    {

        public int Id { get; set; }

        public string TextoBoton { get; set; } = null!;

        public string? Descripcion { get; set; }

        public bool PorDefecto { get; set; }

       
    }

    public class FormularioSolicitudTextoBotonFiltroDTO
    {
        public int Id { get; set; }
        public string TextoBoton { get; set; } = null!;
    }
}

