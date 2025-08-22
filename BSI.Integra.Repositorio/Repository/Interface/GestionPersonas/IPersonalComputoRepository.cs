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
    public interface IPersonalComputoRepository : IGenericRepository<TPersonalComputo>
    {
        #region Metodos Base
        TPersonalComputo Add(PersonalComputo entidad);
        TPersonalComputo Update(PersonalComputo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalComputo> Add(IEnumerable<PersonalComputo> listadoEntidad);
        IEnumerable<TPersonalComputo> Update(IEnumerable<PersonalComputo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PersonalInformaticaDTO> ObtenerPersonalComputo(int idPersonal);
        PersonalComputo ObtenerPorId(int Id);
    }
}
