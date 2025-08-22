using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp.CampaniaMailingWhatsAppFiltradoDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.CampaniasMailingWhatsapp
{
    public interface ICampaniaGeneralDetalleSubAreaService
    {
        #region Metodos Base
        CampaniaGeneralDetalleSubArea Add(CampaniaGeneralDetalleSubArea entidad,string usuario);
        CampaniaGeneralDetalleSubArea Update(CampaniaGeneralDetalleSubArea entidad, string usuario);
        bool Delete(int id, string usuario);

        List<CampaniaGeneralDetalleSubArea> Add(List<CampaniaGeneralDetalleSubArea> listadoEntidad,string usuario);
        List<CampaniaGeneralDetalleSubArea> Update(List<CampaniaGeneralDetalleSubArea> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
