using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IHoraBloqueadaRepository : IGenericRepository<THoraBloqueadum>
    {
        #region Metodos Base
        THoraBloqueadum Add(HoraBloqueada entidad);
        THoraBloqueadum AddAsync(HoraBloqueada entidad);
        THoraBloqueadum Update(HoraBloqueada entidad);
        bool Delete(int id, string usuario);

        IEnumerable<THoraBloqueadum> Add(IEnumerable<HoraBloqueada> listadoEntidad);
        IEnumerable<THoraBloqueadum> Update(IEnumerable<HoraBloqueada> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<HoraBloqueadaDTO> ObtenerHoraBloqueada();
        List<HoraBloqueadaRADTO> ObtenerHorasBloquedasReprogramacionPorAsesor(int idPersonal, DateTime fecha);
        bool ReprogramarAlumnoClasesOnline(int idAlumno);
    }
}