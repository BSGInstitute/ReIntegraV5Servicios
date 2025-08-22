using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IRemitenteMailingAsesor
    {
        #region Metodos Base
        RemitenteMailingAsesor Add(RemitenteMailingAsesor entidad);
        RemitenteMailingAsesor Update(RemitenteMailingAsesor entidad);
        bool Delete(int id, string usuario);

        List<RemitenteMailingAsesor> Add(List<RemitenteMailingAsesor> listadoEntidad);
        List<RemitenteMailingAsesor> Update(List<RemitenteMailingAsesor> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
