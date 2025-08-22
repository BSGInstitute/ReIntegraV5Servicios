using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEtiquetaRepository : IGenericRepository<TEtiquetum>
    {
        #region Metodos Base
        TEtiquetum Add(Etiqueta entidad);
        TEtiquetum Update(Etiqueta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TEtiquetum> Add(IEnumerable<Etiqueta> listadoEntidad);
        IEnumerable<TEtiquetum> Update(IEnumerable<Etiqueta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<EtiquetaDTO> ObtenerEtiqueta();
        IEnumerable<EtiquetaComboDTO> ObtenerCombo();
        IEnumerable<Etiqueta> ObtenerPorIdNodoPadre(int idNodoPadre);
        Task<IEnumerable<Etiqueta>> ObtenerPorIdNodoPadreAsync(int idNodoPadre);
    }
}