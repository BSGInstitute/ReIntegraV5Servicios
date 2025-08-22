namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public partial class CriterioCalificacionDTO
    {
        public string Nombre { get; set; } = null!;
        public string Sigla { get; set; } = null!;
        public bool EstadoDocumento { get; set; }
        public bool DocOriginal { get; set; }
        public bool DocPasarela { get; set; }
        public bool DocPasCancelados { get; set; }
    }

    public class ModalidadCriterioDTO
    {
        public List<int> Criterio { get; set; }
    }
}
