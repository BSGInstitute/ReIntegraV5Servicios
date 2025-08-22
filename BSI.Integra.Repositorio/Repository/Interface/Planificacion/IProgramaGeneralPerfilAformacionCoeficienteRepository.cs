using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilAformacionCoeficienteRepository : IGenericRepository<TProgramaGeneralPerfilAformacionCoeficiente>
    {
        #region Metodos Base
        TProgramaGeneralPerfilAformacionCoeficiente Add(ProgramaGeneralPerfilAformacionCoeficiente entidad);
        TProgramaGeneralPerfilAformacionCoeficiente Update(ProgramaGeneralPerfilAformacionCoeficiente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilAformacionCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilAformacionCoeficiente> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilAformacionCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilAformacionCoeficiente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilAformacionCoeficiente? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilAformacionCoeficiente> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
