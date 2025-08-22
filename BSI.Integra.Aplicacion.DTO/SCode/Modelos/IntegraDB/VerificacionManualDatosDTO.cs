using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class VerificacionManualDatosDTO
    {
        public IEnumerable<ComboDTO> listaCentroCosto { get; set; }
        public IEnumerable<ComboDTO> listaCategoriaDato { get; set; }
        public IEnumerable<PaisComboDTO> listaPais { get; set; }
        public IEnumerable<CiudadComboDTO> listaCiudad { get; set; }
        public IEnumerable<DTO.ComboDTO> listaProbabilidad { get; set; }
        public IEnumerable<ComboDTO> listaIndustria { get; set; }
        public IEnumerable<ComboDTO> listaCargo { get; set; }
        public IEnumerable<ComboDTO> listaAreaTrabajo { get; set; }
        public IEnumerable<ComboDTO> listaAreaFormacion { get; set; }
    }
    public class FiltroBusquedaVerificacionManualDatosCompuestoDTO
    {
        public PaginadorDTO paginador { get; set; }
        public FiltroVerificacionManualDatosDTO? filtroRegistros { get; set; }
    }
    public class FiltroVerificacionManualDatosDTO
    {
        public string IdCentroCosto { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string IdIndustria { get; set; }
        public string IdCategoriaDato { get; set; }
        public string IdCargo { get; set; }
        public string IdPais { get; set; }
        public string IdAreaTrabajo { get; set; }
        public string IdProbabilidad { get; set; }
        public string IdAreaFormacion { get; set; }
    }
    public class VerificacionManualDatosCompuestoDTO
    {
        public int TotalRegistros { get; set; }
        public int? Id { get; set; }
        public int? IdAlumno { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Movil { get; set; }
        public string Correo { get; set; }
        public string? MovilEncriptado { get; set; }
        public string? CorreoEncriptado { get; set; }
        public string AreaFormacion { get; set; }
        public int? IdAreaFormacion { get; set; }
        public string Cargo { get; set; }
        public int? IdCargo { get; set; }
        public string AreaTrabajo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public string Industria { get; set; }
        public int? IdIndustria { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string FechaRegistro { get; set; }
        public string HoraRegistro { get; set; }
        public string NombrePrograma { get; set; }
        public string CentroCosto { get; set; }
        public int? IdCentroCosto { get; set; }
        public string? TipoDato { get; set; }
        public int? IdTipoDato { get; set; }
        public string Categoria { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public string Origen { get; set; }
        public int? IdOrigen { get; set; }
        public string OrigenCampania { get; set; }
        public string Formulario { get; set; }
        public string FaseOportunidad { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string Pais { get; set; }
        public int? IdPais { get; set; }
        public string Ciudad { get; set; }
        public int? IdCiudad { get; set; }
        public decimal? ProbabilidadActual { get; set; }
        public string NombreProbabilidadActual { get; set; }
        public int? CodigoProbabilidad { get; set; }
        public bool? AptoProcesamiento { get; set; }
        public string OriginalNombre1 { get; set; }
        public string OriginalNombre2 { get; set; }
        public string OriginalApellidoPaterno { get; set; }
        public string OriginalApellidoMaterno { get; set; }
        public string OriginalTelefono { get; set; }
        public string OriginalMovil { get; set; }
        public string OriginalCorreo { get; set; }
        public int? OriginalIdAreaFormacion { get; set; }
        public int? OriginalIdCargo { get; set; }
        public int? OriginalIdAreaTrabajo { get; set; }
        public int? OriginalIdIndustria { get; set; }
    }
}
