using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilScoringAtrabajoRepository : IGenericRepository<TProgramaGeneralPerfilScoringAtrabajo>
    {
        #region Metodos Base
        TProgramaGeneralPerfilScoringAtrabajo Add(ProgramaGeneralPerfilScoringAtrabajo entidad);
        TProgramaGeneralPerfilScoringAtrabajo Update(ProgramaGeneralPerfilScoringAtrabajo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilScoringAtrabajo> Add(IEnumerable<ProgramaGeneralPerfilScoringAtrabajo> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilScoringAtrabajo> Update(IEnumerable<ProgramaGeneralPerfilScoringAtrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilScoringAtrabajo? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilScoringAtrabajo> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
