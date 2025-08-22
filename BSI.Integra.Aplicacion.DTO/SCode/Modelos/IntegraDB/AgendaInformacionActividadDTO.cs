using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ParametroFinalizarActividadAlternoDTO
    {
        public DatosFiltroFinalizarActividadDTO? filtro { get; set; }
        public OportunidadDTO datosOportunidad { get; set; }
        public ActividadDetalleDTO ActividadAntigua { get; set; }
        public DatosCompuestoDTO DatosCompuesto { get; set; }
        //public DatosCompuestoDTO DatosCompuesto { get; set; }
        public ComprobantePagoOportunidadDTO? ComprobantePago { get; set; }
        public CalidadLlamadaDTO? CalidadLlamada { get; set; }
        public string Usuario { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string? tipoProgramacion { get; set; }//manual o automatica
    }
    public class DatosFiltroFinalizarActividadDTO
    {
        public int IdOcurrencia { get; set; }
        public string Tipo { get; set; }
        public int IdActividadCabecera { get; set; }
        public int IdCategoria { get; set; }
        public int? IdPersonal { get; set; }
        public string Usuario { get; set; }
    }
    public class OportunidadPreriquisitosBeneficiosSolucionesCompuestoDTO
    {
        public OportunidadCompetidorFinalizarActividadDTO? OportunidadCompetidor { get; set; }
        public List<OportunidadPrerequisitoGeneralDTO>? ListaPrerequisitoGeneral { get; set; }
        public List<OportunidadPrerequisitoEspecificoDTO>? ListaPrerequisitoEspecifico { get; set; }
        public List<OportunidadBeneficioDTO>? ListaBeneficio { get; set; }
        public List<int>? ListaCompetidor { get; set; }
        public List<SolucionClienteByActividadDTO>? ListaSoluciones { get; set; }
        //Ultimo Agregado
        public List<OportunidadPrerequisitoGeneralAlternoDTO>? ListaPrerequisitoGeneralAlterno { get; set; }
        public List<OportunidadBeneficioAlternoDTO>? ListaBeneficioAlterno { get; set; }
    }
    public class DatosCompuestoDTO
    {
        public OportunidadCompetidorFinalizarActividadDTO OportunidadCompetidor { get; set; }
        public List<OportunidadMotivacionDTO> ListaMotivacion { get; set; }
        //public List<OportunidadMotivacionDTO> ListaMotivacion { get; set; }
        public List<OportunidadPublicoObjetivoDTO> ListaPublicoObjetivo { get; set; }
        public List<OportunidadCertificacionDTO> ListaCertificacion { get; set; }
        public List<OportunidadPrerequisitoGeneralAlternoDTO> ListaPrerequisitoGeneralAlterno { get; set; }
        public List<OportunidadBeneficioAlternoDTO> ListaBeneficioAlterno { get; set; }
        public List<int> ListaCompetidor { get; set; }
        //public List<SolucionClienteByActividadDTO> ListaSoluciones { get; set; }
        public List<SolucionClienteByActividadAlternoDTO> ListaSolucionesAlterno { get; set; }
    }

    public class OportunidadMotivacionDTO
    {
        public int IdOportunidad { get; set; }
        public int IdMotivacion { get; set; }
        public int Respuesta { get; set; }
    }

    public class OportunidadPublicoObjetivoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdPublicoObjetivo { get; set; }
        public int Respuesta { get; set; }
    }
    public class OportunidadCertificacionDTO
    {
        public int IdOportunidad { get; set; }
        public int IdCertificacion { get; set; }
        public int Respuesta { get; set; }
    }
    public class OportunidadBeneficioAlternoDTO
    {
        public int IdOportunidad { get; set; }
        public int IdBeneficio { get; set; }
        public int Respuesta { get; set; }
    }
    public class AlumnoActualizarDTO
    {
        public int Id { get; set; }
        public string Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Dni { get; set; }
        public string Email1 { get; set; }
        public string? Email2 { get; set; }
        public string Celular { get; set; }
        public string? Celular2 { get; set; }
        public string? Telefono { get; set; }
        public string? Telefono2 { get; set; }
        public string? Genero { get; set; }
        public string? Parentesco { get; set; }
        public string? NombreFamiliar { get; set; }
        public string? TelefonoFamiliar { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public int? IdCargo { get; set; }
        public string? Cargo { get; set; }
        public int? IdAtrabajo { get; set; }
        public string? Atrabajo { get; set; }
        public int? IdEmpresa { get; set; }
        public string? Empresa { get; set; }
        public int? IdAformacion { get; set; }
        public string? Aformacion { get; set; }
        public int? IdIndustria { get; set; }
        public string? Industria { get; set; }
        public int? IdCiudad { get; set; }
        public string? Ciudad { get; set; }
        public int? IdCodigoPais { get; set; }
        public string? Municipio { get; set; }
        public int? IdMunicipioMexico { get; set; }
        public string? EstadoLugar { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Colonia { get; set; }
        public int? IdAsentamientoMexico { get; set; }
        public int? IdCiudadMexico { get; set; }
        public string? Curp { get; set; }
        public string? Rfc { get; set; }
        public string? PrincipalResponsabilidadProfesional { get; set; }
        public int? IdExperiencia { get; set; }
        public int? IdTamanioEmpresaAgenda { get; set; }
    }

    public class AlumnoPerfilActualizarDTO
    {
        public int IdAlumno { get; set; }
        public int idNuevo { get; set; }
        public string? descripcion { get; set; }
    }
    public class FiltroBeneficiosSolicitadosPorAlumnos
    {
        public string? CodigoMatricula { get; set; }
        public string? BeneficioSolicitado { get; set; }
        public int? IdEstadoSolicitudBeneficio { get; set; }
        public DateTime? FechaProgramadaInicio { get; set; }
        public DateTime? FechaProgramadaFin { get; set; }
        public DateTime? FechaCongelamientoInicio { get; set; }
        public DateTime? FechaCongelamientoFin { get; set; }
    }

    public class EncuestaAsignadoMatriculaDTO
    {
        public int IdPEspecificoSesionEncuestaAlumno { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdEncuestaSesionPrograma { get; set; }
        public int IdPGeneral { get; set; }
        public string Descripcion { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string FechaEncuesta { get; set; }
        public string FechaEncuestaRealizada { get; set; }
        public string Estatus { get; set; }
        public string ComentarioAlumno { get; set; }
        public List<PEspecificoSesionEncuestaPreguntaCategoriaDTO> PreguntasEncuesta { get; set; }
        public List<PEspecificoSesionEncuestaAlumnoDTO> RespuestasEncuesta { get; set; }

    }

    public class PEspecificoSesionEncuestaPreguntaCategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public int IdEncuestaSesionPrograma { get; set; }
        public List<PEspecificoSesionEncuestaPreguntaDTO> Preguntas { get; set; }
    }

    public class PEspecificoSesionEncuestaPreguntaDTO
    {
        public int Id { get; set; }
        public int IdEncuestaSesionPrograma { get; set; }
        public int IdEncuestaOnline { get; set; }
        public int IdPreguntaEncuestaTipo { get; set; }
        public string Pregunta { get; set; }
        public bool DescripcionActiva { get; set; }
        public string? Descripcion { get; set; }
        public string NombreTipoPregunta { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public List<PEspecificoSesionEncuestaPreguntaAlternativaDTO> Alternativas { get; set; }
        public bool PreguntaObligatoria { get; set; }
        public bool PreguntaActiva { get; set; }
    }
    public class PEspecificoSesionEncuestaPreguntaAlternativaDTO
    {
        public int? Id { get; set; }
        public int IdEncuestaSesionPrograma { get; set; }
        public int IdEncuestaOnline { get; set; }
        public int IdPreguntaEncuesta { get; set; }
        public string Respuesta { get; set; }
        public int Orden { get; set; }
        public decimal Puntaje { get; set; }
        public int IdPEspecificoSesion { get; set; }
    }

    public class PEspecificoSesionEncuestaAlumnoDTO
    {
        public int Id { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public int IdEncuestaSesionPrograma { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int Puntaje { get; set; }
        public DateTime FechaRealizada { get; set; }
        public List<PEspecificoSesionEncuestaAlumnoRespuestaDTO> Respuestas { get; set; }

    }
    public class PEspecificoSesionEncuestaAlumnoRespuestaDTO
    {
        public int Id { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public int IdPreguntaEncuesta { get; set; }
        public int IdPEspecificoSesionEncuestaAlumno { get; set; }
        public int IdPreguntaRespuestaEncuesta { get; set; }
        public string Valor { get; set; }
        public decimal Puntos { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }

    public class AgregarPEspecificoSesionEncuestaAlumnoDTO
    {
        public int IdEncuestaSesionPrograma { get; set; }
        public bool Inicio { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecificoSesion { get; set; }
        public int IdPGeneral { get; set; }
        public int IdPEspecifico { get; set; }
        public List<EncuestaAvanceCategoriaDTO> Categorias { get; set; }
        public string? Usuario { get; set; }
    }

    public class EncuestaAvanceCategoriaDTO
    {
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public List<EncuestaAvancePreguntaDTO> Preguntas { get; set; }
    }

    public class EncuestaAvancePreguntaDTO
    {
        public int IdPregunta { get; set; }
        public string Pregunta { get; set; }
        public int IdPreguntaEncuestaTipo { get; set; }
        public bool PreguntaObligatoria { get; set; }
        public List<EncuestaAvancePreguntaRespuestaDTO> ValorRespuesta { get; set; }
    }

    public class EncuestaAvancePreguntaRespuestaDTO
    {
        public int IdRespuesta { get; set; }
        public string Respuesta { get; set; }
        public int Puntaje { get; set; }

    }

    public class EncuestaComentarioDTO
    {
        public int IdPEspecificoSesionEncuestaAlumno { get; set; }
        public string Comentario { get; set; } 
        public string? Usuario { get; set; } 

    }

}
