

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialAdicionalAulaVirtualRegistroRepository
    {
        #region Metodos Base
        TMaterialAdicionalAulaVirtualRegistro Add(MaterialAdicionalAulaVirtualRegistro entidad);
        TMaterialAdicionalAulaVirtualRegistro Update(MaterialAdicionalAulaVirtualRegistro entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialAdicionalAulaVirtualRegistro> Add(IEnumerable<MaterialAdicionalAulaVirtualRegistro> listadoEntidad);
        IEnumerable<TMaterialAdicionalAulaVirtualRegistro> Update(IEnumerable<MaterialAdicionalAulaVirtualRegistro> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialAdicionalAulaVirtualRegistroDTO> ObtenerMaterialAdicionalDetalleRegistro(int idMaterialAdicional);
        IEnumerable<ValorDTO> ObtenerIdsPorIdMaterialAdicional(int idMaterialAdicional);
        MaterialAdicionalAulaVirtualRegistro ObtenerPorId(int id);
        MaterialAdicionalAulaVirtualRegistro ObtenerPorIdYIdMaterialAdicionalAulaVirtual(int id, int idMaterialAdicional);
    }
}
