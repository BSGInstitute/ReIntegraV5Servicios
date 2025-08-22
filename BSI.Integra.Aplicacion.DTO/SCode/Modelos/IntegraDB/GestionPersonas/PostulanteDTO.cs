using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    public class PostulanteDTO
    {
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string? NroDocumento { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public string? Telefono2 { get; set; }
        public string? Celular2 { get; set; }
        public string? Celular3 { get; set; }
        public string? Email2 { get; set; }
        public string? Email3 { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdSexo { get; set; }
        public string? UrlPerfilFacebook { get; set; }
        public string? UrlPerfilLinkedin { get; set; }
        public bool? EsProcesoAnterior { get; set; }
        public int? Edad { get; set; }
        public bool? TieneHijo { get; set; }
        public int? CantidadHijo { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
        public int? IdPersonalOperadorProceso { get; set; }
        public int? IdPaginaReclutadoraPersonal { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
    }


    public class PostulanteFormulariDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NroDocumento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public int? IdEstadoProcesoSeleccion { get; set; }
        public DateTime? FechaEnvioAccesos { get; set; }
        public DateTime? FechaRendicionExamen { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdPostulanteProcesoSeleccion { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public int? IdPaginaReclutadoraPersonal { get; set; }
        public int? IdPersonalOperadorProceso { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public int? IdSexo { get; set; }
        public int? Edad { get; set; }
        public bool? TieneHijo { get; set; }
        public int? CantidadHijo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? UrlPerfilFacebook { get; set; } = null!;
        public string? UrlPerfilLinkedin { get; set; } = null!;
        //SE comento por peticion de GP
        //public List<FiltroInsertarDTO> ListaRespuestaDesaprobatoria { get; set; }
    }
    //SE comento por peticion de GP
    //public class FiltroInsertarDTO
    //{
    //    public int IdRespuestaDesaprobatoria { get; set; }
    //    public string Nombre { get; set; }
    //    public int? Id { get; set; }
    //}

    public class InsertarPostulanteDTO
    {
        public PostulanteFormulariDTO DatosPostulanteFormulario { get; set; }
        public string Usuario { get; set; }
    }
    public class PostulanteProcesoDTO
    {
        [Required]
        public int IdPostulante { get; set; }
        [Required]
        public int IdProcesoSeleccion { get; set; }
    }
    public class PostulanteProcesoEvaluacionesDTO
    {
        public int IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public int IdExamen { get; set; }
        public string NombreExamen { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public bool EstadoExamen { get; set; }
    }

    public class PostulanteProcesoFormatDTO
    {
        public int IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public List<PostulanteProcesoExamenesDTO> ListaExamenes { get; set; }
    }
    public class PostulanteProcesoExamenesDTO
    {
        public int IdExamen { get; set; }
        public string NombreExamen { get; set; }
        public bool EstadoExamen { get; set; }

    }
    public class PostulanteInformacionProcesoDTO
    {
        public int IdPostulante { get; set; }
        public string NombrePostulante { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NroDocumento { get; set; }
        public string Email { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public DateTime? FechaProcesoSeleccion { get; set; }
        public string NombrePersonal { get; set; }
    }
    public class DatosPostulanteDTO
    {
        public int IdPostulante { get; set; }
        public string Nombre { get; set; } = null!;
        public string? ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string? NroDocumento { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdPostulanteProcesoSeleccion { get; set; }
        public int? IdEstadoProcesoSeleccion { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public Nullable<DateTime> FechaRendicionExamen { get; set; }
        public Nullable<DateTime> FechaEnvioAccesos { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPersonal_OperadorProceso { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public bool? EsProcesoAnterior { get; set; }
        public string? IdRespuestas { get; set; }
        public int? IdSexo { get; set; }
        public Nullable<DateTime> FechaNacimiento { get; set; }
        public bool? TieneHijo { get; set; }
        public int? CantidadHijo { get; set; }
        public string? UrlPerfilFacebook { get; set; }
        public string? UrlPerfilLinkedin { get; set; }

        public string? Codigo { get; set; }
        public string? NombreCombocatoria { get; set; }

        public string? Telefono2 { get; set; }
        public string? Celular2 { get; set; }
        public string? Celular3 { get; set; }
        public string? Email2 { get; set; }
        public string? Email3 { get; set; }
        public int? Edad { get; set; }
        public int? IdPersonalOperadorProceso { get; set; }
        public int? IdPaginaReclutadoraPersonal { get; set; }
    }

    public class ResultadoDatosPostulanteDTO
    {
        public IEnumerable<DatosPostulanteDTO> data { get; set; }
        public int Total { get; set; }
        //public List<GridFilterDTO> filtrosUsado { get; set; }
    }

    public class TotalDatosPostulanteDTO
    {
        public int TotalFilas { get; set; }
    }


    public class DatosPostulanteFiltroGrillaDTO
    {
        //Filtro,sort,skip,take
        public FiltroKendoGridDTO? filter { get; set; }
        public DatosPostulanteDTO filtro { get; set; }
    }


    public class ReportePostulanteMatriculaDTO
    {
        public int IdPostulante { set; get; }
        public string NombrePostulante { get; set; }
        public string ValorEscala { get; set; }
        public string NombreProgramaEspecifico { get; set; }
        public Guid? Usuario { get; set; }
        public int ProgramaGeneral { get; set; }
    }
    public class EnvioDatosReestablecerDTO
    {
        public int IdPostulante { get; set; }
        public int IdProgramaGeneral { get; set; }
    }
    public class DatosMatriculaPostulanteDTO
    {
        public int idPostulante { get; set; }
        public string? Usuario { get; set; }
        public string? Contraseña { get; set; }
        public int IdAlumno { get; set; }
        public string? NombrePostulante { get; set; }
        public string? ApellidoPostulante { get; set; }
        public string? ApellidoP { get; set; }
        public string? ApellidoM { get; set; }
        public string? NombreCurso { get; set; }
        public string? Email { get; set; }
        public Nullable<int>  idPEspecifico { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public string? CodigoMatricula { get; set; }
    }
    public class ProcesoSeleccionInscritoDTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int? IdSede { get; set; }
        public string Sede { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    public class EnvioPlantillaPostulanteDTO
    {
        public int IdPostulante { get; set; }
        public List<int> ListaIdPostulanteProcesoSeleccion { get; set; }
        public string? Descripcion { get; set; }
        public int IdPlantilla { get; set; }
        public string Usuario { get; set; }
        public DateTime? Fecha { get; set; }
    }

    public class PostulanteAlumnoDTO
    {
        public int IdPostulante { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdClasificacionPersona { get; set; }
    }

    public class CuentaPortalDevuelvePostulanteDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int idAlumno { get; set; }

    }

    public class PostulanteImportadoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public string Origen { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public List<int>? ListaRespuestaDesaprobatoria { get; set; }
    }

    public class ImportacionPostulanteResultadoDTO
    {
        public List<PostulanteImportadoDTO> ListaPostulante { get; set; }
        public List<PostulanteImportadoDTO> ListaPostulanteRepetido { get; set; }
        public List<string> ListaErrores { get; set; }
        public int NregistrosNuevo { get; set; }
        public int NregistrosRepetido { get; set; }
    }

    public class PostulanteProcesoSeleccionConsolidadoDTO
    {
        public List<PostulanteImportadoDTO> listaPostulante { get; set; }
        public string Usuario { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdEtapaProcesoSeleccion { get; set; }
        public int IdProveedor { get; set; }
        public int IdPersonalOperadorProceso { get; set; }
        public int IdConvocatoria { get; set; }
    }

    public class ResultadoInsertarPostulante
    {
        public string Mensaje { get; set; }
        public bool Valor { get; set; }
    }


    public class PostulanteInformacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NroDocumento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdPostulanteProcesoSeleccion { get; set; }
        public int? IdEstadoProcesoSeleccion { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }
        public DateTime? FechaRendicionExamen { get; set; }
        public DateTime? FechaEnvioAccesos { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPersonal_OperadorProceso { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
        public int? IdEstadoEtapaProcesoSeleccion { get; set; }
        public string IdRespuestas { get; set; }
        public List<int> ListaRespuestaDesaprobatoria { get; set; }
        public int? IdSexo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? CantidadHijo { get; set; }
        public string UrlPerfilFacebook { get; set; }
        public string UrlPerfilLinkedin { get; set; }
    }

    public class InsertarPostulanteImportadoDTO
    {
        public PostulanteInformacionDTO DatosPostulanteImportación{ get; set; }
        public string Usuario { get; set; }
    }

}
