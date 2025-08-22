using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFlujoRepository : IGenericRepository<TFlujo>
    {
        #region Metodos Base
        TFlujo Add(Flujo entidad);
        TFlujo Update(Flujo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFlujo> Add(IEnumerable<Flujo> listadoEntidad);
        IEnumerable<TFlujo> Update(IEnumerable<Flujo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Flujo? ObtenerPorId(int id);
        IEnumerable<FlujoDetalleDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerComboClasificacionUbicacionDocente();
    }
}
