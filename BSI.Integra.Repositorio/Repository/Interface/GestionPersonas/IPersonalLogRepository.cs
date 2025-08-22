using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPersonalLogRepository : IGenericRepository<TPersonalLog>
    {
        #region Metodos Base
        TPersonalLog Add(PersonalLog entidad);
        TPersonalLog Update(PersonalLog entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPersonalLog> Add(IEnumerable<PersonalLog> listadoEntidad);
        IEnumerable<TPersonalLog> Update(IEnumerable<PersonalLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PersonalJefeInmediatoDTO> ObtenerJefeInmediatoHistorico(int idPersonal);
        List<PersonalTipoAsesorDTO> ObtenerTipoAsesorHistorico(int idPersonal);
        public List <PersonalLog> ObtenerPorIdPersonal(int idPersonal);

    }
}
 