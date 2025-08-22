
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialAdicionalAulaVirtualRepository
    {
        #region Metodos Base
        TMaterialAdicionalAulaVirtual Add(MaterialAdicionalAulaVirtual entidad);
        TMaterialAdicionalAulaVirtual Update(MaterialAdicionalAulaVirtual entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialAdicionalAulaVirtual> Add(IEnumerable<MaterialAdicionalAulaVirtual> listadoEntidad);
        IEnumerable<TMaterialAdicionalAulaVirtual> Update(IEnumerable<MaterialAdicionalAulaVirtual> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialAdicionalAulaVirtualDTO> ObtenerMaterialAdicional();
        MaterialAdicionalAulaVirtual ObtenerPorId(int idMaterialAdicional);
        MaterialAdicionalAulaVirtualDTO ObtenerMaterialAdicionalDetalle(int idMaterialAdicional);
        IEnumerable<ValorDTO> ObtenerIdsPorIdMaterialAdicional(int idMaterialAdicional);
        IEnumerable<ComboDTO> ObtenerMarcadorCombo();
        bool NotificacionMaterialAdicional(int idMaterialAdicional, int idPEspecifico, string usuario);
    }
}
