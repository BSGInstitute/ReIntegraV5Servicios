using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPersonalPuestoSedeHistoricoRepository : IGenericRepository<TPersonalPuestoSedeHistorico>
    {
        #region Metodos Base
        TPersonalPuestoSedeHistorico Add(PersonalPuestoSedeHistorico entidad);

        TPersonalPuestoSedeHistorico Update(PersonalPuestoSedeHistorico entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPersonalPuestoSedeHistorico> Add(IEnumerable<PersonalPuestoSedeHistorico> listadoEntidad);
        IEnumerable<TPersonalPuestoSedeHistorico> Update(IEnumerable<PersonalPuestoSedeHistorico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
