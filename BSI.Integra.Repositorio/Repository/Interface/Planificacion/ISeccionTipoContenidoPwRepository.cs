using BSI.Integra.Aplicacion.DTO;
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
    public interface ISeccionTipoContenidoPwRepository : IGenericRepository<TSeccionTipoContenidoPw>
    {
        #region Metodos Base
        TSeccionTipoContenidoPw Add(SeccionTipoContenidoPw entidad);
        TSeccionTipoContenidoPw Update(SeccionTipoContenidoPw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSeccionTipoContenidoPw> Add(IEnumerable<SeccionTipoContenidoPw> listadoEntidad);
        IEnumerable<TSeccionTipoContenidoPw> Update(IEnumerable<SeccionTipoContenidoPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
