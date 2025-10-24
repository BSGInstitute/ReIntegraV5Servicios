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
        ProgramaGeneralProblemaDetalleInsertarDTO Insertar(ProgramaGeneralProblemaDetalleInsertarDTO dto, string usuario);
    }
}
