using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralArgumentoDetalleRepository
    {
        #region Metodos Base
        TProgramaGeneralArgumentoDetalle Add(ProgramaGeneralArgumentoDetalle entidad);
        TProgramaGeneralArgumentoDetalle Update(ProgramaGeneralArgumentoDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralArgumentoDetalle> AddList(IEnumerable<ProgramaGeneralArgumentoDetalle> listadoEntidad);
        IEnumerable<TProgramaGeneralArgumentoDetalle> UpdateList(IEnumerable<ProgramaGeneralArgumentoDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
