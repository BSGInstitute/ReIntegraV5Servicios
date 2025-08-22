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
    public interface ICampaniaGeneralDetalleProgramaService
    {
        #region Metodos Base
        CampaniaGeneralDetallePrograma Add(CampaniaGeneralDetallePrograma entidad,string usuario);
        CampaniaGeneralDetallePrograma Update(CampaniaGeneralDetallePrograma entidad);
        bool Delete(int id, string usuario);

        List<CampaniaGeneralDetallePrograma> Add(List<CampaniaGeneralDetallePrograma> listadoEntidad, string usuario);
        List<CampaniaGeneralDetallePrograma> Update(List<CampaniaGeneralDetallePrograma> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
