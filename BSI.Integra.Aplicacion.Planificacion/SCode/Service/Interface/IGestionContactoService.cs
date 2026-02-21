using BSI.Integra.Aplicacion.DTO;
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
        IEnumerable<ComboDTO> ObtenerFiltroAutocomplete(string valor);
        IEnumerable<ComboDTO> ObtenerPEspecificoPorCentroCosto(int idCentroCosto);
        IEnumerable<PEspecificoSesionProveedorDTO> ObtenerSesionesProveedorPorPEspecifico(int idPEspecifico);
        IEnumerable<ComboDTO> ObtenerGestionDocenteFlujos();
        Task<int> InsertarOportunidadDocenteAsync(CrearOportunidadDocenteDTO dto);
        IEnumerable<EstadoGestionContactoDTO> ObtenerEstadosGestionContacto();
        Task<int> InsertarGestionContactoDocenteFlujoAsync(InsertarGestionContactoDocenteFlujoDTO dto);
        Task<int> CongelarFlujoDocenteAsync(int idGestionContactoDocenteFlujo);
    }
}
