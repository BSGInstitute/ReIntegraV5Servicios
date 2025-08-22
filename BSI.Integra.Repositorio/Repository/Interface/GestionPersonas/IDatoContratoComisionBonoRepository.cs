using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IDatoContratoComisionBonoRepository : IGenericRepository<TDatoContratoComisionBono>
    {
        #region Metodos Base
        TDatoContratoComisionBono Add(DatoContratoComisionBono entidad);
        TDatoContratoComisionBono Update(DatoContratoComisionBono entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDatoContratoComisionBono> Add(IEnumerable<DatoContratoComisionBono> listadoEntidad);
        IEnumerable<TDatoContratoComisionBono> Update(IEnumerable<DatoContratoComisionBono> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
