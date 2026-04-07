using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaGeneralArgumentoDetalleMotivacionRepository
    {
        #region Metodos Base
        TProgramaGeneralArgumentoDetalleMotivacion Add(ProgramaGeneralArgumentoDetalleMotivacion entidad);
        TProgramaGeneralArgumentoDetalleMotivacion Update(ProgramaGeneralArgumentoDetalleMotivacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralArgumentoDetalleMotivacion> AddList(IEnumerable<ProgramaGeneralArgumentoDetalleMotivacion> listadoEntidad);
        IEnumerable<TProgramaGeneralArgumentoDetalleMotivacion> Update(IEnumerable<ProgramaGeneralArgumentoDetalleMotivacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


        bool ActualizarProgramaGeneralArgumentoDetalleMotivacion(int id, string usuario ,int idMotivacion);
    }
}
