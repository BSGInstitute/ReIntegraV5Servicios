using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICompromisoAlumnoRepository : IGenericRepository<TCompromisoAlumno>
    {
        #region Metodos Base
        TCompromisoAlumno Add(CompromisoAlumno entidad);
        TCompromisoAlumno Update(CompromisoAlumno entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCompromisoAlumno> Add(IEnumerable<CompromisoAlumno> listadoEntidad);
        IEnumerable<TCompromisoAlumno> Update(IEnumerable<CompromisoAlumno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
