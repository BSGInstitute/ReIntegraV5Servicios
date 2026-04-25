using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class GrabacionesClasesOnlineFiltroDTO
    {
        public List<int>? Area { get; set; }
        public List<int>? SubArea { get; set; }
        public List<int>? PGeneral { get; set; }
        public List<int>? PEspecifico { get; set; }
        public List<int>? Partner { get; set; }
    }

    public class GrabacionesClasesOnlineDTO
    {
        public string NombrePGeneral { get; set; }
        public string NombrePEspecifico { get; set; }
        public string NombreArea { get; set; }
        public string NombreSubArea { get; set; }
        public string NombrePartner { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string IdPEspecifico { get; set; }
    }

    public class SesionesFiltroDTO
    {
        public int? IdPEspecifico { get; set; }
    }

    public class SesionesClasesOnlineDTO
    {
        public string Sesion { get; set; }
        public string FechaSesion { get; set; }
        public string Id { get; set; }
        public string IdPEspecifico { get; set; }
        public string IdPEspecificoSesion { get; set; }
        public string NombreSesion { get; set; }
        public string IdTipoProveedorVideo { get; set; }
        public string Video { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Habilitado { get; set; }
    }

    public class SesionesClasesOnlineResumenDTO
    {
        public string Sesion { get; set; }
        public string FechaSesion { get; set; }
        public string Id { get; set; }
        public string IdPEspecifico { get; set; }
        public string IdPEspecificoSesion { get; set; }
        public string NombreSesion { get; set; }
        public string IdTipoProveedorVideo { get; set; }
        public string Video { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Habilitado { get; set; }
        public string EstadoResumen { get; set; }
        public string FechaFinalizacionResumen { get; set; }
        public string FechaCreacionProcesamientoTipoGenerar { get; set; }
        public int? PorcentajeFidelidad { get; set; }
        public bool ResumenClaseActivo { get; set; }
        public bool TutorVirtualActivo { get; set; }
        public string EstadoTutor { get; set; }
        public string FechaFinalizacionTutor { get; set; }
    }

    public class SesionesClasesOnlineDetalleResumenDTO
    {
        public string Sesion { get; set; }
        public string FechaSesion { get; set; }
        public string Id { get; set; }
        public string IdPEspecifico { get; set; }
        public string IdPEspecificoSesion { get; set; }
        public string NombreSesion { get; set; }
        public string IdTipoProveedorVideo { get; set; }
        public string Video { get; set; }
        public string UrlPdf { get; set; }
        public string UrlMapa { get; set; }
        public string UrlAudio { get; set; }
        public string UrlVideo { get; set; }
        public int IdProcesamientoSesionOnline { get; set; }
    }

    public class SesionesClasesOnlineConfigurarDTO
    {
        public string IdPEspecifico { get; set; }
        public string IdPEspecificoSesion { get; set; }
        public string NombreSesion { get; set; }
        public string IdTipoProveedorVideo { get; set; }
        public string Video { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Habilitado { get; set; }
    }

    public class SesionesClasesOnlineModificarFiltroDTO
    {
        public List<SesionesClasesOnlineConfigurarDTO> Data { get; set; }
    }

    public class DataDisponibilidadProgramaDefectoDTO
    {
        public string Id { get; set; }
        public string NumeroDia { get; set; }
    }

    /// <summary>
    /// DTO para el resultado del SP SP_PEspecificoUltimaSesionPorIdPEspecifico
    /// </summary>
    public class PEspecificoUltimaSesionDTO
    {
        public int Id { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
    }

}
