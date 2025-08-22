using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IModalidadTrabajoRepository : IGenericRepository<TModalidadTrabajo>
    {
        #region Metodos Base
        TModalidadTrabajo Add(ModalidadTrabajo entidad);
        TModalidadTrabajo Update(ModalidadTrabajo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModalidadTrabajo> Add(IEnumerable<ModalidadTrabajo> listadoEntidad);
        IEnumerable<TModalidadTrabajo> Update(IEnumerable<ModalidadTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
