using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilIndustriaCoeficienteRepository : IGenericRepository<TProgramaGeneralPerfilIndustriaCoeficiente>
    {
        #region Metodos Base
        TProgramaGeneralPerfilIndustriaCoeficiente Add(ProgramaGeneralPerfilIndustriaCoeficiente entidad);
        TProgramaGeneralPerfilIndustriaCoeficiente Update(ProgramaGeneralPerfilIndustriaCoeficiente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilIndustriaCoeficiente> Add(IEnumerable<ProgramaGeneralPerfilIndustriaCoeficiente> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilIndustriaCoeficiente> Update(IEnumerable<ProgramaGeneralPerfilIndustriaCoeficiente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilIndustriaCoeficiente? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilIndustriaCoeficiente> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
