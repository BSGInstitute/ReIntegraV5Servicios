using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IAsesorChatDetalleService
    {
        #region Metodos Base
        AsesorChatDetalle Add(AsesorChatDetalle entidad);
        AsesorChatDetalle Update(AsesorChatDetalle entidad);
        bool Delete(int id, string usuario);

        List<AsesorChatDetalle> Add(List<AsesorChatDetalle> listadoEntidad);
        List<AsesorChatDetalle> Update(List<AsesorChatDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        List<IdDTO> ObtenerPaisesPorIdAsesorChat(int idAsesorChat);
        List<IdDTO> ObtenerProgramasGeneralesPorIdAsesorChat(int idAsesorChat);
        List<IdDTO> ObtenerAreasCapacitacionPorIdAsesorChat(int idAsesorChat);
        List<IdDTO> ObtenerSubAreasCapacitacionPorIdAsesorChat(int idAsesorChat);
        void ActualizarAsesorChaDetalleYLog(int idAsesorChat, int idPersonal, string usuario, string listaProgramas, string listaPaises);
        void EliminarAsesorChatDetalle(int idAsesorChat, string nombreUsuario);
    }
}
