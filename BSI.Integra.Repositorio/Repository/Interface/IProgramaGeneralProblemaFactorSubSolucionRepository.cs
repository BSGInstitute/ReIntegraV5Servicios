using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralProblemaFactorSubSolucionRepository : IGenericRepository<TProgramaGeneralProblemaFactorSubSolucion>
    {
        #region Metodos Base
        TProgramaGeneralProblemaFactorSubSolucion Add(ProgramaGeneralProblemaFactorSubSolucion entidad);
        TProgramaGeneralProblemaFactorSubSolucion Update(ProgramaGeneralProblemaFactorSubSolucion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaFactorSubSolucion> Add(IEnumerable<ProgramaGeneralProblemaFactorSubSolucion> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaFactorSubSolucion> Update(IEnumerable<ProgramaGeneralProblemaFactorSubSolucion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO> Obtener();
        IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO> ObtenerPorIdProgramaGeneralProblemaFactorSolucion(int idProgramaGeneralProblemaFactorSolucion);
        ProgramaGeneralProblemaFactorSubSolucion? ObtenerPorId(int id);
        Task<IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO>> ObtenerAsync();
    }
}
