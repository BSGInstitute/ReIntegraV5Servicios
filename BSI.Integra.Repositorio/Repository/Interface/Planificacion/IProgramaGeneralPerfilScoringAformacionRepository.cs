using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilScoringAformacionRepository : IGenericRepository<TProgramaGeneralPerfilScoringAformacion>
    {
        #region Metodos Base
        TProgramaGeneralPerfilScoringAformacion Add(ProgramaGeneralPerfilScoringAformacion entidad);
        TProgramaGeneralPerfilScoringAformacion Update(ProgramaGeneralPerfilScoringAformacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilScoringAformacion> Add(IEnumerable<ProgramaGeneralPerfilScoringAformacion> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilScoringAformacion> Update(IEnumerable<ProgramaGeneralPerfilScoringAformacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilScoringAformacion? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilScoringAformacion> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
