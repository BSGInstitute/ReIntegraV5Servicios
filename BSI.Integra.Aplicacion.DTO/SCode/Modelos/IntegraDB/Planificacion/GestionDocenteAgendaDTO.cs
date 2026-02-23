using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    /// <summary>
    /// DTO para un �tem de la lista de docentes con cursos asignados.
    /// </summary>
    public class DocenteConCursoDTO
    {
        public int IdProveedor { get; set; }
        public string NombreDocente { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }
        public int IdGestionContacto { get; set; }
        public int IdFlujo { get; set; }
        public string NombreFlujo { get; set; }
    }

    public class DocenteAgendaDetalleDTO
    {
        public DocenteAgendaCabeceraDTO Cabecera { get; set; }
        public DocenteAgendaFlujoDTO Flujo { get; set; }
        public List<DocenteAgendaCronogramaDTO> Cronogramas { get; set; }
    }
    public class DocenteAgendaCronogramaDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public string EstadoCurso { get; set; }
        public string TipoCurso { get; set; }
        public string CategoriaCurso { get; set; }
        public string CiudadCurso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public bool EsPriorizado { get; set; }
        public List<DocenteAgendaSesionDTO> Sesiones { get; set; }
    }

    public class DocenteAgendaCabeceraDTO
    {
        public int IdProveedor { get; set; }
        public string NombreCompleto { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public string PersonalAsignado { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
    }

    public class DocenteAgendaFlujoDTO
    {
        public int IdFlujo { get; set; }
        public string NombreFlujo { get; set; }
        public string DescripcionFlujo { get; set; }
    }

    /// <summary>
    /// DTO para una sesión dentro de un cronograma de curso.
    /// </summary>
    public class SesionCronogramaDTO
    {
        public int IdSesion { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int Grupo { get; set; }
        public string Comentario { get; set; }
    }

    /// <summary>
    /// DTO para un cronograma (programa específico) del docente con sus sesiones.
    /// </summary>
    public class CronogramaDocenteDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombreCurso { get; set; }
        public string CodigoCurso { get; set; }
        public string EstadoCurso { get; set; }
        public string TipoCurso { get; set; }
        public string CategoriaCurso { get; set; }
        public string CiudadCurso { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        public bool EsPriorizado { get; set; }
        public List<DocenteAgendaSesionDTO> Sesiones { get; set; }
    }

   


    public class DocenteAgendaSesionDTO
    {
        public int IdSesion { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public decimal Duracion { get; set; }
        public int Grupo { get; set; }
        public string Comentario { get; set; }
    }
}
