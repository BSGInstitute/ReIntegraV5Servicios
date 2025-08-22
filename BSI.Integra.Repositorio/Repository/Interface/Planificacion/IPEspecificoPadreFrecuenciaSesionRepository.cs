using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPEspecificoPadreFrecuenciaSesionRepository : IGenericRepository<TPespecificoPadreFrecuenciaSesion>
    {
        #region Metodos Base
        TPespecificoPadreFrecuenciaSesion Add(PespecificoPadreFrecuenciaSesion entidad);
        TPespecificoPadreFrecuenciaSesion Update(PespecificoPadreFrecuenciaSesion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoPadreFrecuenciaSesion> Add(IEnumerable<PespecificoPadreFrecuenciaSesion> listadoEntidad);
        IEnumerable<TPespecificoPadreFrecuenciaSesion> Update(IEnumerable<PespecificoPadreFrecuenciaSesion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PespecificoPadreFrecuenciaSesion? ObtenerPorId(int id);
        IEnumerable<PEspecificoPadreFrecuenciaSesionDTO> ObtenerTodoPorPEspecificoPadreFrecuencia(int id);
    }
}
