using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralProblemaFactorSubSolucionAsignadaRepository : IGenericRepository<TProgramaGeneralProblemaFactorSubSolucionAsignadum>
    {
        #region Metodos Base
        TProgramaGeneralProblemaFactorSubSolucionAsignadum Add(ProgramaGeneralProblemaFactorSubSolucionAsignada entidad);
        TProgramaGeneralProblemaFactorSubSolucionAsignadum Update(ProgramaGeneralProblemaFactorSubSolucionAsignada entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaFactorSubSolucionAsignadum> Add(IEnumerable<ProgramaGeneralProblemaFactorSubSolucionAsignada> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaFactorSubSolucionAsignadum> Update(IEnumerable<ProgramaGeneralProblemaFactorSubSolucionAsignada> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ProgramaGeneralProblemaFactorSubSolucionAsignada> ObtenerPorIdProblemaDetalle(int idProblemaDetalle);
        ProgramaGeneralProblemaFactorSubSolucionAsignada? ObtenerPorId(int id);
    }
}
