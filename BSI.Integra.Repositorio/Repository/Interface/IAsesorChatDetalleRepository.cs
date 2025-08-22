using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAsesorChatDetalleRepository : IGenericRepository<TAsesorChatDetalle>
    {
        #region Metodos Base
        TAsesorChatDetalle Add(AsesorChatDetalle entidad);
        TAsesorChatDetalle Update(AsesorChatDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAsesorChatDetalle> Add(IEnumerable<AsesorChatDetalle> listadoEntidad);
        IEnumerable<TAsesorChatDetalle> Update(IEnumerable<AsesorChatDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<IdDTO> ObtenerPaisesPorIdAsesorChat(int idAsesorChat);
        List<IdDTO> ObtenerProgramasGeneralesPorIdAsesorChat(int idAsesorChat);
        List<IdDTO> ObtenerAreasCapacitacionPorIdAsesorChat(int idAsesorChat);
        List<IdDTO> ObtenerSubAreasCapacitacionPorIdAsesorChat(int idAsesorChat);
        void ActualizarAsesorChaDetalleYLog(int idAsesorChat, int idPersonal, string usuario, string listaProgramas, string listaPaises);
        void EliminarAsesorChatDetalle(int idAsesorChat, string nombreUsuario);
    }
}
