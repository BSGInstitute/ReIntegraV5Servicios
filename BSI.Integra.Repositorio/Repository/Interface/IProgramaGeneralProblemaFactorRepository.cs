using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralProblemaFactorRepository : IGenericRepository<TProgramaGeneralProblemaFactor>
    {
        #region Metodos Base
        TProgramaGeneralProblemaFactor Add(ProgramaGeneralProblemaFactor entidad);
        TProgramaGeneralProblemaFactor Update(ProgramaGeneralProblemaFactor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaFactor> Add(IEnumerable<ProgramaGeneralProblemaFactor> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaFactor> Update(IEnumerable<ProgramaGeneralProblemaFactor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaFactorDTO> Obtener();
        ProgramaGeneralProblemaFactor? ObtenerPorId(int id);
        Task<IEnumerable<ProgramaGeneralProblemaFactorDTO>> ObtenerAsync();
    }
}
