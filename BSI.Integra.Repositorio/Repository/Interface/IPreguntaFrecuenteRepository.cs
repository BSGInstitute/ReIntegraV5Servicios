using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPreguntaFrecuenteRepository : IGenericRepository<TPreguntaFrecuente>
    {
        #region Metodos Base
        TPreguntaFrecuente Add(PreguntaFrecuente entidad);
        TPreguntaFrecuente Update(PreguntaFrecuente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPreguntaFrecuente> Add(IEnumerable<PreguntaFrecuente> listadoEntidad);
        IEnumerable<TPreguntaFrecuente> Update(IEnumerable<PreguntaFrecuente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion  
        IEnumerable<PreguntaFrecuenteDTO> Obtener();
        IEnumerable<PreguntaFrecuenteFiltroResultadoDTO> ObtenerPorFiltro(FiltroPreguntaFrecuenteDTO filtro);
        PreguntaFrecuente ObtenerPorId(int id);
    }
}
