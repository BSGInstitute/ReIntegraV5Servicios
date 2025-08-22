using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ISolicitudInterna
    {
        #region Metodos Base
        SolicitudInterna Add(SolicitudInterna entidad);
        SolicitudInterna Update(SolicitudInterna entidad);
        bool Delete(int id, string usuario);
        List<SolicitudInterna> Add(List<SolicitudInterna> listadoEntidad);
        List<SolicitudInterna> Update(List<SolicitudInterna> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
