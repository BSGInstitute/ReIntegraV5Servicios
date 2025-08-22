using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.PersonalHorarioDTO;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPersonalHorarioRepository : IGenericRepository<TPersonalHorario>
    {
        #region Metodos Base
        TPersonalHorario Add(PersonalHorario entidad);
        TPersonalHorario Update(PersonalHorario entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalHorario> Add(IEnumerable<PersonalHorario> listadoEntidad);
        IEnumerable<TPersonalHorario> Update(IEnumerable<PersonalHorario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PersonalHorarioDTO> ObtenerPersonalHorario();
        HorarioAsesorDTO ObtenerHorarioAsesor(int idPersonal);
        public List<HorarioDTO> ObtenerHorario(int idPersonal);
        List<PersonalHorario> ObtenerPorIdPersonal(int idPersonal);
    }
}