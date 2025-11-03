using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IProgramaMotivacionService
    {
        IEnumerable<ProgramaMotivacionDTO> Obtener();
        ProgramaMotivacionDTO Insertar(ProgramaMotivacionDTO dto, string usuario);
        ProgramaMotivacionDTO Actualizar(ProgramaMotivacionDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
