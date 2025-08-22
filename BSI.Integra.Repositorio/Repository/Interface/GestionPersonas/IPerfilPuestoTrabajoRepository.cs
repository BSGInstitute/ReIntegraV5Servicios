using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPerfilPuestoTrabajoRepository : IGenericRepository<TPerfilPuestoTrabajo>
    {
        #region Metodos Base
        TPerfilPuestoTrabajo Add(PerfilPuestoTrabajo entidad);

        TPerfilPuestoTrabajo Update(PerfilPuestoTrabajo entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPerfilPuestoTrabajo> Add(IEnumerable<PerfilPuestoTrabajo> listadoEntidad);
        IEnumerable<TPerfilPuestoTrabajo> Update(IEnumerable<PerfilPuestoTrabajo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PuestoTrabajoRelacionDetalleDTO> ObtenerPuestoTrabajoRelacion(int? idPerfilPuestoTrabajo);
        List<PuestoTrabajoVersionesDTO> ObtenerListaPerfilPuestoTrabajoHistorico(int? idPuestoTrabajo);
        PerfilPuestoTrabajo ObtenerIdPuestoTrabajoANDIdPerfilPuestoTrabajoEstadoSolicitud(int idPuestoTrabajo, int idPerfilPuestoTrabajoEstadoSolicitud);
        List<PersonalAprobacionDTO> ObtenerPersonalAprobacionVersion(int idPersonal);
    }
}
