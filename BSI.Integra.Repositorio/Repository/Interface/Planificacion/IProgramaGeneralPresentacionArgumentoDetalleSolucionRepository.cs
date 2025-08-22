using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPresentacionArgumentoDetalleSolucionRepository : IGenericRepository<TProgramaGeneralPresentacionArgumentoDetalleSolucion>
    {
        #region Metodos Base
        TProgramaGeneralPresentacionArgumentoDetalleSolucion Add(ProgramaGeneralPresentacionArgumentoDetalleSolucion entidad);
        TProgramaGeneralPresentacionArgumentoDetalleSolucion Update(ProgramaGeneralPresentacionArgumentoDetalleSolucion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TProgramaGeneralPresentacionArgumentoDetalleSolucion> Add(IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucion> listadoEntidad);
        IEnumerable<TProgramaGeneralPresentacionArgumentoDetalleSolucion> Update(IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO> ObtenerProgramaGeneralPresentacionArgumentoDetalleSolucion();
        IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucionComboDTO> ObtenerCombo();
        ProgramaGeneralPresentacionArgumentoDetalleSolucion ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucionAgendaDTO> ObtenerProgramaGeneralPresentacionArgumentoDetalleSolucionParaAgenda(int idProgramaGeneralPresentacionArgumento, int idOportunidad);
        IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleSolucionDTO> ObtenerPresentacionArgumentoDetalleSolucionPorIdPresentacionArgumento(int idPresentacionArgumento);
    }
}
