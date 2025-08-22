using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionDePersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPuestoTrabajoNivelRepository : IGenericRepository<TPuestoTrabajoNivel>
    {
        #region Metodos Base
        TPuestoTrabajoNivel Add(PuestoTrabajoNivel entidad);
        TPuestoTrabajoNivel Update(PuestoTrabajoNivel entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPuestoTrabajoNivel> Add(IEnumerable<PuestoTrabajoNivel> listadoEntidad);
        IEnumerable<TPuestoTrabajoNivel> Update(IEnumerable<PuestoTrabajoNivel> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PuestoTrabajoNivelDTO> Obtener();
        PuestoTrabajoNivel? ObtenerPorId(int idPuestoNivelTrabajo);
        IEnumerable<ComboDTO> ObtenerListaParaFiltro();
    }
}
