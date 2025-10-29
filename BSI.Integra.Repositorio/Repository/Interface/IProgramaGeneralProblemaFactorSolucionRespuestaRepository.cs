using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralProblemaFactorSolucionRespuestaSolucionRespuestaRepository : IGenericRepository<TProgramaGeneralProblemaFactorSolucionRespuestum>
    {
        #region Metodos Base
        TProgramaGeneralProblemaFactorSolucionRespuestum Add(ProgramaGeneralProblemaFactorSolucionRespuesta entidad);
        TProgramaGeneralProblemaFactorSolucionRespuestum Update(ProgramaGeneralProblemaFactorSolucionRespuesta entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaFactorSolucionRespuestum> Add(IEnumerable<ProgramaGeneralProblemaFactorSolucionRespuesta> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaFactorSolucionRespuestum> Update(IEnumerable<ProgramaGeneralProblemaFactorSolucionRespuesta> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralProblemaFactorSolucionRespuesta ObtenerPorIdOportunidadIdProblemaFactorSolucion(int idOportunidad, int idProblemaFactorSolucion);
    }
}
