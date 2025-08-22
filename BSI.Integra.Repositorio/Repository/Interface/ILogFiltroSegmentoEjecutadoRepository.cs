using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ILogFiltroSegmentoEjecutadoRepository : IGenericRepository<TLogFiltroSegmentoEjecutado>
    {

        #region Metodos Base
        public TLogFiltroSegmentoEjecutado Add(LogFiltroSegmentoEjecutado entidad);

        #endregion
        
        List<LogFiltroSegmentoEjecutadoDTO> ObtenerPorIdFiltroSegmento(int idFiltroSegmento);
    }
}
