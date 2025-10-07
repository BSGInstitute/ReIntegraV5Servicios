namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DocumentoSeccionPwDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int IdDocumentoPw { get; set; }
        public int IdSeccionPw { get; set; }
        public bool VisibleWeb { get; set; }
        public int? ZonaWeb { get; set; }
        public int? OrdenWeb { get; set; }
        public int? IdSeccionTipoDetalle_Pw { get; set; }
        public int? NumeroFila { get; set; }
        public string? Cabecera { get; set; }
        public string? PiePagina { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class DocumentoSeccionPwComboDTO
    {
        public int Id { get; set; }
        public string DocumentoPw { get; set; } = null!;
        public string Titulo { get; set; } = null!;

    }
    public class SeccionDocumentoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int? IdPGeneral { get; set; }
        public int? OrdenWeb { get; set; }
        public int Orden { get; set; }
        public int? NumeroFila { get; set; }
        public string NombreTitulo { get; set; }
    }
    public class ProgramaGeneralSeccionAnexosHTMLDTO
    {
        public string? Seccion { get; set; }
        public string? Contenido { get; set; }
    }
    public class ProgramaGeneralSeccionDocumentoDTO
    {
        public string Seccion { get; set; }
        public List<ProgramaGeneralSeccionDocumentoDetalleDTO> DetalleSeccion { get; set; }
    }
    public class ProgramaGeneralSeccionDocumentoDetalleDTO
    {
        public string Titulo { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public List<string> DetalleContenido { get; set; }
    }
    public class ProgramaGeneralDoumentoDTO
    {
        public virtual List<RegistroListaSeccionesDocumentoDTO> ListaSeccionesContenidosDocumento { get; set; }
        public virtual List<RegistroListaSeccionesDocumentoDTO> ListaSeccionesContenidosDocumentoEstructura { get; set; }
        public bool EsProgramaPadre { get; set; }
    }
    public class RegistroListaSeccionesDocumentoDTO
    {
        public int IdPGeneral { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int? IdSeccionTipoDetalle_PW { get; set; }
        public int? NumeroFila { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public int? OrdenWeb { get; set; }
        public string NombreCurso { get; set; }
    }
    public class ProgramaGeneralEstructuraAgrupadoDTO
    {
        public string Seccion { get; set; }
        public string Titulo { get; set; }
        public List<ProgramaGeneralEstructuraDetalleDTO> DetalleContenido { get; set; }
    }
    public class ProgramaGeneralEstructuraDetalleDTO
    {
        public string Contenido { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
    }
    public class PGeneralDocumentoSeccionDTO
    {
        public string Titulo { get; set; } = null!;
        public string? Contenido { get; set; }
        public int? IdSeccionTipoDetallePw { get; set; }
        public int? NumeroFila { get; set; }
        public string? Cabecera { get; set; }
        public string? PiePagina { get; set; }
        public int? OrdenWeb { get; set; }
    }
    public class ProgramaExpositoresDTO
    {
        public int Id { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombrePais { get; set; }
        public string HojaVidaResumidaPerfil { get; set; }
        public int? IdPGeneral { get; set; }
    }
    public class SesionSubSesionPreguntaInteractivaDTO
    {
        public int IdPGeneral { get; set; }
        public string Contenido { get; set; }
        public int NumeroFila { get; set; }
    }
    public class EstructuraProgramaCapituloDTO
    {
        public string Contenido { get; set; }
        public int? NroCapitulo { get; set; }
    }
    public class DocumentoSeccionPwFiltroAgrupadoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPW { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public int IdDocumentoPW { get; set; }
        public int IdSeccionPW { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public List<SubSeccionTipoDetallePwDTO> ListaSubSeccionesPw { get; set; }
    }
    public class SubSeccionTipoDetallePwDTO
    {
        public int? IdSeccionTipoDetallePw { get; set; }
        public string? NombreSubSeccion { get; set; }
        public int? IdSubSeccionTipoContenido { get; set; }
        public string? ContenidoSubSeccion { get; set; }
        public int? NumeroFila { get; set; }
    }
    public class DocumentoSeccionPwFiltroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public bool VisibleWeb { get; set; }
        public int ZonaWeb { get; set; }
        public int OrdenEeb { get; set; }
        public string Contenido { get; set; }
        public int IdPlantillaPw { get; set; }
        public int Posicion { get; set; }
        public int Tipo { get; set; }
        public string Cabecera { get; set; }
        public string PiePagina { get; set; }
        public int IdDocumentoPW { get; set; }
        public int IdSeccionPW { get; set; }
        public int? IdSeccionTipoContenido { get; set; }
        public string NombreSeccionTipoContenido { get; set; }
        public int? IdSeccionTipoDetallePw { get; set; }
        public string? NombreSubSeccion { get; set; }
        public int? IdSubSeccionTipoContenido { get; set; }
        public string? ContenidoSubSeccion { get; set; }
        public int? NumeroFila { get; set; }
        public List<listaGridListaSeccionesDTO> listaGridListaSecciones { get; set; }
    }

    public class VersionIntroduccionDTO
    {
        public int IdVersionPrograma { get; set; }
        public string? Introduccion { get; set; }
        public int IdDocumentoPw { get; set; }
    }
    public class listaGridListaSeccionesDTO
    {
        public string Clave { get; set; }
        public string Valor { get; set; }
        public int? NumeroFila { get; set; }
    }


    public class PresentacionProgramadto
    {
        public int IdPGeneral { get; set; }
        public string Titulo { get; set; }
        public string Cabecera { get; set; }
        public string Solucion { get; set; }
    }
}