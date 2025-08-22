using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPrestacionRegistroRepository : IGenericRepository<TPrestacionRegistro>
    {
        #region Metodos Base
        TPrestacionRegistro Add(PrestacionRegistro entidad);
        TPrestacionRegistro Update(PrestacionRegistro entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPrestacionRegistro> Add(IEnumerable<PrestacionRegistro> listadoEntidad);
        IEnumerable<TPrestacionRegistro> Update(IEnumerable<PrestacionRegistro> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PrestacionRegistroComboDTO> ObtenerCombo();
    }
}
