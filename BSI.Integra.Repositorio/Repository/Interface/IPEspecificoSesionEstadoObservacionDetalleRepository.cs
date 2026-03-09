using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPEspecificoSesionEstadoObservacionDetalleRepository : IGenericRepository<TPEspecificoSesionEstadoObservacionDetalle>
    {
        #region Metodos Base
        TPEspecificoSesionEstadoObservacionDetalle Add(PEspecificoSesionEstadoObservacionDetalle entidad);
        TPEspecificoSesionEstadoObservacionDetalle Update(PEspecificoSesionEstadoObservacionDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPEspecificoSesionEstadoObservacionDetalle> Add(IEnumerable<PEspecificoSesionEstadoObservacionDetalle> listadoEntidad);
        IEnumerable<TPEspecificoSesionEstadoObservacionDetalle> Update(IEnumerable<PEspecificoSesionEstadoObservacionDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PEspecificoSesionEstadoObservacionDetalleDTO> Obtener();
        PEspecificoSesionEstadoObservacionDetalle? ObtenerPorId(int id);
    }
}
