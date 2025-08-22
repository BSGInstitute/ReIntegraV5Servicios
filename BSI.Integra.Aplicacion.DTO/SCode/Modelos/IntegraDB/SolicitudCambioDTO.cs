namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SolicitudCambioDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int Version { get; set; }
        public string NombreSolicitante { get; set; }
        public DateTime Fecha { get; set; }
        public string Observacion { get; set; }
        public string Cambios { get; set; }
        public string IdsCambios { get; set; }
    }
    public class SolicitudVisualizarOportunidadDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPersonal { get; set; }

    }
    public class ClienteWolkVoxDTO
    {
        public string Numero { get; set; }

    }
    public class ResultadoWolkvoxDTO
    {
        public int IdSkill { get; set; }

    }
}
