using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilCategoriaCoeficienteRepository : IGenericRepository<TProgramaGeneralPerfilCategoriaCoeficiente>
    {
        #region Metodos Base
        TProgramaGeneralPerfilCategoriaCoeficiente Add(ProgramaGeneralPerfilCategoriaCoeficiente entidad);
        TProgramaGeneralPerfilCategoriaCoeficiente Update(ProgramaGeneralPerfilCategoriaCoeficiente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilCategoriaCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilCategoriaCoeficiente> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilCategoriaCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilCategoriaCoeficiente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilCategoriaCoeficiente? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilCategoriaCoeficiente> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
