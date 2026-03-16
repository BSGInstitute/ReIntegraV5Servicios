using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPEspecificoSesionEstadoService
    {
        IEnumerable<PEspecificoSesionEstadoDTO> Obtener();
        PEspecificoSesionEstadoDTO Insertar(PEspecificoSesionEstadoDTO dto, string usuario);
        PEspecificoSesionEstadoDTO Actualizar(PEspecificoSesionEstadoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        bool ActualizarEstadoCurso(EstadoCursoDTO dto, string usuario);
        bool ActualizarEstadoObservacion(EstadoCursoObservacionDTO dto, string usuario);
    }
}
