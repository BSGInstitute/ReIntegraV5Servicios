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
    public interface IPersonalExperienciaRepository : IGenericRepository<TPersonalExperiencium>
    {
        #region Metodos Base
        TPersonalExperiencium Add(PersonalExperiencia entidad);
        TPersonalExperiencium Update(PersonalExperiencia entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalExperiencium> Add(IEnumerable<PersonalExperiencia> listadoEntidad);
        IEnumerable<TPersonalExperiencium> Update(IEnumerable<PersonalExperiencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<PersonalExperienciaDTO> ObtenerPersonalExperiencia(int idPersonal);
        PersonalExperiencia? ObtenerPorId(int Id);
    }
}
