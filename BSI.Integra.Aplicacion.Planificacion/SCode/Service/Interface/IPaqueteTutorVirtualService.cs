using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPaqueteTutorVirtualService
    {
        
        IEnumerable<PaqueteTutorVirtualDTO> Obtener();
        PaqueteTutorVirtualDTO Insertar(PaqueteTutorVirtualDTO dto, string usuario);
        PaqueteTutorVirtualDTO Actualizar(PaqueteTutorVirtualDTO dto, string usuario);
        bool Eliminar(int id, string usuario);

    }
}
