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
    public interface IPersonalCeseRepository : IGenericRepository<TPersonalCese>
    {
        #region Metodos Base
        TPersonalCese Add(PersonalCese entidad);
        TPersonalCese Update(PersonalCese entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalCese> Add(IEnumerable<PersonalCese> listadoEntidad);
        IEnumerable<TPersonalCese> Update(IEnumerable<PersonalCese> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PersonalCeseDTO ObtenerMotivoFechaUltimo(int idPersonal);
    }
}
