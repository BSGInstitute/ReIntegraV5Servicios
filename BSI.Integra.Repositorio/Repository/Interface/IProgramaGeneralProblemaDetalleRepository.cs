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
    public interface IProgramaGeneralProblemaDetalleRepository
    {
        #region Metodos Base
        TProgramaGeneralProblemaDetalle Add(ProgramaGeneralProblemaDetalle entidad);
        TProgramaGeneralProblemaDetalle Update(ProgramaGeneralProblemaDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaDetalle> Add(IEnumerable<ProgramaGeneralProblemaDetalle> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaDetalle> Update(IEnumerable<ProgramaGeneralProblemaDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        ProgramaGeneralProblemaDetalle? ObtenerPorId(int id);
        IEnumerable<ProblemaClienteByPGeneral> Obtener(int idPGeneral);
        IEnumerable<ProblemaAgendaRow> ObtenerProblemasClienteAgendaV6(int idPGeneral, int idOportundad);

    }
}
