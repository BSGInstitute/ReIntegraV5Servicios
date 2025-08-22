using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionDePersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface INivelPuestoTrabajoService
    {
        IEnumerable<PuestoTrabajoNivelDTO> Obtener();
        PuestoTrabajoNivelDTO Insertar(PuestoTrabajoNivelDTO dto, string usuario);
        PuestoTrabajoNivelDTO Actualizar(PuestoTrabajoNivelDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
    