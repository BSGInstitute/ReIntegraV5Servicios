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
    public interface IPersonalInformacionMedicaRepository : IGenericRepository<TPersonalInformacionMedica>
    {
        #region Metodos Base
        TPersonalInformacionMedica Add(PersonalInformacionMedica entidad);
        TPersonalInformacionMedica Update(PersonalInformacionMedica entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalInformacionMedica> Add(IEnumerable<PersonalInformacionMedica> listadoEntidad);
        IEnumerable<TPersonalInformacionMedica> Update(IEnumerable<PersonalInformacionMedica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PersonalInformacionMedicaDTO> ObtenerPersonalInformacionMedica(int idPersonal);
        PersonalInformacionMedica? ObtenerPorId(int Id);
    }
}
