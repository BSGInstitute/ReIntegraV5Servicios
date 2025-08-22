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
    public interface IPersonalFormacionRepository : IGenericRepository<TPersonalFormacion>
    {
        #region Metodos Base
        TPersonalFormacion Add(PersonalFormacion entidad);
        TPersonalFormacion Update(PersonalFormacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalFormacion> Add(IEnumerable<PersonalFormacion> listadoEntidad);
        IEnumerable<TPersonalFormacion> Update(IEnumerable<PersonalFormacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PersonalFormacionDTO> Obtener(int idPersonal);
        PersonalFormacion? ObtenerPorId(int Id);

    }
}
