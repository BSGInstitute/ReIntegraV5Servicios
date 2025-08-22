
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialAdicionalAulaVirtualPespecificoRepository
    {
        #region Metodos Base
        TMaterialAdicionalAulaVirtualPespecifico Add(MaterialAdicionalAulaVirtualPespecifico entidad);
        TMaterialAdicionalAulaVirtualPespecifico Update(MaterialAdicionalAulaVirtualPespecifico entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialAdicionalAulaVirtualPespecifico> Add(IEnumerable<MaterialAdicionalAulaVirtualPespecifico> listadoEntidad);
        IEnumerable<TMaterialAdicionalAulaVirtualPespecifico> Update(IEnumerable<MaterialAdicionalAulaVirtualPespecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<int> ObtenerIdsMaterialAdicionalDetallePespecifico(int idMaterialAdicional);
        IEnumerable<ValorDTO> ObtenerIdsPorIdMaterialAdicional(int idMaterialAdicional);
        MaterialAdicionalAulaVirtualPespecifico ObtenerPorIdPespecificoIdMaterialAdicional(int idMaterialAdicional, int idPEspecifico);
    }
}
