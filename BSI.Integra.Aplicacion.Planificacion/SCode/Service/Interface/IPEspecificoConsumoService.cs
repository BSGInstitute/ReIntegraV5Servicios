using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPEspecificoConsumoService
    {
        bool InsertarFurSesiones(List<PEspecificoConsumoDTO> dto, string usuario);
        bool InsertarFurPrograma(FurProgramaDTO dto, string usuario);
        bool EliminarSesionFur(int idFurSesion, string usuario);
        bool ActualizarSesionFur(FurSesionFiltroDTO dto, string usuario);
    }
}
