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
    public interface IPersonalSeguroSaludRepository : IGenericRepository<TPersonalSeguroSalud>
    {
        #region Metodos Base
        TPersonalSeguroSalud Add(PersonalSeguroSalud entidad);
        TPersonalSeguroSalud Update(PersonalSeguroSalud entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalSeguroSalud> Add(IEnumerable<PersonalSeguroSalud> listadoEntidad);
        IEnumerable<TPersonalSeguroSalud> Update(IEnumerable<PersonalSeguroSalud> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<PersonalSeguroSaludDTO> ObtenerPersonalSeguroSalud(int idPersonal);
    }
}
