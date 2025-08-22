using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRemitenteMailingAsesorRepository : IGenericRepository<TRemitenteMailingAsesor>
    {
        #region Metodos Base
        TRemitenteMailingAsesor Add(RemitenteMailingAsesor entidad);
        TRemitenteMailingAsesor Update(RemitenteMailingAsesor entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TRemitenteMailingAsesor> Add(IEnumerable<RemitenteMailingAsesor> listadoEntidad);
        IEnumerable<TRemitenteMailingAsesor> Update(IEnumerable<RemitenteMailingAsesor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
