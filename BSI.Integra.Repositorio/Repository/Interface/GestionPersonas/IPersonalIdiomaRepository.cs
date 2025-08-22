using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPersonalIdiomaRepository : IGenericRepository<TPersonalIdioma>
    {
        #region Metodos Base
        TPersonalIdioma Add(PersonalIdioma entidad);
        TPersonalIdioma Update(PersonalIdioma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalIdioma> Add(IEnumerable<PersonalIdioma> listadoEntidad);
        IEnumerable<TPersonalIdioma> Update(IEnumerable<PersonalIdioma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PersonalIdiomaDTO> ObtenerPersonalIdioma(int idPersonal);
        PersonalIdioma? ObtenerPorId(int Id);
    }
}
