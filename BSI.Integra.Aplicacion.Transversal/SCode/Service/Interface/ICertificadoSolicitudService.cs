using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICertificadoSolicitudService
    {
        #region Metodos Base
        CertificadoSolicitud Add(CertificadoSolicitud entidad);
        CertificadoSolicitud Update(CertificadoSolicitud entidad);
        bool Delete(int id, string usuario);

        List<CertificadoSolicitud> Add(List<CertificadoSolicitud> listadoEntidad);
        List<CertificadoSolicitud> Update(List<CertificadoSolicitud> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
