using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPespecificoCronogramaRepository : IGenericRepository<TPespecificoCronograma>
    {
        #region Metodos Base
        TPespecificoCronograma Add(PespecificoCronograma entidad);
        TPespecificoCronograma Update(PespecificoCronograma entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPespecificoCronograma> Add(IEnumerable<PespecificoCronograma> listadoEntidad);
        IEnumerable<TPespecificoCronograma> Update(IEnumerable<PespecificoCronograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PespecificoCronograma? ObtenerPorIdPespecificoPorIdPais(int idPespecifico, int idPais);
        IEnumerable<PEspecificoCronogramaGrupalDTO> ObtenerPEspecificoCronogramaGrupalPorIdPEspecifico(int idPespecifico);
    }
}
