using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
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
    public interface ICampaniaMailingFiltradoService
    {
        RespuestaGenerica FiltradoDeDatosParaMailing(CampaniaMailingFiltrado datosFiltro);
        RespuestaGenerica FiltradoDeDatosParaMailingObtenerData(int IdcampaniaGeneral, int Prioridad);
        bool SendinMail(string usuario, int IdcampaniaGeneral);
        List<TCampaniaGeneralDetalle> ObtenerDetalleCampaniaGeneralPorIdDeCampaniaGeneral(int IdCampaniaGeneral);
        List<CampaniaGeneralDetalleAreaSubAreaProgramaReturn> ObtenerCampaniaGeneralMasAreaSubAreaYprogramaById(int idCampaniaGeneralDetalle, int idCamapniaGeneral);
    }
}
