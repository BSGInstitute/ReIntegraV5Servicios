using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface IDetalleFraseReconocidaRepository : IGenericRepository<TDetalleFraseReconocidum>
    {
        #region Metodos Base
        TDetalleFraseReconocidum Add(DetalleFraseReconocida entidad);
        TDetalleFraseReconocidum Update(DetalleFraseReconocida entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDetalleFraseReconocidum> Add(IEnumerable<DetalleFraseReconocida> listadoEntidad);
        IEnumerable<TDetalleFraseReconocidum> Update(IEnumerable<DetalleFraseReconocida> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

    }
}
