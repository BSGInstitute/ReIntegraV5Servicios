using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface IProcesoSeleccionEtapaService
    {
        IEnumerable<ProcesosSeleccionEtapaComboDTO> ObtenerComboProcesoSeleccionEtapa();
    }
}
