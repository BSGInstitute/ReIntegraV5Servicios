using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICajaEgresoRepository : IGenericRepository<TCajaEgreso>
    {
        #region Metodos Base
        TCajaEgreso Add(CajaEgreso entidad);
        TCajaEgreso Update(CajaEgreso entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCajaEgreso> Add(IEnumerable<CajaEgreso> listadoEntidad);
        IEnumerable<TCajaEgreso> Update(IEnumerable<CajaEgreso> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CajaEgresoDTO> ObtenerCajaEgresoEnviado(FiltroCajaEgresoDTO filtro);
        IEnumerable<CajaPorRendirCombosDTO> ObtenerCajaPorRendirSolicitanteREC(int idPersonalResponsable);
        IEnumerable<CajaEgresoGenerarPdfDTO> ObtenerCajaEgresoAprobadoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja);
        IEnumerable<CajaEgresoGenerarPdfDTO> ObtenerDatosCajaEgreso(int[] IdEgresoCaja);
        List<CajaEgresoDTO> ObtenerRegistroCajaEgreso(int Id);
        List<CajaEgresoDTO> ObtenerRegistrosCajaEgreso(int IdCajaPorRendirCabecera);



    }
}
