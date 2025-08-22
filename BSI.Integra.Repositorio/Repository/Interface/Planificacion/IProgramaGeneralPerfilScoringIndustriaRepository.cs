using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilScoringIndustriaRepository : IGenericRepository<TProgramaGeneralPerfilScoringIndustrium>
    {
        #region Metodos Base
        TProgramaGeneralPerfilScoringIndustrium Add(ProgramaGeneralPerfilScoringIndustria entidad);
        TProgramaGeneralPerfilScoringIndustrium Update(ProgramaGeneralPerfilScoringIndustria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilScoringIndustrium> Add(IEnumerable<ProgramaGeneralPerfilScoringIndustria> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilScoringIndustrium> Update(IEnumerable<ProgramaGeneralPerfilScoringIndustria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilScoringIndustria? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilScoringIndustria> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
