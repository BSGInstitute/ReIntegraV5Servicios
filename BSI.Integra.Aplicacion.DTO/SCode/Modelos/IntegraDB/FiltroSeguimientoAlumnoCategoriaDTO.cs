namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FiltroSeguimientoAlumnoCategoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdTipoSeguimientoAlumnoCategoria { get; set; }
        public bool AplicaModalidadOnline { get; set; }
        public bool AplicaModalidadAonline { get; set; }
        public bool AplicaModalidadPresencial { get; set; }
    }
    public class SeguimientoAlumnoCategoriaEntradaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int IdTipoSeguimientoAlumnoCategoria { get; set; }
        public bool? AplicaModalidadOnline { get; set; }
        public bool? AplicaModalidadAonline { get; set; }
        public bool? AplicaModalidadPresencial { get; set; }
        public int idEstadoMatricula { get; set; }
        public List<int> idSubEstadoMatricula { get; set; }
        public string Usuario { get; set; }
        
    }


    public class ComentarioConfiguracionDTO
    {
        public int IdTipoSeguimiento { get; set; }
        public string NombreTipoSeguimiento { get; set; }
        public int? IdTipoSeguimientoCategoria { get; set; }
        public string NombreSeguimientoCategoria { get; set; }
        public int?  IdEstadoMatricula{ get; set; }
        public string EstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
    }

    public class ComentarioConfiguracionAgrupadoDTO
    {
        public int IdTipoSeguimiento { get; set; }
        public string NombreTipoSeguimiento { get; set; }
        public int? IdTipoSeguimientoCategoria { get; set; }
        public string NombreSeguimientoCategoria { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public List<int> IdSubEstadoMatricula { get; set; }
        public List<string> SubEstadoMatricula { get; set; }
    }
}
