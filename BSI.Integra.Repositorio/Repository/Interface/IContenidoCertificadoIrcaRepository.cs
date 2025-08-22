using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IContenidoCertificadoIrcaRepository : IGenericRepository<TContenidoCertificadoIrca>
    {
        #region Metodos Base
        TContenidoCertificadoIrca Add(ContenidoCertificadoIrca entidad);
        TContenidoCertificadoIrca Update(ContenidoCertificadoIrca entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TContenidoCertificadoIrca> Add(IEnumerable<ContenidoCertificadoIrca> listadoEntidad);
        IEnumerable<TContenidoCertificadoIrca> Update(IEnumerable<ContenidoCertificadoIrca> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ContenidoCertificadoIrca? ObtenerPorId(int id);
        string? ObtenerDescripcionResultadoIrca(int idContenidoCertificadoIrca);
        VistaPreviaCertificadoIrcaDTO? ObtenerValoresVistaPreviaIrca(int idPgeneral);
        void InsertarListaContenidoCertificadoIrca(List<ContenidoCertificadoIrcaDTO> objs);
    }
}
