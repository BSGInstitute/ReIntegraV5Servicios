using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IGestionRemuneracionPuestoTrabajoService
    {
        IEnumerable<GestionRemuneracionPuestoTrabajoDTO> Obtener();
        ObtenerDataComboPuestoTrabajoDTO ObtenerCombosModulo();
        List<PuestoTrabajoRemuneracionDetalleDTO> ObtenerPuestoTrabajoRemuneracionVariableRegistrado(int IdPuestoTrabajoRemuneracion);
        GestionRemuneracionPuestoTrabajoDTO Insertar(GestionRemuneracionPuestoTrabajoDTO dto, string usuario);
        object ProcesarPuestoTrabajoRemuneracionExcel(IFormFile file, string usuario);
        GestionRemuneracionPuestoTrabajoDTO Actualizar(GestionRemuneracionPuestoTrabajoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
