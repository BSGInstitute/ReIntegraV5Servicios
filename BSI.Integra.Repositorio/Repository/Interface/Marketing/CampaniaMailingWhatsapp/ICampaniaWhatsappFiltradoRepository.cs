using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp.CampaniaMailingWhatsAppFiltradoDTO;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp
{
    public interface ICampaniaWhatsappFiltradoRepository 
    {
        Task<bool> FiltradoDeDatosParaWhatsapp(CampaniaWhatsAppFiltrado datosFiltro);
        List<CampaniaWhatsAppFiltroWhatsApp> FiltradoDeDatosParaWhatsappObtenerData(int IdcampaniaGeneral, int Prioridad);
        List<TFiltradoDeDatosPorPrioridadWhatsApp> FiltradoDeDatosParaWhatsappObtenerAllData(int IdcampaniaGeneral);
        int ObtenerCantidadDeDataPorPioridadYcampania(int prioridad, int IdcampaniaGeneralDetalle);
        bool EliminarFiltradoPasadoWhatsApp(int IdcampaniaGeneral, string usuario);
    }
}
