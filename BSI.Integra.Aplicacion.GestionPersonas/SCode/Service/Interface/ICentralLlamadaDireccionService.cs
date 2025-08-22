using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface ICentralLlamadaDireccionService
    {
        IEnumerable<CentralLlamadaDireccionDTO> Obtener();
        IEnumerable<DominioPbxDTO> ObtenerComboDominioPbx();

    }
}
