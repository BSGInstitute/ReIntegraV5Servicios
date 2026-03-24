using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPEspecificoSesionEstadoRepository : IGenericRepository<TPespecificoSesionEstado>
    {
        #region Metodos Base
        TPespecificoSesionEstado Add(PEspecificoSesionEstado entidad);
        TPespecificoSesionEstado Update(PEspecificoSesionEstado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoSesionEstado> Add(IEnumerable<PEspecificoSesionEstado> listadoEntidad);
        IEnumerable<TPespecificoSesionEstado> Update(IEnumerable<PEspecificoSesionEstado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PEspecificoSesionEstadoDTO> Obtener();
        PEspecificoSesionEstado? ObtenerPorId(int id);
        void ActualizarEstadoCurso(EstadoCursoDTO dto, string usuario);
        void ActualizarEstadoObservacion(EstadoCursoObservacionDTO dto, string usuario);
    }
}
