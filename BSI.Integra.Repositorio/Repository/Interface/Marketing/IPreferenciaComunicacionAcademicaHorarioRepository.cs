using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface IPreferenciaComunicacionAcademicaHorarioRepository : IGenericRepository<TPreferenciaComunicacionAcademicaHorario>
    {
        #region Metodos Base
        TPreferenciaComunicacionAcademicaHorario Add(PreferenciaComunicacionAcademicaHorario entidad);
        TPreferenciaComunicacionAcademicaHorario Update(PreferenciaComunicacionAcademicaHorario entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPreferenciaComunicacionAcademicaHorario> Add(IEnumerable<PreferenciaComunicacionAcademicaHorario> listadoEntidad);
        IEnumerable<TPreferenciaComunicacionAcademicaHorario> Update(IEnumerable<PreferenciaComunicacionAcademicaHorario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PreferenciaComunicacionAcademicaHorarioDTO> ObtenerPreferenciaHorarioComunicacionByIdAlumno(int IdAlumno);
    }
}
