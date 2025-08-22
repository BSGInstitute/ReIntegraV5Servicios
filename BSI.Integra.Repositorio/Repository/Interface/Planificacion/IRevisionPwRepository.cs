
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
    public interface IRevisionPwRepository : IGenericRepository<TRevisionPw>
    {
        #region Metodos Base
        TRevisionPw Add(RevisionPw entidad);
        TRevisionPw Update(RevisionPw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TRevisionPw> Add(IEnumerable<RevisionPw> listadoEntidad);
        IEnumerable<TRevisionPw> Update(IEnumerable<RevisionPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RevisionPwDTO> ObtenerCombo();
    }
}
