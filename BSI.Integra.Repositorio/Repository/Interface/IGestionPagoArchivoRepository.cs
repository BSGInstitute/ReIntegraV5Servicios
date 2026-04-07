using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IGestionPagoArchivoRepository : IGenericRepository<TGestionPagoArchivo>
    {
        #region Metodos Base
        TGestionPagoArchivo Add(GestionPagoArchivo entidad);
        TGestionPagoArchivo Update(GestionPagoArchivo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TGestionPagoArchivo> Add(IEnumerable<GestionPagoArchivo> listadoEntidad);
        IEnumerable<TGestionPagoArchivo> Update(IEnumerable<GestionPagoArchivo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<GestionPagoArchivoDTO> ObtenerArchivosPorGestionPago(int idGestionPago);
        IEnumerable<GestionPagoArchivoDTO> ObtenerArchivosPorCronograma(int idGestionPagoCronograma);
    }
}
