using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISeccionPreguntaFrecuenteRepository : IGenericRepository<TSeccionPreguntaFrecuente>
    {
        #region Metodos Base
        TSeccionPreguntaFrecuente Add(SeccionPreguntaFrecuente entidad);
        TSeccionPreguntaFrecuente Update(SeccionPreguntaFrecuente entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSeccionPreguntaFrecuente> Add(IEnumerable<SeccionPreguntaFrecuente> listadoEntidad);
        IEnumerable<TSeccionPreguntaFrecuente> Update(IEnumerable<SeccionPreguntaFrecuente> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
