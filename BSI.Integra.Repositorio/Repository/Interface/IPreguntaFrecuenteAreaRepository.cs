using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPreguntaFrecuenteAreaRepository : IGenericRepository<TPreguntaFrecuenteArea>
    {
        #region Metodos Base
        TPreguntaFrecuenteArea Add(PreguntaFrecuenteArea entidad);
        TPreguntaFrecuenteArea Update(PreguntaFrecuenteArea entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPreguntaFrecuenteArea> Add(IEnumerable<PreguntaFrecuenteArea> listadoEntidad);
        IEnumerable<TPreguntaFrecuenteArea> Update(IEnumerable<PreguntaFrecuenteArea> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion  
        IEnumerable<PreguntaFrecuenteAreaDTO> Obtener();
        IEnumerable<PreguntaFrecuenteArea> ObtenerPorIdPreguntaFrecuente(int idPreguntaFrecuente);
        PreguntaFrecuenteArea ObtenerPorIdPreguntaFrecuenteYIdArea(int idPreguntaFrecuente, int idArea);
    }
}
