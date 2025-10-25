using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralProblemaDetalleService
    {
        ProgramaGeneralProblemaDetalleDTO Insertar(ProgramaGeneralProblemaDetalleDTO dto, string usuario);
        ProgramaGeneralProblemaDetalleDTO Actualizar(ProgramaGeneralProblemaDetalleDTO dto, string usuario);
        IEnumerable<ProgramaGeneralProblemaDetalleDTO> Obtener(int idPGeneral);

    }
}
