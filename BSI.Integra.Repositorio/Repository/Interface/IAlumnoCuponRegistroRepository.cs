using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAlumnoCuponRegistroRepository : IGenericRepository<TAlumnoCuponRegistro>
    {
        #region Metodos Base
        TAlumnoCuponRegistro Add(AlumnoCuponRegistro entidad);
        TAlumnoCuponRegistro Update(AlumnoCuponRegistro entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAlumnoCuponRegistro> Add(IEnumerable<AlumnoCuponRegistro> listadoEntidad);
        IEnumerable<TAlumnoCuponRegistro> Update(IEnumerable<AlumnoCuponRegistro> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<AlumnoCuponRegistroDTO> ObtenerAlumnoCuponRegistro();
        IEnumerable<AlumnoCuponRegistroComboDTO> ObtenerCombo();
    }
}