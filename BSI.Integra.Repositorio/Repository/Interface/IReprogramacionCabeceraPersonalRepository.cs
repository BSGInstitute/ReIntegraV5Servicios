using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReprogramacionCabeceraPersonalRepository : IGenericRepository<TReprogramacionCabeceraPersonal>
    {
        #region Metodos Base
        TReprogramacionCabeceraPersonal Add(ReprogramacionCabeceraPersonal entidad);
        TReprogramacionCabeceraPersonal AddAsync(ReprogramacionCabeceraPersonal entidad);
        TReprogramacionCabeceraPersonal Update(ReprogramacionCabeceraPersonal entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TReprogramacionCabeceraPersonal> Add(IEnumerable<ReprogramacionCabeceraPersonal> listadoEntidad);
        IEnumerable<TReprogramacionCabeceraPersonal> Update(IEnumerable<ReprogramacionCabeceraPersonal> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ReprogramacionCabeceraPersonalDTO> ObtenerReprogramacionCabeceraPersonal();
        ReprogramacionCabeceraPersonal? ObtenerPorIdActividadCabeceraIdCategoriaOrigenIdPersonal(int idActividadCabecera, int idCategoriaOrigen, int idPersonal);
        Task<ReprogramacionCabeceraPersonal>? ObtenerPorIdActividadCabeceraIdCategoriaOrigenIdPersonalAsync(int idActividadCabecera, int idCategoriaOrigen, int idPersonal);
    }
}