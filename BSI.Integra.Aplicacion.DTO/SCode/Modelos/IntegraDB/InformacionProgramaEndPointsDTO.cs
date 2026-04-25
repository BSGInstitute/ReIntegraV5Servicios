using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class CargarInformacionProgramaInversionRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<InversionVersionDTO> Inversion { get; set; }
        public string Error { get; set; }
    }
    public class InversionVersionDTO
    {
        public string Version { get; set; }
        public string PrecioContado { get; set; }
        public string PrecioCredito { get; set; }
    }
    public class CargarInformacionProgramaPresentacionRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<string> Presentacion { get; set; }
        public string Error { get; set; }
    }
    public class CargarInformacionProgramaPublicoObjetivoRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<string> PublicoObjetivo { get; set; }
        public string Error { get; set; }
    }

    public class CargarInformacionProgramaDuracionHorariosRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<DuracionHorarioItemDTO> DuracionHorarios { get; set; }
        public string Error { get; set; }
    }
    public class DuracionHorarioItemDTO
    {
        public string Modalidad { get; set; }
        public List<string> Horario { get; set; }
    }
    public class CargarInformacionProgramaPrerrequisitosRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<string> Prerrequisitos { get; set; }
        public string Error { get; set; }
    }
    public class DetalleSeccionContenidoDTO
    {
        public string Contenido { get; set; }
    }
    public class CargarInformacionProgramaExpositoresRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<Expositor2DTO> Expositores { get; set; }
        public string Error { get; set; }
    }
    public class Expositor2DTO
    {
        public string nombre { get; set; }
        public List<string> descripcion { get; set; }
    }
    public class CargarInformacionProgramaEstructuraCurricularRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public object EstructuraCurricular { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Nota { get; set; }
        public string Error { get; set; }
    }

    public class EstructuraCurricularDTO
    {
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int IdSeccionTipoDetalle_PW { get; set; }
        public int NumeroFila { get; set; }
        public int OrdenWeb { get; set; }
        public int Orden { get; set; }
        public string Capitulo { get; set; }
        public List<string> Sesiones { get; set; }
    }

    public class CursoEstructuraDTO
    {
        public string NombreCurso { get; set; }
        public List<string> Capitulos { get; set; }
    }

    public class CapituloEstructuraDTO
    {
        public string Capitulo { get; set; }
        public List<string> Sesiones { get; set; } 
    }

    public class ResumenProgramaV3DTO
    {
        public int IdProgramaGeneral { get; set; }
        public string Categoria { get; set; }
        public string Tipo { get; set; }
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public string NombrePrograma { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class ObjetivosResponseDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<string> Objetivos { get; set; }
        public string Error { get; set; }
    }

    public class ObjetivosRawDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; } // Categoria = Curso / Programa
        public string ObjetivosHtml { get; set; }
    }
    public class BeneficioProgramaResponseDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<BeneficioVersionDTO> Beneficios { get; set; }
        public string Error { get; set; }
    }

    public class BeneficioVersionDTO
    {
        public string Version { get; set; }
        public List<string> Beneficios { get; set; }
        public string Nota { get; set; }
    }
    public class BeneficioRawDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public int? IdVersion { get; set; }
        public string Version { get; set; }
        public int idDocumento { get; set; }
        public string Beneficio { get; set; }
        public string Nota { get; set; }
    }
    public class CertificacionProgramaResponseDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<string> Certificacion { get; set; }
        public string Error { get; set; }
    }
    public class CertificacionRawDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public int idDocumento { get; set; }
        public string Beneficio { get; set; }
        public string Cabecera { get; set; }
        public string Nota { get; set; }
    }
    public class MetodologiaProgramaResponseDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public MetodologiaDTO Metodologia { get; set; }
        public string Error { get; set; }
    }

    public class MetodologiaDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public List<string> Componentes { get; set; }
    }

    public class MetodologiaRawDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public int idDocumento { get; set; }
        public string Titulo { get; set; }
        public string ContenidoMetodologia { get; set; }
    }
    public class PautasComplementariasProgramaResponseDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<string> PautasComplementarias { get; set; }
        public string Error { get; set; }
    }

    public class PautasComplementariasRawDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public int idDocumento { get; set; }
        public string Titulo { get; set; }
        public string ContenidoMetodologia { get; set; }
    }
    public class PerfilProfesionalClienteResponseDTO
    {
        public PerfilProfesionalClienteDTO PerfilProfesionalCliente { get; set; }
        public string Error { get; set; }
    }

    public class PerfilProfesionalClienteDTO
    {
        public string AreaFormacion { get; set; }
        public string Cargo { get; set; }
        public string PrincipalResponsabilidadProfesional { get; set; }
        public string AniosExperienciaProfesional { get; set; }
        public string Industria { get; set; }
        public string AreaTrabajo { get; set; }
        public string Empresa { get; set; }
        public string TamanioEmpresa { get; set; }
    }
    public class ProgramaDetalleResponseDTO
    {
        public string NombrePrograma { get; set; }
        public string Duracion { get; set; }
        public List<CursoDetalleDTO> Cursos { get; set; }
        public string Error { get; set; }
    }

    public class CursoDetalleResponseDTO
    {
        public string NombreCurso { get; set; }
        public string Duracion { get; set; }
        public string Presentacion { get; set; }
        public List<string> Objetivos { get; set; }
        public List<string> PublicoObjetivo { get; set; }
        public List<EstructuraCurricularDTO> EstructuraCurricular { get; set; }
        public List<string> MaterialCurso { get; set; }
        public List<string> Bibliografia { get; set; }
        public string Error { get; set; }
    }

    public class CursoDetalleDTO
    {
        public int Orden { get; set; }
        public string NombreCurso { get; set; }
        public string Duracion { get; set; }
        public string Presentacion { get; set; }
        public List<string> Objetivos { get; set; }
        public List<string> PublicoObjetivo { get; set; }
        public List<EstructuraCurricularDTO> EstructuraCurricular { get; set; }
        public List<string> MaterialCurso { get; set; }
        public List<string> Bibliografia { get; set; }
    }

    public class SeccionProgramaRawDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public int IdSeccion { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int OrdenWeb { get; set; }
    }

    public class CargarInformacionProgramaTodoRequestDTO
    {
        public int idCentroCosto { get; set; }
        public string codigoPais { get; set; }
    }

    public class CargarInformacionProgramaTodoRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public object Informacion { get; set; }
        public string Error { get; set; }
    }

    public class InformacionPrograma2DTO
    {
        public List<string> Presentacion { get; set; }
        public List<string> Objetivos { get; set; }
        public List<string> PublicoObjetivo { get; set; }
        public List<string> Prerrequisitos { get; set; }
        public List<CursoEstructuraDTO> EstructuraCurricular { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Nota { get; set; }
        public List<ModalidadDTO> Modalidades { get; set; }
        public List<DuracionHorarioItemDTO> DuracionHorarios { get; set; }
        public List<InversionVersionDTO> Inversion { get; set; }
        public List<BeneficioVersionDTO> Beneficios { get; set; }
        public List<string> Certificacion { get; set; }
        public MetodologiaDTO Metodologia { get; set; }
        public List<Expositor2DTO> Expositores { get; set; }
    }

    public class InformacionCursoDTO
    {
        public List<ModalidadDTO> Modalidades { get; set; }
        public List<InversionVersionDTO> Inversion { get; set; }
        public List<BeneficioVersionDTO> Beneficios { get; set; }
        public List<string> Presentacion { get; set; }
        public List<string> Objetivos { get; set; }
        public List<string> PublicoObjetivo { get; set; }
        public List<string> Prerrequisitos { get; set; }
        public List<CapituloEstructuraDTO> EstructuraCurricular { get; set; }
        [System.Text.Json.Serialization.JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Nota { get; set; }
        public List<DuracionHorarioItemDTO> DuracionHorarios { get; set; }
        public List<string> Certificacion { get; set; }
        public MetodologiaDTO Metodologia { get; set; }
        public List<Expositor2DTO> Expositores { get; set; }
        public List<string> PautasComplementarias { get; set; }
    }

    public class ModalidadDTO
    {
        public string Tipo { get; set; }
        public string CentroCosto { get; set; }
        public string FechaInicio { get; set; }
    }

    public class CursoHijoV2DTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Pg_titulo { get; set; }
        public string pw_duracion { get; set; }
        public int Orden { get; set; }
    }

    public class RegistroSeccionContenidoDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int IdPGeneral { get; set; }
        public int Orden { get; set; }
        public int NumeroFila { get; set; }
        public string NombreTitulo { get; set; } // "Capitulo", "Sesion", "SubSeccion", "Descripcion"
    }
    public class EstructuraCurricularv2DTO
    {
        public int Orden { get; set; }
        public string Capitulo { get; set; }
        public List<string> Sesiones { get; set; }
    }

    public class CursoDetallev2DTO
    {
        public int Orden { get; set; }
        public string NombreCurso { get; set; }
        public string Duracion { get; set; }
        public string Presentacion { get; set; }
        public List<string> Objetivos { get; set; }
        public List<string> PublicoObjetivo { get; set; }
        public List<EstructuraCurricularv2DTO> EstructuraCurricular { get; set; }
        public List<string> MaterialCurso { get; set; }
        public List<string> Bibliografia { get; set; }
    }

    public class ProgramaDetalleResponsev2DTO
    {
        public string NombrePrograma { get; set; }
        public string Duracion { get; set; }
        public List<CursoDetallev2DTO> Cursos { get; set; }
        public string Error { get; set; }
    }

    public class CursoDetalleResponsev2DTO
    {
        public string NombreCurso { get; set; }
        public string Duracion { get; set; }
        public string Presentacion { get; set; }
        public List<string> Objetivos { get; set; }
        public List<string> PublicoObjetivo { get; set; }
        public List<EstructuraCurricularv2DTO> EstructuraCurricular { get; set; }
        public List<string> MaterialCurso { get; set; }
        public List<string> Bibliografia { get; set; }
        public string Error { get; set; }
    }
    public class SilaboProgramav2DTO
    {
        public string NombrePrograma { get; set; }
        public string Duracion { get; set; }
        public List<SilaboCursov2DTO> Cursos { get; set; }
        public string Error { get; set; }
    }

    public class SilaboCursov2DTO
    {
        public int Orden { get; set; }
        public string NombreCurso { get; set; }
        public string Duracion { get; set; }
        public string Presentacion { get; set; }
        public List<string> Objetivos { get; set; }
        public List<string> PublicoObjetivo { get; set; }
        public List<EstructuraCurricularv2DTO> EstructuraCurricular { get; set; }
        public List<string> MaterialCurso { get; set; }
        public List<string> Bibliografia { get; set; }
    }
    public class PgeneralDocumentoSeccionv2DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string pw_duracion { get; set; }
        public List<SeccionDocumentov2DTO> ListaSeccion { get; set; }
        public List<ProgramaGeneralSeccionAnexosHTMLv2DTO> ListaSeccionV2 { get; set; }
    }

    public class PgeneralHijov2DTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string pw_duracion { get; set; }
        public int Orden { get; set; }
    }

    public class SeccionDocumentov2DTO
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
    public class EstructuraCurricularFlatDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int IdPGeneral { get; set; }
        public int Orden { get; set; }
        public int NumeroFila { get; set; }
        public string NombreTitulo { get; set; }
    }
    public class ProgramaGeneralSeccionAnexosHTMLv2DTO
    {
        public string? Seccion { get; set; }
        public string? Contenido { get; set; }
    }

    public class ModalidadProgramaSimpleDTO
    {
        public string Tipo { get; set; }
        public string CentroCosto { get; set; }
        public string FechaHoraInicio { get; set; }
    }

    public class ModalidadesResponseDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<ModalidadProgramaSimpleDTO> Modalidades { get; set; }
        public string? Error { get; set; }
    }
    public class PGeneralAtributosPrincipalesv2DTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public string PwImgPortada { get; set; }
        public string PwDuracion { get; set; }
        public int IdCategoria { get; set; }
        public string EsProgramaOCurso { get; set; }
    }
    public class PEspecificoPorIdPGeneralV2DTO
    {
        public int Id { get; set; }
        public int IdCentroCosto { get; set; }
        public string Nombre { get; set; }
        public string CentroCosto { get; set; }
        public string Ciudad { get; set; }
        public string Tipo { get; set; }
        public string Duracion { get; set; }
        public int? EstadoPId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public string FechaInicioTexto { get; set; }
        public string Frecuencia { get; set; }
        public int IdCategoria { get; set; }
        public string CodigoBanco { get; set; }
    }
    public class ModalidadesProgramaResponseDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<ModalidadDTO> Modalidades { get; set; }
        public string Error { get; set; }
    }

    public class Modalidadv2DTO
    {
        public string Tipo { get; set; }
        public string CentroCosto { get; set; }
        public string FechaInicio { get; set; }
    }
}
