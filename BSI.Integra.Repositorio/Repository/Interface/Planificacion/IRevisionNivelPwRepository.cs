using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IRevisionNivelPwRepository : IGenericRepository<TRevisionNivelPw>
    {
        #region Metodos Base
        TRevisionNivelPw Add(RevisionNivelPw entidad);
        TRevisionNivelPw Update(RevisionNivelPw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TRevisionNivelPw> Add(IEnumerable<RevisionNivelPw> listadoEntidad);
        IEnumerable<TRevisionNivelPw> Update(IEnumerable<RevisionNivelPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RevisionNivelPwDTO> ObtenerPorIdRevisionPw(int idRevisionPw);
    }
}
