using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilScoringCategoriaRepository : IGenericRepository<TProgramaGeneralPerfilScoringCategorium>
    {
        #region Metodos Base
        TProgramaGeneralPerfilScoringCategorium Add(ProgramaGeneralPerfilScoringCategoria entidad);
        TProgramaGeneralPerfilScoringCategorium Update(ProgramaGeneralPerfilScoringCategoria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilScoringCategorium> Add(IEnumerable<ProgramaGeneralPerfilScoringCategoria> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilScoringCategorium> Update(IEnumerable<ProgramaGeneralPerfilScoringCategoria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilScoringCategoria? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilScoringCategoria> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
