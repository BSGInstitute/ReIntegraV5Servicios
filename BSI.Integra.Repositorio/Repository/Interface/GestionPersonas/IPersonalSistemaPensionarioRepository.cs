using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPersonalSistemaPensionarioRepository : IGenericRepository<TPersonalSistemaPensionario>
    {
        #region Metodos Base
        TPersonalSistemaPensionario Add(PersonalSistemaPensionario entidad);
        TPersonalSistemaPensionario Update(PersonalSistemaPensionario entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalSistemaPensionario> Add(IEnumerable<PersonalSistemaPensionario> listadoEntidad);
        IEnumerable<TPersonalSistemaPensionario> Update(IEnumerable<PersonalSistemaPensionario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<PersonalSistemaPensionarioDTO> ObtenerPersonalSistemaPensionario(int idPersonal);
    }
}
