using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilScoringCargoRepository : IGenericRepository<TProgramaGeneralPerfilScoringCargo>
    {
        #region Metodos Base
        TProgramaGeneralPerfilScoringCargo Add(ProgramaGeneralPerfilScoringCargo entidad);
        TProgramaGeneralPerfilScoringCargo Update(ProgramaGeneralPerfilScoringCargo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilScoringCargo> Add(IEnumerable<ProgramaGeneralPerfilScoringCargo> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilScoringCargo> Update(IEnumerable<ProgramaGeneralPerfilScoringCargo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilScoringCargo? ObtenerPorId(int idPGeneral);
        IEnumerable<ProgramaGeneralPerfilScoringCargo> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
