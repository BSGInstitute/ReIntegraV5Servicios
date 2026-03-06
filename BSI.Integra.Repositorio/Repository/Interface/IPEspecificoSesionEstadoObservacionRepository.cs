using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPEspecificoSesionEstadoObservacionRepository : IGenericRepository<TPEspecificoSesionEstadoObservacion>
    {
        #region Metodos Base
        TPEspecificoSesionEstadoObservacion Add(PEspecificoSesionEstadoObservacion entidad);
        TPEspecificoSesionEstadoObservacion Update(PEspecificoSesionEstadoObservacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPEspecificoSesionEstadoObservacion> Add(IEnumerable<PEspecificoSesionEstadoObservacion> listadoEntidad);
        IEnumerable<TPEspecificoSesionEstadoObservacion> Update(IEnumerable<PEspecificoSesionEstadoObservacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PEspecificoSesionEstadoObservacionDTO> Obtener();
        PEspecificoSesionEstadoObservacion? ObtenerPorId(int id);
    }
}
