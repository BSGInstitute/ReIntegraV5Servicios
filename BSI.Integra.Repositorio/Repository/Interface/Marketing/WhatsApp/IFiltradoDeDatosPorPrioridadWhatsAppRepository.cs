using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.WhatsApp
{
    public interface IFiltradoDeDatosPorPrioridadWhatsAppRepository
    {
        List<TFiltradoDeDatosPorPrioridadWhatsApp> ObtenerFiltradoPorCampaniaGeneralAndPrioridad(int campaniaGeneral, int prioridad);
        List<TFiltradoDeDatosPorPrioridadWhatsApp> ObtenerFiltradoPorCampaniaGeneral(int campaniaGeneral);
    }
}
