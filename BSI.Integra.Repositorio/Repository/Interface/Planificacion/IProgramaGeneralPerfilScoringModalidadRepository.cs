using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilScoringModalidadRepository : IGenericRepository<TProgramaGeneralPerfilScoringModalidad>
    {
        #region Metodos Base
        TProgramaGeneralPerfilScoringModalidad Add(ProgramaGeneralPerfilScoringModalidad entidad);
        TProgramaGeneralPerfilScoringModalidad Update(ProgramaGeneralPerfilScoringModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilScoringModalidad> Add(IEnumerable<ProgramaGeneralPerfilScoringModalidad> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilScoringModalidad> Update(IEnumerable<ProgramaGeneralPerfilScoringModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilScoringModalidad? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilScoringModalidad> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
