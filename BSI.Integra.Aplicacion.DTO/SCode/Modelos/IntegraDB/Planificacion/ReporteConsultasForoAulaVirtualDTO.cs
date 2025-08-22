using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ReporteConsultasForoAulaVirtualDTO
    {
        public int IdForoCurso { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public string Curso { get; set; }
        public string Docente { get; set; }
        public string CoordinadorDocente { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string Tema { get; set; }
        public string Consulta { get; set; }
        public string FechaConsulta { get; set; }
        public string HoraConsulta { get; set; }
        public string Respuesta { get; set; }
        public string FechaRespuesta { get; set; }
        public string HoraRespuesta { get; set; }
        public string UsuarioRespuesta { get; set; }
        public string EstadoAtendido { get; set; }
        public string EstadoCerrado { get; set; }
        public string NombreCoordinadora { get; set; } = null;

    }
    public class ReporteConsultasForoFiltroDTO
    {
        public List<int> Programa { get; set; }
        public List<int> Docente { get; set; }
        public List<int> Curso { get; set; }
        public int? EstadoConsulta { get; set; }

        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }

    }
    public class ReasignacionForoDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
    }
    public class ReporteConsultasForoDetalleAulaVirtualDTO
    {
        public int IdForoCurso { get; set; }
        public int IdForoRespuesta { get; set; }
        public string Docente { get; set; }
        public string Tema { get; set; }
        public string Respuesta { get; set; }
        public string UsuarioRespuesta { get; set; }
        public string CodigoMatricula { get; set; }
        public string FechaRespuesta { get; set; }
        public string HoraRespuesta { get; set; }
        public string CentroCosto { get; set; } = null;

        public string NombrePrograma { get; set; } = null;
        public string NombreCoordinadora { get; set; } = null;

    }
    public class ForosCorreoDTO
    {
        public int IdForo { get; set; }
        public string Usuario { get; set; }
        public int IdProveedor { get; set; }
        public string CodigoMatricula { get; set; }
        public string Curso { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
    public class ForoCorreoDetalleDTO
    {
        public string NombreProveedor { get; set; }
        public string NombreCurso { get; set; }
        public string Asunto { get; set; }
        public string Consulta { get; set; }
        public string FechaConsulta { get; set; }
        public string FechaLimite { get; set; }
        public string Email { get; set; }


    }
    public class ResumenForoCorreoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
    public class AperturaForoDTO
    {
        public int IdForo { get; set; }
        public bool EstadoForo { get; set; }
    }
    public class EstadoAtencionForoDTO
    {
        public int IdForo { get; set; }
        public bool EstadoAtendido { get; set; }
    }

    public class EliminarForoRespuestaDTO
    {
        public int IdForoRespuesta { get; set; }
        public string UsuarioModificacion { get; set; }
    }
    public class ComboReporteForoDTO
    {
        public IEnumerable<ComboDTO> Programa { get; set; }
        public IEnumerable<ComboDTO> Docente { get; set; }
        public IEnumerable<ComboDTO> Curso { get; set; }

    }
}
