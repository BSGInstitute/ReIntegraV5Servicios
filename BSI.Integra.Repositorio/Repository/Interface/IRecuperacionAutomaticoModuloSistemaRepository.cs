using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRecuperacionAutomaticoModuloSistemaRepository
    {
        #region Metodos Base
        public TRecuperacionAutomaticoModuloSistema Add(RecuperacionAutomaticoModuloSistemaDTO entidad);
        public TRecuperacionAutomaticoModuloSistema Update(RecuperacionAutomaticoModuloSistemaDTO entidad);
        public bool Delete(int id, string usuario);
        public IEnumerable<TRecuperacionAutomaticoModuloSistema> Add(IEnumerable<RecuperacionAutomaticoModuloSistemaDTO> listadoEntidad);

        public IEnumerable<TRecuperacionAutomaticoModuloSistema> Update(IEnumerable<RecuperacionAutomaticoModuloSistemaDTO> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TRecuperacionAutomaticoModuloSistema> GetBy(Expression<Func<TRecuperacionAutomaticoModuloSistema, bool>> filter);
        TRecuperacionAutomaticoModuloSistema FirstBy(Expression<Func<TRecuperacionAutomaticoModuloSistema, bool>> filter);
    }
}
