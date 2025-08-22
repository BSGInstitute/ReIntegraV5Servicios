using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IAsignacionManualOportunidadOperacionesService
    {
        bool AsignarOportunidadTabActual(AsignarOportunidadOperacionesFiltroDTO objeto);
        bool AsignarOportunidadOperaciones(AsignarOportunidadOperacionesFiltroDTO objeto);
    }
}
