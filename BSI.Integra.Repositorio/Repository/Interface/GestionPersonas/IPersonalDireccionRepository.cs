using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPersonalDireccionRepository : IGenericRepository<TPersonalDireccion>
    {
        #region Metodos Base
        TPersonalDireccion Add(PersonalDireccion entidad);
        TPersonalDireccion Update(PersonalDireccion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalDireccion> Add(IEnumerable<PersonalDireccion> listadoEntidad);
        IEnumerable<TPersonalDireccion> Update(IEnumerable<PersonalDireccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
