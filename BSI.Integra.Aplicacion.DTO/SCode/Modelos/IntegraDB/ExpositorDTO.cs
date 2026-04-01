using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ExpositorDTO
    {
        public int Id { get; set; }
        public int IdTipoDocumento { get; set; }
        [StringLength(50)]
        public string? NroDocumento { get; set; }
        [StringLength(300)]
        public string? PrimerNombre { get; set; }
        [StringLength(300)]
        public string? SegundoNombre { get; set; }
        [StringLength(300)]
        public string? ApellidoPaterno { get; set; }
        [StringLength(300)]
        public string? ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPaisProcedencia { get; set; }
        public int? IdCiudadProcedencia { get; set; }
        public int? IdReferidoPor { get; set; }
        [StringLength(50)]
        public string? TelfCelular1 { get; set; }
        [StringLength(50)]
        public string? TelfCelular2 { get; set; }
        [StringLength(50)]
        public string? TelfCelular3 { get; set; }
        [StringLength(100)]
        public string? Email1 { get; set; }
        [StringLength(100)]
        public string? Email2 { get; set; }
        [StringLength(100)]
        public string? Email3 { get; set; }
        [StringLength(300)]
        public string? Domicilio { get; set; }
        public int? IdPaisDomicilio { get; set; }
        public int? IdCiudadDomicilio { get; set; }
        [StringLength(300)]
        public string? LugarTrabajo { get; set; }
        public int? IdPaisLugarTrabajo { get; set; }
        public int? IdCiudadLugarTrabajo { get; set; }
        [StringLength(300)]
        public string? AsistenteNombre { get; set; }
        [StringLength(50)]
        public string? AsistenteTelefono { get; set; }
        [StringLength(50)]
        public string? AsistenteCelular { get; set; }
        public string? HojaVidaResumidaPerfil { get; set; }
        public string? HojaVidaResumidaSpeech { get; set; }
        public string? FormacionAcademica { get; set; }
        public string? ExperienciaProfesional { get; set; }
        public string? Publicaciones { get; set; }
        public string? PremiosDistinciones { get; set; }
        public string? OtraInformacion { get; set; }
        public bool? EsPersonaValida { get; set; }
        public int? IdPersonalAsignado { get; set; }
        [StringLength(150)]
        public string? FotoDocente { get; set; }
        [StringLength(150)]
        public string? UrlFotoDocente { get; set; }
        public bool? DocenteInstituto { get; set; }
    }
    public class AgendaExpositorDTO
    {
        public string Nombres { get; set; }
        public string HojaVida { get; set; }
    }
    public class ComboModuloExpositorDTO
    {
        public IEnumerable<TipoDocumentoDTO> TipoDocumentos { get; set; }
        public IEnumerable<ComboDTO> Coordinadores { get; set; }
        public IEnumerable<ComboDTO> Paises { get; set; }
        public IEnumerable<CiudadAlternoDTO> Ciudades { get; set; }
        public IEnumerable<ComboDTO> Expositores { get; set; }
    }
    public class BuscarExpositorContactoDTO
    {
        public string? Email { get; set; }
        public string? Celular { get; set; }
        public string? NroDocumento { get; set; }
    }

    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/ObtenerTodoArea
    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/ObtenerTodoSubArea
    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/ObtenerTodoCategoriaPrograma
    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/ObtenerTodoParametroSeo
    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/ObtenerTodo
    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/ObtenerTodoTagPorPrograma/588
    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/AsociarTag
    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/Desasociar/588/29
    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/ActualizarTag
    //https://integrav4-servicios.bsginstitute.com/api/AsociarProgramaTag/EliminarTag
}