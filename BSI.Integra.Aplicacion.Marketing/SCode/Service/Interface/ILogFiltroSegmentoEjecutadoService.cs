using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface ILogFiltroSegmentoEjecutadoService
    {

        #region Metodos Base
        public LogFiltroSegmentoEjecutado Add(LogFiltroSegmentoEjecuDTO entidad, string Usuario);



        #endregion
        List<LogFiltroSegmentoEjecutadoDTO> ObtenerPorIdFiltroSegmento(int idFiltroSegmento);

    }
}
