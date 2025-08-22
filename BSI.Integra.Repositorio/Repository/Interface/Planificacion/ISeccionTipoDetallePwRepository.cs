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
    public interface ISeccionTipoDetallePwRepository : IGenericRepository<TSeccionTipoDetallePw>
    {
        #region Metodos Base
        TSeccionTipoDetallePw Add(SeccionTipoDetallePw entidad);
        TSeccionTipoDetallePw Update(SeccionTipoDetallePw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSeccionTipoDetallePw> Add(IEnumerable<SeccionTipoDetallePw> listadoEntidad);
        IEnumerable<TSeccionTipoDetallePw> Update(IEnumerable<SeccionTipoDetallePw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SeccionTipoDetallePw> ObtenerPorIdSeccionPw(int id);
        SeccionTipoDetallePw ObtenerPorId(int id);
        IEnumerable<SeccionTipoDetallePwEstructuraProgramaDTO> ObtenerIdSeccionTipoDetallePorIdPGeneral(int idPGeneral);
    }
}
