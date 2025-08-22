using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPreguntaFrecuenteTipoRepository : IGenericRepository<TPreguntaFrecuenteTipo>
    {
        #region Metodos Base
        TPreguntaFrecuenteTipo Add(PreguntaFrecuenteTipo entidad);
        TPreguntaFrecuenteTipo Update(PreguntaFrecuenteTipo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPreguntaFrecuenteTipo> Add(IEnumerable<PreguntaFrecuenteTipo> listadoEntidad);
        IEnumerable<TPreguntaFrecuenteTipo> Update(IEnumerable<PreguntaFrecuenteTipo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion  
        IEnumerable<PreguntaFrecuenteTipoDTO> Obtener();
        IEnumerable<PreguntaFrecuenteTipo> ObtenerPorIdPreguntaFrecuente(int idPreguntaFrecuente);
        PreguntaFrecuenteTipo ObtenerPorIdPreguntaFrecuenteYIdTipo(int idPreguntaFrecuente, int idTipo);
    }
}
