using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface IFraseReconocidaRepository : IGenericRepository<TFraseReconocidum>
    {
        #region Metodos Base
        TFraseReconocidum Add(FraseReconocida entidad);
        TFraseReconocidum Update(FraseReconocida entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFraseReconocidum> Add(IEnumerable<FraseReconocida> listadoEntidad);
        IEnumerable<TFraseReconocidum> Update(IEnumerable<FraseReconocida> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


    }
}
