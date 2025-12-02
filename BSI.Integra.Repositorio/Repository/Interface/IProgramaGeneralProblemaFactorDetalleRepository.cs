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
    public interface IProgramaGeneralProblemaFactorDetalleRepository : IGenericRepository<TProgramaGeneralProblemaFactorDetalle>
    {
        #region Metodos Base
        TProgramaGeneralProblemaFactorDetalle Add(ProgramaGeneralProblemaFactorDetalle entidad);
        TProgramaGeneralProblemaFactorDetalle Update(ProgramaGeneralProblemaFactorDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralProblemaFactorDetalle> Add(IEnumerable<ProgramaGeneralProblemaFactorDetalle> listadoEntidad);
        IEnumerable<TProgramaGeneralProblemaFactorDetalle> Update(IEnumerable<ProgramaGeneralProblemaFactorDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralProblemaFactorDetalleDTO> Obtener();
        ProgramaGeneralProblemaFactorDetalle? ObtenerPorId(int id);

    }
}
