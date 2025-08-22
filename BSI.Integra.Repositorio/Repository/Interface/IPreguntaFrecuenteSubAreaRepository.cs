using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPreguntaFrecuenteSubAreaRepository : IGenericRepository<TPreguntaFrecuenteSubArea>
    {
        #region Metodos Base
        TPreguntaFrecuenteSubArea Add(PreguntaFrecuenteSubArea entidad);
        TPreguntaFrecuenteSubArea Update(PreguntaFrecuenteSubArea entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPreguntaFrecuenteSubArea> Add(IEnumerable<PreguntaFrecuenteSubArea> listadoEntidad);
        IEnumerable<TPreguntaFrecuenteSubArea> Update(IEnumerable<PreguntaFrecuenteSubArea> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PreguntaFrecuenteSubAreaDTO> Obtener();
        IEnumerable<PreguntaFrecuenteSubArea> ObtenerPorIdPreguntaFrecuente(int idPreguntaFrecuente);
        PreguntaFrecuenteSubArea ObtenerPorIdPreguntaFrecuenteYIdSubArea(int idPreguntaFrecuente, int idSubArea);
    }
}
