using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialPespecificoRepository : IGenericRepository<TMaterialPespecifico>
    {
        #region Metodos Base
        TMaterialPespecifico Add(MaterialPespecifico entidad);
        TMaterialPespecifico Update(MaterialPespecifico entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialPespecifico> Add(IEnumerable<MaterialPespecifico> listadoEntidad);
        IEnumerable<TMaterialPespecifico> Update(IEnumerable<MaterialPespecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ResultadoMaterialPEspecificoDetalleDTO> ObtenerMateriales(FiltroMaterialDTO dto);
        List<MaterialPespecificoDTO> ObtenerPorIdPEspecifico(int idPEspecifico);
        int ObtenerMaximoGrupoEdicion(int idPEspecifico);
        IEnumerable<ComboDTO> ObtenerGrupoEdicionDisponible(int idPEspecifico);
        MaterialPespecifico ObtenerPorId(int id);
        Task<List <ResultadoMaterialPEspecificoDetalleDTO>> ObtenerMaterialesGestionEnvioAsync(FiltroMaterialDTO filtro);
        Task<List<ComboDTO>> ObtenerMaterialAccionPorMaterialTipo(int idMaterialTipo);
        IEnumerable<MaterialPespecifico> ObtenerPorPEspecificoGrupo(int idPEspecifico, int grupoEdicion);
        List<PEspecificoFurDetalleDTO> ObtenerFursAsociadosPorIdPEspecifico(int idPEspecifico);
        Task<List<MaterialPEspecificoEntregaDTO>> ObtenerMaterialesRegistroEntrega(FiltroMaterialDTO filtro);
       

    }
}
