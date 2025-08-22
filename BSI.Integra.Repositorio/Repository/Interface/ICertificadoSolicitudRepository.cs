using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICertificadoSolicitudRepository : IGenericRepository<TCertificadoSolicitud>
    {
        #region Metodos Base
        TCertificadoSolicitud Add(CertificadoSolicitud entidad);
        TCertificadoSolicitud Update(CertificadoSolicitud entidad);
        bool Delete(int id, string usuario); 
        IEnumerable<TCertificadoSolicitud> Add(IEnumerable<CertificadoSolicitud> listadoEntidad);
        IEnumerable<TCertificadoSolicitud> Update(IEnumerable<CertificadoSolicitud> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
