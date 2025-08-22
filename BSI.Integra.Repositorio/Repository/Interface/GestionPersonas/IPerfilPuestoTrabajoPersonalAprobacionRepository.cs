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
    public interface IPerfilPuestoTrabajoPersonalAprobacionRepository : IGenericRepository<TPerfilPuestoTrabajoPersonalAprobacion>
    {
        #region Metodos Base
        TPerfilPuestoTrabajoPersonalAprobacion Add(PerfilPuestoTrabajoPersonalAprobacion entidad);
        TPerfilPuestoTrabajoPersonalAprobacion Update(PerfilPuestoTrabajoPersonalAprobacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPerfilPuestoTrabajoPersonalAprobacion> Add(IEnumerable<PerfilPuestoTrabajoPersonalAprobacion> listadoEntidad);
        IEnumerable<TPerfilPuestoTrabajoPersonalAprobacion> Update(IEnumerable<PerfilPuestoTrabajoPersonalAprobacion> listadoEntidad);  
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        PerfilPuestoTrabajoPersonalAprobacion? ObtenerPorId(int id);
        PerfilPuestoTrabajoPersonalAprobacion? ObtenerPorIdPersonalAndIdPuestoTrabajo(int idPersonal, int idPuestoTrabajo);
        List<PerfilPuestoTrabajoPersonalAprobacionDatosDTO> ObtenerPersonalConfigurado();
        List<PerfilPuestoTrabajoPersonalAprobacion>? ObtenerbyIdPersonal(int idPersonal);
    }
}
