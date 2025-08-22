using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IGestionRemuneracionPuestoTrabajoRepository : IGenericRepository<TPuestoTrabajoRemuneracion>
    {
        #region Metodos Base
        TPuestoTrabajoRemuneracion Add(PuestoTrabajoRemuneracion entidad);
        TPuestoTrabajoRemuneracion Update(PuestoTrabajoRemuneracion entidad);
        bool Delete(int id, string usuario);

        //IEnumerable<TPuestoTrabajoRemuneracion> Add(IEnumerable<PuestoTrabajoRemuneracion> listadoEntidad);
        //IEnumerable<TPuestoTrabajoRemuneracion> Update(IEnumerable<PuestoTrabajoRemuneracion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<GestionRemuneracionPuestoTrabajoDTO> Obtener();
        PuestoTrabajoRemuneracion? ObtenerPorId(int id);
        List<ComboDTO> ObtenerRemuneracion();
        List<ComboDTO> ObtenerTipoRemuneracion();
        List<ComboDTO> ObtenerClaseRemuneracion();
        List<ComboDTO> ObtenerPeriodoRemuneracion();
        List<ComboDTO> ObtenerMonedaParaTableroComercial();
        List<ComboDTO> ObtenerDescripcionMonetaria();
        IEnumerable<PuestoTrabajoRemuneracionDetalle> ObtenerPorIdPuestoTrabajo(int idPuestoTrabajo);
        PuestoTrabajoRemuneracionDetalle ObtenerDetallePorId(int idDetalle);
        List<PuestoTrabajoRemuneracionDetalleDTO> ObtenerPuestoTrabajoRemuneracionVariableRegistrado(int IdPuestoTrabajoRemuneracion);
        List<PuestoTrabajoRemuneracionDetalle> ObtenerDetallePuestoTrabajoRemuneracionPorId(int IdPuestoTrabajoRemuneracion);
    }
}
