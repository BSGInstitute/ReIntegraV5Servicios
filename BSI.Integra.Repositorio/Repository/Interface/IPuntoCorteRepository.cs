using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPuntoCorteRepository : IGenericRepository<TPuntoCorte>
    {
        #region Metodos Base
        TPuntoCorte Add(PuntoCorte entidad);
        TPuntoCorte Update(PuntoCorte entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPuntoCorte> Add(IEnumerable<PuntoCorte> listadoEntidad);
        IEnumerable<TPuntoCorte> Update(IEnumerable<PuntoCorte> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ComboDTO> ObtenerCombo();
    }
}
