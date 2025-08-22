using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralPerfilInterceptoRepository : IGenericRepository<TProgramaGeneralPerfilIntercepto>
    {
        #region Metodos Base
        TProgramaGeneralPerfilIntercepto Add(ProgramaGeneralPerfilIntercepto entidad);
        TProgramaGeneralPerfilIntercepto Update(ProgramaGeneralPerfilIntercepto entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralPerfilIntercepto> Add(IEnumerable<ProgramaGeneralPerfilIntercepto> listadoEntidad);
        IEnumerable<TProgramaGeneralPerfilIntercepto> Update(IEnumerable<ProgramaGeneralPerfilIntercepto> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ProgramaGeneralPerfilIntercepto? ObtenerPorId(int id);
        ProgramaGeneralPerfilIntercepto ObtenerPorIdPGeneral(int idPGeneral);
    }
}
