using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IAsesorChatMktService
    {
        #region Metodos Base
        AsesorChat Add(AsesorChat entidad);
        AsesorChat Update(AsesorChat entidad);
        bool Delete(int id, string usuario);

        List<AsesorChat> Add(List<AsesorChat> listadoEntidad);
        List<AsesorChat> Update(List<AsesorChat> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        ChatAsignadoNoAsignadoCompuestoDTO ObtenerChatAsignadosNoAsignados(FiltroCompuestroGrillaDTO paginador);
        ChatListaAsesoresCompuestoDTO ObtenerChatListaAsesores(FiltroCompuestroGrillaDTO paginador);
        ChatListaAsesoresCompuestoDTO ObtenerChatListaAsesores2();
    }
}
