using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPgeneralAsubPgeneralRepository : IGenericRepository<TPgeneralAsubPgeneral>
    {
        #region Metodos Base
        TPgeneralAsubPgeneral Add(PgeneralAsubPgeneral entidad);
        TPgeneralAsubPgeneral Update(PgeneralAsubPgeneral entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPgeneralAsubPgeneral> Add(IEnumerable<PgeneralAsubPgeneral> listadoEntidad);
        IEnumerable<TPgeneralAsubPgeneral> Update(IEnumerable<PgeneralAsubPgeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PgeneralAsubPgeneralDTO> ObtenerPGeneralASubPGeneral();
        IEnumerable<PGeneralASubPGeneralComboDTO> ObtenerCombo();
        List<PgeneralHijoDTO> ObtenerPGeneralHijos(int idPgeneral);
        Task<List<PgeneralHijoDTO>> ObtenerPGeneralHijosAsync(int idPgeneral);
        List<PgeneralHijoDTO> ObtenerPGeneralHijosVersion(int idPgeneral, string version);
        List<CursoHijoIdDTO> ObtenerCursosCongelamientoEstrucuraCurricular(int idMatriculaCabecera);
        List<CursoHijoDuracionDTO> ObtenerCursosEstrucuraCurricular(int idMatriculaCabecera);
        List<CursoHijoDuracionDTO> ObtenerCursosCongeladosEstrucuraCurricular(int idMatriculaCabecera);
        Task<IEnumerable<ComboDTO>> ObtenerModuloAsync();
        Task<IEnumerable<ComboDTO>> ObtenerCicloAsync();
        PgeneralAsubPgeneral? ObtenerPorId(int id);
        IEnumerable<PgeneralAsubPgeneral> ObtenerPorIds(List<int> id);
        IEnumerable<PgeneralAsubPgeneral> ObtenerPorIdPgeneralPadre(int idPgeneralPadre);
        IEnumerable<PgeneralAsubPgeneralCursoHijoDTO> ObtenerCursosHijosPorIdPgeneral(int idPGeneral);
        PgeneralAsubPgeneralCursoHijoDTO? ObtenerCursosHijosPorIdSubPgeneral(int idSubPgeneral);
    }
}
