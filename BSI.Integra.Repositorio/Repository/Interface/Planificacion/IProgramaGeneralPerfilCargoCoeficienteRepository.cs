using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilCargoCoeficienteRepository : IGenericRepository<TProgramaGeneralPerfilCargoCoeficiente>
    {
        #region Metodos Base
        TProgramaGeneralPerfilCargoCoeficiente Add(ProgramaGeneralPerfilCargoCoeficiente entidad);
        TProgramaGeneralPerfilCargoCoeficiente Update(ProgramaGeneralPerfilCargoCoeficiente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilCargoCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilCargoCoeficiente> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilCargoCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilCargoCoeficiente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilCargoCoeficiente? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilCargoCoeficiente> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
