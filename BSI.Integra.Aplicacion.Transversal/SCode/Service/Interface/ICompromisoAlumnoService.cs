using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICompromisoAlumnoService
    {
        #region Metodos Base
        CompromisoAlumno Add(CompromisoAlumno entidad);
        CompromisoAlumno Update(CompromisoAlumno entidad);
        bool Delete(int id, string usuario);

        List<CompromisoAlumno> Add(List<CompromisoAlumno> listadoEntidad);
        List<CompromisoAlumno> Update(List<CompromisoAlumno> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
