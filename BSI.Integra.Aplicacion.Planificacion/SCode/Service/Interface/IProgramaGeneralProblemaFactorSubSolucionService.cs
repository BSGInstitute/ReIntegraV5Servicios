using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaGeneralProblemaFactorSubSolucionService
    {
        IEnumerable<ProgramaGeneralProblemaFactorSubSolucionDTO> Obtener();
        ProgramaGeneralProblemaFactorSubSolucionDTO Insertar(ProgramaGeneralProblemaFactorSubSolucionDTO dto, string usuario);
        ProgramaGeneralProblemaFactorSubSolucionDTO Actualizar(ProgramaGeneralProblemaFactorSubSolucionDTO dto, string usuario);
        bool Eliminar(int id, string usuario);

    }
}
