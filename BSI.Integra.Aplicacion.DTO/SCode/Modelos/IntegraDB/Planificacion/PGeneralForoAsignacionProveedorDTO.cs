
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PGeneralForoAsignacionProveedorDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
        public int IdProveedor { get; set; }
    }
    public class PgeneralForoAsignacionProveedorAlternoDTO
    {
        public int IdModalidadCurso { get; set; }
        public List<int> Proveedores { get; set; }
    }
}
