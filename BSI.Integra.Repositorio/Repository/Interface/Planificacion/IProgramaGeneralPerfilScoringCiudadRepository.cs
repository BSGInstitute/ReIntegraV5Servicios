using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilScoringCiudadRepository : IGenericRepository<TProgramaGeneralPerfilScoringCiudad>
    {
        #region Metodos Base
        TProgramaGeneralPerfilScoringCiudad Add(ProgramaGeneralPerfilScoringCiudad entidad);
        TProgramaGeneralPerfilScoringCiudad Update(ProgramaGeneralPerfilScoringCiudad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilScoringCiudad> Add(IEnumerable<ProgramaGeneralPerfilScoringCiudad> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilScoringCiudad> Update(IEnumerable<ProgramaGeneralPerfilScoringCiudad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilScoringCiudad? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilScoringCiudad> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
