using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AdicionalProgramaGeneral : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public string? Descripcion { get; set; }
        public string? NombreImagen { get; set; }

        public int? IdTitulo { get; set; }
        public string NombreTitulo { get; set; } = null!;

    }
}
