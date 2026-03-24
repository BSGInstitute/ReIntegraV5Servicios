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
    public interface IPEspecificoSesionEstadoObservacionRepository : IGenericRepository<TPespecificoSesionEstadoObservacion>
    {
        #region Metodos Base
        TPespecificoSesionEstadoObservacion Add(PEspecificoSesionEstadoObservacion entidad);
        TPespecificoSesionEstadoObservacion Update(PEspecificoSesionEstadoObservacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoSesionEstadoObservacion> Add(IEnumerable<PEspecificoSesionEstadoObservacion> listadoEntidad);
        IEnumerable<TPespecificoSesionEstadoObservacion> Update(IEnumerable<PEspecificoSesionEstadoObservacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PEspecificoSesionEstadoObservacionQueryDTO> Obtener();
        PEspecificoSesionEstadoObservacion? ObtenerPorId(int id);
    }
}
