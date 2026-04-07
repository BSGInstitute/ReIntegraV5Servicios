using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PEspecificoSesionDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public string Comentario { get; set; }
        public bool? SesionAutoGenerada { get; set; }
        public bool? Predeterminado { get; set; }
        public bool Estado { get; set; }
    }
    public class SesionTempDTO
    {
        public string Dia { get; set; }
        public string Horainicio { get; set; }
        public string Horafin { get; set; }
        public string Horainicio2 { get; set; }
        public string Horafin2 { get; set; }
        public bool Tipo { get; set; }
        public string Ciudad { get; set; }
        public int Idciudad { get; set; }
        public int Veces { get; set; }
    }
    public class PEspecificoProximaSesionWebexDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NombreDia { get; set; }
        public string UrlWebex { get; set; }
    }
    public class PEspecificoSesionFechaHoraInicioDTO
    {
        public int IdPEspecifico { get; set; }
        public DateTime? FechaHoraInicio { get; set; }
    }
    public class InformacionProgramaEspecificoSesionDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdProveedor { get; set; }
        public string Comentario { get; set; }
        public bool SesionAutoGenerada { get; set; }
        public int? IdAmbiente { get; set; }
        public bool? Predeterminado { get; set; }
    }
    public class PespecificoSesionCompuestoDTO
    {
        public int Id { get; set; }
        public int? IdPespecifico { get; set; }
        public int? PEspecificoHijoId { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal? Duracion { get; set; }
        public decimal? DuracionTotal { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdCiudad { get; set; }
        public string? Comentario { get; set; }
        public string Curso { get; set; }
        public string Tipo { get; set; }
        public string ModalidadSesion { get; set; }
        public bool? SesionAutoGenerada { get; set; }
        public int? IdAmbiente { get; set; }
        public bool? Predeterminado { get; set; }
        public bool? EsSesionInicial { get; set; }
        public bool? Cruce { get; set; }
        public bool? MostrarPDF { get; set; }
    }
    public class FechaSesionDTO
    {
        public int IdSesion { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class VerificarFechaSesionDTO
    {
        public IEnumerable<FeriadoDTO> Feriados { get; set; }
        public List<CruceFechaSesionDTO> CrucesExpositor { get; set; }
        public List<CruceFechaSesionDTO> CrucesAmbiente { get; set; }
    }
    public class CruceFechaSesionDTO
    {
        public int SesionId { get; set; }
        public string SesionFechaInicio { get; set; }
        public string SesionFechaFin { get; set; }
        public string SesionComentario { get; set; }
        public int PespecificoId { get; set; }
        public string PespecificoNombre { get; set; }
        public string AmbienteId { get; set; }
        public string AmbienteNombre { get; set; }
        public int ExpositorId { get; set; }
        public string ExpositorNombre { get; set; }
    }
    public class InformacionCronogramaSesionesDTO
    {
        public int Id { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdAmbiente { get; set; }
        public int? IdModalidadCurso { get; set; }
        public int? GrupoSesion { get; set; }
        public bool AplicarCambios { get; set; }
        public bool? MostrarPortalWeb { get; set; }
    }
    public class CruceSesionPEspecificoDTO
    {
        public int Id { get; set; }
        public int IdPEspecifico { get; set; }
        public string Curso { get; set; }
        public string NombreCentroCosto { get; set; }
        public string Ambiente { get; set; }
        public string Expositor { get; set; }
        public string Proveedor { get; set; }
        public double Duracion { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? IdAmbiente { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdProveedor { get; set; }
    }
    public class PadrePespecificoHijoCompuestoDTO
    {
        public int Id { get; set; }
        public int IdSesion { get; set; }
        public int PEspecificoHijoId { get; set; }
        public int PEspecificoPadreId { get; set; }
    }
    public class ActualizarFechaPorSesionDTO
    {
        public int SesionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentario { get; set; }
        public bool RecorrerFecha { get; set; }
        public bool EsFechaInicio { get; set; }
    }
    public class EsquemaSesionesDTO
    {
        public InformacionPespecificoHijoDTO Curso { get; set; }
        public decimal? Duracion { get; set; }
        public byte? Dia { get; set; }
        public DateTime? FechaAsignar { get; set; }
        public int? SesionId { get; set; }
    }
    public class CancelarWebinarDTO
    {
        public int? IdPEspespecifico { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public string ComentarioCancelacion { get; set; }
        public bool? Confirmo { get; set; }
    }
    public class InformacionPespecificoSesionDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int? IdExpositor { get; set; }
        public string Comentario { get; set; }
        public bool SesionAutoGenerada { get; set; }
        public int? IdAmbiente { get; set; }
        public bool? Predeterminado { get; set; }
        public int Grupo { get; set; }
        public int? GrupoSesion { get; set; }
        public int? IdPEspecificoSesionEstado { get; set; }
        public bool? Reprogramacion { get; set; }
    }
    public class ConfirmacionWebinarDTO
    {
        public int IdPEspecificoSesion { get; set; }
        public string? ComentarioCancelacion { get; set; }
        public bool Confirmo { get; set; }
    }
    public class DetalleSesionesAlumnosDTO
    {
        public int IdPGeneral { get; set; }
        public int IdAlumno { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdSesion { get; set; }
        public int IdCoordinadoraAcademica { get; set; }
        public string EmailCoordinadoraAcademica { get; set; }
        public string NombreCoordinadoraAcademica { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string CentroCosto { get; set; }
        public string EstadoMatricula { get; set; }
        public string Confirmo { get; set; }
        public string Email { get; set; }
        public string NombrePais { get; set; }
        public string ZonaHoraria { get; set; }
        public string EnvioCorreo { get; set; }
        public string EnvioWhatsApp { get; set; }
        public string CelularWhatsApp { get; set; }
        public int IdPais { get; set; }
    }
    public class SesionFiltroDTO
    {
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdSesion { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string? CodigoMatricula { get; set; }
    }
    public class PEspecificoSesionesRecordatorioClases
    {
        public string Sesion { get; set; }
        public int IdPespecificoSesion { get; set; }
        public string PEspecifico { get; set; }
        public DateTime FechaSesion { get; set; }
        public string HoraSesion { get; set; }
    }

    public class RptaActualizarDatosCronogramaSesionesDTO
    {
        public bool EstadoCruce { get; set; }
        public IEnumerable<CruceSesionPEspecificoDTO?> Cruces { get; set; }
        public string? Detalle { get; set; }
        public int? IdPEspecificoSesion { get; set; }
        public int? IdTipoPrograma { get; set; }
        public DateTime? FechaSesion { get; set; }
    }
    public class RptaActualizarDuracionInsertarSesionDTO
    {
        public int IdPEspecificoSesion { get; set; }
        public int IdTipoPrograma { get; set; }
        public DateTime FechaSesion { get; set; }
    }
    public class ReprogramarSesionDTO
    {
        public int IdPespecifico { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdAmbiente { get; set; }
        public string Comentario { get; set; }
        public int Grupo { get; set; }
        public int GrupoSesion { get; set; }
        public int? IdModalidadCurso { get; set; }
    }
}
