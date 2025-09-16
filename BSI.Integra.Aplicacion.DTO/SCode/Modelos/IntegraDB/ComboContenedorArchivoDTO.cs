namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ComboContenedorArchivoDTO
    {
        public int IdContenedor { get; set; }
        public string Contenedor { get; set; }
        public int? IdSubcontenedor { get; set; }
        public bool AplicaSubcontenedores { get; set; }
        public bool AplicaSubidaMultiple { get; set; }
        public bool AplicaValidacion { get; set; }


    }
    public class ContenedorArchivoCompletoDTO
    {
        public int IdContenedor { get; set; }
        public string Contenedor { get; set; }
        public int IdProveedorNube { get; set; }
        public string Subdominio { get; set; }
        public bool AplicaSubcontenedores { get; set; }
        public bool AplicaSubidaMultiple { get; set; }
        public bool AplicaValidacion { get; set; }
        public int IdSubContenedor { get; set; }
        public string Subcontenedor { get; set; }
    }
}

