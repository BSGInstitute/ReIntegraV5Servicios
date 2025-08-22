using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPgeneralAsubPgeneralService
    {
        PgeneralAsubPgeneralCursoHijoDTO Insertar(PgeneralAsubPgeneralInsertarDTO pGeneralASubPGeneralDTO, string usuario);
        public PgeneralAsubPgeneralCursoHijoDTO Actualizar(PGeneralASubPGeneralActualizarDTO json, string userName);
        bool Eliminar(int idCursoPGeneral, string usuario);
        IEnumerable<PgeneralAsubPgeneralCursoHijoDTO> ObtenerCursosHijosPorIdPgeneral(int idPGeneral);
    }
}
