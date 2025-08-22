using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPespecificoCursoAdicionalRepository : IGenericRepository<TPespecificoCursoAdicional>
    {
        #region Metodos Base
        TPespecificoCursoAdicional Add(PespecificoCursoAdicional entidad);
        TPespecificoCursoAdicional Update(PespecificoCursoAdicional entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoCursoAdicional> Add(IEnumerable<PespecificoCursoAdicional> listadoEntidad);
        IEnumerable<TPespecificoCursoAdicional> Update(IEnumerable<PespecificoCursoAdicional> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        bool Exist(int id);
        #endregion
    }
}
