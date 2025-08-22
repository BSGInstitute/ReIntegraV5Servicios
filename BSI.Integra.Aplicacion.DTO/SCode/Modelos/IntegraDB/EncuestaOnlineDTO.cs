using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Google.Api.Ads.AdWords.v201809;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{

    public class EncuestaOnlineDTO
    {


        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public int? Version { get; set; }
    }

    public class EncuestaOnlineEntradaDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string Usuario { get; set; }
        public int? Version { get; set; }
        public int? IdTipoEncuesta { get; set; }
        public int? IdModalidadCurso { get; set; }
    }

    public class EncuestaRegistradaDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int? Version { get; set; }
        public int? IdTipoEncuesta { get; set; }
        public int? IdModalidadCurso { get; set; }
    }

    public class VersionEncuestaSincronicaDTO
    {
        public int version { get; set; }
    }

    public class EncuestaEstructuraAsincronicaDTO{
        public int Id { get; set; }
        public int IdExamen { get; set; }
        public int IdPGeneral { get; set; }
        public int IdSeccionPw { get; set; }
        public int Fila { get; set; }
        public int OrdenCapitulo { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Titulo { get; set; }
        public bool ?EncuestaObligatoria { get; set; }
        public bool ?EncuestaActiva { get; set; }
        public int ?IdTipoPersona { get; set; }
        public int ?UbicacionEncuesta { get; set; }
    }

    public class EncuestaAsincronicaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int IdCapituloProgramaCapacitacion { get; set; }
        public int UbicacionEncuesta { get; set; }
        public int IdExamen { get; set; }
        public bool EncuestaObligatoria {get;set;}
        public bool EncuestaActiva { get; set; }
        public int IdTipoPersona { get; set; }
        public string Usuario { get; set; }
    }

    public class EncuestaAsincronicaEntradaDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string Usuario { get; set; }
        public int? Version { get; set; }
        public int? IdTipoEncuesta { get; set; }
        public int? IdModalidadCurso { get; set; }
    }

    public class PreguntaExamenAsincronicaDTO
    {
        public int ?Id { get; set; } 
        public int ?IdPregunta { get; set; }
        public int ?IdExamen { get; set; }
        public string Usuario { get; set; }
    }

    public class IdExamenEncuestaDTO
    {
        public int? Id { get; set; }
    }
}
