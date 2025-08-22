using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITiempoLibreRepository : IGenericRepository<TTiempoLibre>
    {
        #region Metodos Base
        TTiempoLibre Add(TiempoLibre entidad);
        TTiempoLibre Update(TiempoLibre entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTiempoLibre> Add(IEnumerable<TiempoLibre> listadoEntidad);
        IEnumerable<TTiempoLibre> Update(IEnumerable<TiempoLibre> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TiempoLibreDTO> ObtenerTiempoLibre();
        TiempoLibreRADTO ObtenerTiempoLibreTipoUno();
        TiempoLibreRADTO ObtenerTiempoLibreTipoDos();
        ValorIntDTO ObtenerDiasReprogramacionAutomaticaOperaciones(int idOportunidad);
    }
}