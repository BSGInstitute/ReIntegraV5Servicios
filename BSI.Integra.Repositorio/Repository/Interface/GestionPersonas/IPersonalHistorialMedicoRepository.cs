using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
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
    public interface IPersonalHistorialMedicoRepository : IGenericRepository<TPersonalHistorialMedico>
    {
        #region Metodos Base
        TPersonalHistorialMedico Add(PersonalHistorialMedico entidad);
        TPersonalHistorialMedico Update(PersonalHistorialMedico entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalHistorialMedico> Add(IEnumerable<PersonalHistorialMedico> listadoEntidad);
        IEnumerable<TPersonalHistorialMedico> Update(IEnumerable<PersonalHistorialMedico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<PersonalHistorialMedicoDTO> ObtenerPersonalHistorialMedico(int idPersonal);
        PersonalHistorialMedico? ObtenerPorId(int Id);

    }
}
