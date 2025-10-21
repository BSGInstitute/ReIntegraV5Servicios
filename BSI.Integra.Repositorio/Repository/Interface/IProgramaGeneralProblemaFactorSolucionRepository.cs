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
    public interface IProgramaGeneralProblemaFactorSolucionRepository : IGenericRepository<TProgramaGeneralProblemaFactorSolucion>
    {
        #region Metodos Base
        TProgramaGeneralProblemaFactorSolucion Add(ProgramaGeneralProblemaFactorSolucion entidad);
        TProgramaGeneralProblemaFactorSolucion Update(ProgramaGeneralProblemaFactorSolucion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaFactorSolucion> Add(IEnumerable<ProgramaGeneralProblemaFactorSolucion> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaFactorSolucion> Update(IEnumerable<ProgramaGeneralProblemaFactorSolucion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaFactorSolucionDTO> Obtener();
        ProgramaGeneralProblemaFactorSolucion? ObtenerPorId(int id);

    }
}
