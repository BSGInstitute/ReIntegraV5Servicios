using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialAsociacionCriterioVerificacionRepository
    {
        #region Metodos Base
        TMaterialAsociacionCriterioVerificacion Add(MaterialAsociacionCriterioVerificacion entidad);
        TMaterialAsociacionCriterioVerificacion Update(MaterialAsociacionCriterioVerificacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialAsociacionCriterioVerificacion> Add(IEnumerable<MaterialAsociacionCriterioVerificacion> listadoEntidad);
        IEnumerable<TMaterialAsociacionCriterioVerificacion> Update(IEnumerable<MaterialAsociacionCriterioVerificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialAsociacionCriterioVerificacion> ObtenerPorIdMaterialTipo(int idMaterialTipo);
        Task<List<MaterialDetalleCriterioVerificacionDTO>> ObtenerCriteriosVerificacionPorMaterialDetalleAsync(int idMaterialPEspecificoDetalle);
    }
}
