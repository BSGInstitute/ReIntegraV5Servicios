using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPEspecificoSesionEstadoObservacionService
    {
        IEnumerable<PEspecificoSesionEstadoObservacionDTO> Obtener();
        PEspecificoSesionEstadoObservacionDTO Insertar(PEspecificoSesionEstadoObservacionDTO dto, string usuario);
        PEspecificoSesionEstadoObservacionDTO Actualizar(PEspecificoSesionEstadoObservacionDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
