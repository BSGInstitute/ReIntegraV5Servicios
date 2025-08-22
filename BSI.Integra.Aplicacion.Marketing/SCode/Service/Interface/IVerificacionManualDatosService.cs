using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IVerificacionManualDatosService
    {
        IEnumerable<VerificacionManualDatosCompuestoDTO> ObtenerDatosVerificacion(FiltroBusquedaVerificacionManualDatosCompuestoDTO paginador);
    }
}
