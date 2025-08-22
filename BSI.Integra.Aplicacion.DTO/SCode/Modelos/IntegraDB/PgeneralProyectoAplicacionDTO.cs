using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PgeneralProyectoAplicacionDTO
    {
        public int Id { get; set; }
        public List<PgeneralProyectoAplicacionModalidadDTO> PGeneralProyectoAplicacionModalidad { get; set; }
        public List<PgeneralProyectoAplicacionProveedorDTO> PGeneralProyectoAplicacionProveedor { get; set; }
    }
    public class PgeneralProyectoAplicacionAlternoDTO
    {
        public int Id { get; set; }
        public List<int> Modalidades { get; set; }
        public List<int> Proveedores { get; set; }
    }
    public class ProveedorEmailDTO
    {
        public List<int> IdModalidadCurso { get; set; }
        public int IdProveedor { get; set; }
    }
}