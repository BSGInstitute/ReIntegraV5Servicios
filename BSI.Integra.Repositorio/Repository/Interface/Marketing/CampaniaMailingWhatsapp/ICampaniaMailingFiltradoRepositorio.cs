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
    public interface ICampaniaMailingFiltradoRepositorio
    {
        bool FiltradoDeDatosParaMailing(CampaniaMailingFiltrado datosFiltro);
        List<TFiltradoDeDatosPorPrioridadMailing> FiltradoDeDatosParaMailingObtenerData(int IdcampaniaGeneral, int Prioridad);
        List<CampaniaMailingSendingBlueFiltroMailing> FiltradoDeDatosParaMailingObtenerDataMailing(int IdcampaniaGeneral, int Prioridad);
        bool EliminarFiltradoPasado(int IdcampaniaGeneral, string usuario);
        List<TFiltradoDeDatosPorPrioridadMailing> FiltradoDeDatosParaMailingObtenerData(int IdcampaniaGeneral);

    }
}
