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
        IEnumerable<PaqueteTutorVirtualDetalleDTO> ObtenerDetalle();
        PaqueteTutorVirtualGuardarDTO Insertar(PaqueteTutorVirtualGuardarDTO dto, string usuario);
        PaqueteTutorVirtualGuardarDTO Actualizar(PaqueteTutorVirtualGuardarDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
