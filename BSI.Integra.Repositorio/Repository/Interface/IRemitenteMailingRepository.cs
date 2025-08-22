using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRemitenteMailingRepository : IGenericRepository<TRemitenteMailing>
    {
        #region Metodos Base
        TRemitenteMailing Add(RemitenteMailing entidad);
        TRemitenteMailing Update(RemitenteMailing entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TRemitenteMailing> Add(IEnumerable<RemitenteMailing> listadoEntidad);
        IEnumerable<TRemitenteMailing> Update(IEnumerable<RemitenteMailing> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<RemitenteMailingDTO> ObtenerTodosRemitenteMailing();
        List<RemitenteMailingAsesorDTO> ObtenerListaRemitenteMailingAsesor(int IdRemitenteMailing);
    }
}
