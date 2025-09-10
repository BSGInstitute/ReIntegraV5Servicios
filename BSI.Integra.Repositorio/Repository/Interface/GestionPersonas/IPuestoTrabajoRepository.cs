using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPuestoTrabajoRepository : IGenericRepository<TPuestoTrabajo>
    {
        #region Metodos Base
        TPuestoTrabajo Add(PuestoTrabajo entidad);
 
        TPuestoTrabajo Update(PuestoTrabajo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajo> Add(IEnumerable<PuestoTrabajo> listadoEntidad);
        IEnumerable<TPuestoTrabajo> Update(IEnumerable<PuestoTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PuestoTrabajo? ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        List<PuestoTrabajoRemuneracionDTO> ObtenerPuestoTrabajoRemuneracionRegistrado();
        PuestoTrabajoRemuneracionDTO ObtenerComboRemuneracion(int IdPuestoTrabajo);
        List<PuestoTrabajoGestionContratoDTO> ObtenerPuestoTrabajoRemuneracionDet(int IdPuestoTrabajoRemuneracion);
        List<FuncionPuestoTrabajoDTO> ObtenerFuncionPuestoTrabajo();
        List<PuestoTrabajoPorFechaDTO> ObtenerPuestoTrabajoRegistradoFechaModificacion();
        

    }
}
