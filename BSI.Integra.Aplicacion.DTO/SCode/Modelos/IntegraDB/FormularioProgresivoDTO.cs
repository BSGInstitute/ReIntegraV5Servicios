namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FormularioProgresivoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class FormularioProgresivoEntradaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int Tipo { get; set; }
        public bool? Activado { get; set; }
        public int? IdFormularioProgresivoInicial { get; set; }
        public int? CondicionMostrar { get; set; }
        public int? TiempoProgramasPublicidad { get; set; }
        public bool? Titulo { get; set; }
        public string? TituloTexto { get; set; }
        public bool? CabeceraMensajeSup { get; set; }
        public string? CabeceraMensajeSupTexto { get; set; }
        public bool? CabeceraMensaje { get; set; }
        public bool? CabeceraMensajeIndexCurso { get; set; }
        public string? CabeceraMensajeTexto { get; set; }
        public string? CabeceraMensajeTextoCurso { get; set; }
        public bool? CabeceraMensajeBordes { get; set; }
        public bool? CabeceraMensajeInf { get; set; }
        public bool? CabeceraMensajeInfIndexCurso { get; set; }
        public string? CabeceraMensajeInfTexto { get; set; }
        public string? CabeceraMensajeInfTextoCurso { get; set; }
        public bool? CabeceraBoton { get; set; }
        public string? CabeceraBotonTexto { get; set; }
        public int? CabeceraBotonAccion { get; set; }
        public bool? CuerpoMensajeSup { get; set; }
        public string? CuerpoMensajeSupTexto { get; set; }
        public bool? CuerpoCorreo { get; set; }
        public int? CuerpoCorreoOrden { get; set; }
        public bool? CuerpoCorreoObl { get; set; }
        public bool? CuerpoNombres { get; set; }
        public int? CuerpoNombresOrden { get; set; }
        public bool? CuerpoNombresObl { get; set; }
        public bool? CuerpoApellidos { get; set; }
        public int? CuerpoApellidosOrden { get; set; }
        public bool? CuerpoApellidosObl { get; set; }
        public bool? CuerpoPais { get; set; }
        public int? CuerpoPaisOrden { get; set; }
        public bool? CuerpoPaisObl { get; set; }
        public bool? CuerpoTelefono { get; set; }
        public int? CuerpoTelefonoOrden { get; set; }
        public bool? CuerpoTelefonoObl { get; set; }
        public bool? CuerpoCargo { get; set; }
        public int? CuerpoCargoOrden { get; set; }
        public bool? CuerpoCargoObl { get; set; }
        public bool? CuerpoAreaFormacion { get; set; }
        public int? CuerpoAreaFormacionOrden { get; set; }
        public bool? CuerpoAreaFormacionObl { get; set; }
        public bool? CuerpoAreaTrabajo { get; set; }
        public int? CuerpoAreaTrabajoOrden { get; set; }
        public bool? CuerpoAreaTrabajoObl { get; set; }
        public bool? CuerpoIndustria { get; set; }
        public int? CuerpoIndustriaOrden { get; set; }
        public bool? CuerpoIndustriaObl { get; set; }
        public bool? CuerpoMensajeInf { get; set; }
        public string? CuerpoMensajeInfTexto { get; set; }
        public bool? CuerpoBoton { get; set; }
        public string? CuerpoBotonTexto { get; set; }
        public int? CuerpoBotonAccion { get; set; }
        public bool? Pie { get; set; }
        public string? PieTexto { get; set; }
        public bool? Boton { get; set; }
        public string? BotonTexto { get; set; }
        public int? BotonAccion { get; set; }
        public string Usuario { get; set; }
        public int? TiempoProgramasOrganico { get; set; }
        public int? TiempoBlogsWhite { get; set; }
        public int? TiempoIndexTags { get; set; }
        public string? CabeceraMensajeTextoWhitepaper { get; set; }
        public string? CabeceraMensajeInfTextoWhitepaper { get; set; }
        public bool? CabeceraMensajeSupIndexCurso { get; set; }
        public string? CabeceraMensajeSupTextoCurso { get; set; }
        public string? CabeceraMensajeSupTextoWhitepaper { get; set; }
        public bool? CuerpoMensajeSupIndexCurso { get; set; }
        public string? CuerpoMensajeSupTextoCurso { get; set; }
        public string? CuerpoMensajeSupTextoWhitepaper { get; set; }
        public List<FormularioProgresivoConfiguracionBotonEntradaDTO>? ConfiguracionBoton { get; set; }
    }

    public class ActualizaActivadoDTO
    {
        public int Id { get; set; }
        public bool Activado { get; set; }
        public string Usuario { get; set; }
    }

    public class FormularioProgresivoInicialDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

}
