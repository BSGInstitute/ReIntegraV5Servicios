using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilEscalaProbabilidadRepository : IGenericRepository<TProgramaGeneralPerfilEscalaProbabilidad>
    {
        #region Metodos Base
        TProgramaGeneralPerfilEscalaProbabilidad Add(ProgramaGeneralPerfilEscalaProbabilidad entidad);
        TProgramaGeneralPerfilEscalaProbabilidad Update(ProgramaGeneralPerfilEscalaProbabilidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilEscalaProbabilidad> Add(IEnumerable<ProgramaGeneralPerfilEscalaProbabilidad> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilEscalaProbabilidad> Update(IEnumerable<ProgramaGeneralPerfilEscalaProbabilidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilEscalaProbabilidad? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilEscalaProbabilidad> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
