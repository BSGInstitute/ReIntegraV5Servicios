using BSI.Integra.Aplicacion.DTOs;


namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RegistrarOportunidadFitroGrillaDTO
    {
        public PaginadorDTO? paginador { get; set; }
        public FiltrosRegistrarOportunidadDTO? filtro { get; set; }
    }
    public class FiltrosRegistrarOportunidadDTO
    {
        public string? CentrosCosto { get; set; }
        public string? Asesores { get; set; }
        public string? TiposDato { get; set; }
        public string? Origenes { get; set; }
        public string? FasesOportunidad { get; set; }
        public string? contacto { get; set; }
        public Nullable<DateTime> FechaInicio { get; set; }
        public Nullable<DateTime> FechaFin { get; set; }
    }
    public class ResultadoOportunidadesDTO
    {
        public List<ResultadoRegistrarOportunidadFiltroDTO> data { get; set; }
        public int Total { get; set; }
    }

    public class ResultadoRegistrarOportunidadFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int? IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public int? IdTipoDato { get; set; }
        public string NombreTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public string CodigoFase { get; set; }
        public string CodigoFaseMaxima { get; set; }
        public int? IdOrigen { get; set; }
        public string NombreOrigen { get; set; }
        public int? CodigoPais { get; set; }
        public string NombrePais { get; set; }
        public int? CodigoCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public string HoraPeru { get; set; }
        public string HoraContacto { get; set; }
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Dni { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdCargo { get; set; }
        public int? IdFormacion { get; set; }
        public int? IdTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? IdOportunidad { get; set; }
        public DateTime? FechaCreacionOportunidad { get; set; }
        public int? IdReferido { get; set; }
        public bool? Asociado { get; set; }
        public string NombreGrupo { get; set; }
        public string CodigoMailing { get; set; }
    }
    public class RegistroOportunidadAlumnoDTO
    {
        public AlumnoFormularioOportunidadDTO Alumno { get; set; }
        public OportunidadFormularioDTO Oportunidad { get; set; }
        public string Usuario { get; set; }
        public DateTime? FechaRegistroCampania { get; set; }
    }
   
    public class OportunidadFiltroSegmentoDTO
    {
        public int IdFiltroSegmento { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdTipoDato { get; set; }
        public int IdOrigen { get; set; }
        public int IdFaseOportunidad { get; set; }
        public List<int> ListadoIdsAlumnos { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class RespuestaOportunidadFiltroSegmentoDTO
    {
        public int CantidadOportunidadesCreadas { get; set; }
        public List<Error> Errores { get; set; }
        public bool Exito => Errores == null || !Errores.Any();
    }



    public class LogFiltroSegmentoEjecuDTO 
    {
        public  bool TieneErrores { get; set; }

        public int Id { get; set; }

        public int IdFiltroSegmento { get; set; }

        public int IdCentroCosto { get; set; }

        public int IdTipoDato { get; set; }

        public int IdOrigen { get; set; }

        public int IdFaseOportunidad { get; set; }

        public int TotalOportunidadesCreadas { get; set; }

      
    }


    public class ResultadoCrearOportunidadDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdClasificacionPersona { get; set; }
    }
}
