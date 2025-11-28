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
        PaqueteTutorVirtualDTO Insertar(PaqueteTutorVirtualDTO dto, string usuario);
        PaqueteTutorVirtualDTO Actualizar(PaqueteTutorVirtualDTO dto, string usuario);
        bool Eliminar(int id, string usuario);

        // Métodos para manejo completo con países y beneficios
        PaqueteTutorVirtualGuardarDTO InsertarCompleto(PaqueteTutorVirtualGuardarDTO dto, string usuario);
        PaqueteTutorVirtualGuardarDTO ActualizarCompleto(PaqueteTutorVirtualGuardarDTO dto, string usuario);

    }
}
