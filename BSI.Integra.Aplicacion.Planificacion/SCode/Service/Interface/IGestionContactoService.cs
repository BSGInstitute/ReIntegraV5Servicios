using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IGestionContactoService
    {
        Task<int> ProcesarInsercionGestionAsync(CrearGestionContactoDTO dto);
    }
}
