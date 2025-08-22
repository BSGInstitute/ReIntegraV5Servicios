using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    public class PgeneralAsubPgeneralDTO
    {
        public int Id { get; set; }
        public int IdPgeneralPadre { get; set; }
        public int IdPgeneralHijo { get; set; }
        public int? Orden { get; set; }
        public bool? EsVisiblePortal { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdModulo { get; set; }
    }
    public class PgeneralAsubPgeneralCursoHijoDTO
    {
        public int IdTroncalGeneral { get; set; }
        public string Nombre { get; set; }
        public int IdCurso { get; set; }
        public int IdPadre { get; set; }
        public int? Orden { get; set; }
        public bool? EsVisiblePortal { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdModulo { get; set; }
        public List<int> Versiones { get; set; }
    }
    public class PgeneralAsubPgeneralInsertarDTO
    {
        public int IdPgeneralPadre { get; set; }
        public int IdPgeneralHijo { get; set; }
    }
    public class PGeneralASubPGeneralActualizarDTO
    {
        public int Id { get; set; }
        public int? Orden { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdModulo { get; set; }
        public List<int> Versiones { get; set; }
    }
    public class PGeneralASubPGeneralComboDTO
    {
        public int Id { get; set; }
        public string PGeneralPadre { get; set; } = null!;
        public string PGeneralHijo { get; set; } = null!;
    }
    public class PgeneralHijoDTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Pg_titulo { get; set; }
        public string pw_duracion { get; set; }
        public List<SeccionDocumentoDTO> ListaSeccion { get; set; } = new List<SeccionDocumentoDTO>();
    }
}
