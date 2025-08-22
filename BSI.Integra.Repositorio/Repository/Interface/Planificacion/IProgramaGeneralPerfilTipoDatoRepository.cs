using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilTipoDatoRepository : IGenericRepository<TProgramaGeneralPerfilTipoDato>
    {
        #region Metodos Base
        TProgramaGeneralPerfilTipoDato Add(ProgramaGeneralPerfilTipoDato entidad);
        TProgramaGeneralPerfilTipoDato Update(ProgramaGeneralPerfilTipoDato entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilTipoDato> Add(IEnumerable<ProgramaGeneralPerfilTipoDato> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilTipoDato> Update(IEnumerable<ProgramaGeneralPerfilTipoDato> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilTipoDato? ObtenerPorId(int id);
        IEnumerable<ProgramaGeneralPerfilTipoDato> ObtenerPorIdPGeneral(int idPGeneral);
    }
}
