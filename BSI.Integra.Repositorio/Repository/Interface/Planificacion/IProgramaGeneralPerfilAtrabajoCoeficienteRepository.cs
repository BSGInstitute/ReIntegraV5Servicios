using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilAtrabajoCoeficienteRepository : IGenericRepository<TProgramaGeneralPerfilAtrabajoCoeficiente>
    {
        #region Metodos Base
        TProgramaGeneralPerfilAtrabajoCoeficiente Add(ProgramaGeneralPerfilAtrabajoCoeficiente entidad);
        TProgramaGeneralPerfilAtrabajoCoeficiente Update(ProgramaGeneralPerfilAtrabajoCoeficiente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilAtrabajoCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilAtrabajoCoeficiente> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilAtrabajoCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilAtrabajoCoeficiente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilAtrabajoCoeficiente? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilAtrabajoCoeficiente> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
