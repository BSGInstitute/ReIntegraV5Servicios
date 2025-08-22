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
    public  interface IPersonalRemuneracionRepository : IGenericRepository<TPersonalRemuneracion>
    {
        #region Metodos Base
        TPersonalRemuneracion Add(PersonalRemuneracion entidad);
        TPersonalRemuneracion Update(PersonalRemuneracion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalRemuneracion> Add(IEnumerable<PersonalRemuneracion> listadoEntidad);
        IEnumerable<TPersonalRemuneracion> Update(IEnumerable<PersonalRemuneracion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PersonalRemuneracionDTO> Obtener(int idPersonal);

    }
}
