using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDetraccionRepository : IGenericRepository<TDetraccion>
    {
        #region Metodos Base
        TDetraccion Add(Detraccion entidad);
        TDetraccion Update(Detraccion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDetraccion> Add(IEnumerable<Detraccion> listadoEntidad);
        IEnumerable<TDetraccion> Update(IEnumerable<Detraccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DetraccionComboDTO> ObtenerCombo();
        IEnumerable<ReporteDetraccionDTO> ObtenerReporteDetraccion(string? idSede, string? FechaInicial, string? FechaFinal, int? idProveedor);
        IEnumerable<DetraccionComboDTO> ObtenerValorDetraccionPorPais(int IdPais);
    }
}