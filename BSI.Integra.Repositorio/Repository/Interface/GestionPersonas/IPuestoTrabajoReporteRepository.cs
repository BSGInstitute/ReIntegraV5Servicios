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
    public interface IPuestoTrabajoReporteRepository : IGenericRepository<TPuestoTrabajoReporte>
    {
        #region Metodos Base
        TPuestoTrabajoReporte Add(PuestoTrabajoReporte entidad);

        TPuestoTrabajoReporte Update(PuestoTrabajoReporte entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPuestoTrabajoReporte> Add(IEnumerable<PuestoTrabajoReporte> listadoEntidad);
        IEnumerable<TPuestoTrabajoReporte> Update(IEnumerable<PuestoTrabajoReporte> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PuestoTrabajoReporteDTO> ObtenerPuestoTrabajoReporte(int? idPerfilPuestoTrabajo);
    }
}
