using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public List<string> Presentacion { get; set; } // array de párrafos
        public string Error { get; set; }
    }
    public class CargarInformacionProgramaPublicoObjetivoRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public List<string> PublicoObjetivo { get; set; } // array de párrafos
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
        public List<ExpositorDTO> Expositores { get; set; }
        public string Error { get; set; }
    }
    public class ExpositorDTO
    {
        public string nombre { get; set; }
        public List<string> descripcion { get; set; }
    }

    //public class CapituloEstructuraDTO
    //{
    //    public string Capitulo { get; set; }
    //    public List<string> Sesiones { get; set; }
    //}

    //public class CursoEstructuraDTO
    //{
    //    public string NombreCurso { get; set; }
    //    public List<string> Capitulos { get; set; }
    //}

    //public class CargarInformacionProgramaEstructuraCurricularRespuestaDTO
    //{
    //    public int IdPGeneral { get; set; }
    //    public string EsProgramaOCurso { get; set; }
    //    public List<CapituloEstructuraDTO> Capitulos { get; set; }
    //    public List<CursoEstructuraDTO> Cursos { get; set; }
    //    public string Error { get; set; }
    //}

    public class CargarInformacionProgramaEstructuraCurricularRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public object EstructuraCurricular { get; set; } // Será una lista de CursoEstructuraDTO o CapituloEstructuraDTO
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

    //lolo

    public class ResumenProgramaDTO
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

    public class ProgramaDetalleResponseV2DTO
    {
        public string NombrePrograma { get; set; }
        public string Duracion { get; set; }
        public List<CursoDetalleDTO> Cursos { get; set; }
        public string Error { get; set; }
    }

    public class CursoDetalleResponseV2DTO
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

    public class CursoDetalleV2DTO
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

    public class EstructuraCurricularV2DTO
    {
        public int Orden { get; set; }
        public string Capitulo { get; set; }
        public List<string> Sesiones { get; set; }
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

    // *********************************************************************
    // * DTO Principal de Respuesta para CargarInformacionProgramaTodoAsync *
    // *********************************************************************

    public class CargarInformacionProgramaTodoRequestDTO
    {
        public int idCentroCosto { get; set; }
        public string codigoPais { get; set; }
    }

    // *******************************************************
    // * DTOs PARA LA SALIDA DEL ENDPOINT (RESPONSE OBJECT)  *
    // *******************************************************

    public class CargarInformacionProgramaTodoRespuestaDTO
    {
        public int IdPGeneral { get; set; }
        public string EsProgramaOCurso { get; set; }
        public object Informacion { get; set; } // Será InformacionProgramaDTO o InformacionCursoDTO
        public string Error { get; set; }
    }

    public class InformacionProgramaDTO
    {
        public List<string> Presentacion { get; set; }
        public List<string> Objetivos { get; set; }
        public List<string> PublicoObjetivo { get; set; }
        public List<string> Prerrequisitos { get; set; }
        public List<CursoEstructuraDTO> EstructuraCurricular { get; set; }
        public List<ModalidadDTO> Modalidades { get; set; }
        public List<DuracionHorarioItemDTO> DuracionHorarios { get; set; }
        public List<InversionVersionDTO> Inversion { get; set; }
        public List<BeneficioVersionDTO> Beneficios { get; set; }
        public List<string> Certificacion { get; set; }
        public MetodologiaDTO Metodologia { get; set; }
        public List<ExpositorDTO> Expositores { get; set; }
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
        public List<DuracionHorarioItemDTO> DuracionHorarios { get; set; }
        public List<string> Certificacion { get; set; }
        public MetodologiaDTO Metodologia { get; set; }
        public List<ExpositorDTO> Expositores { get; set; }
        public List<string> PautasComplementarias { get; set; }
    }

    public class ModalidadDTO
    {
        public string Tipo { get; set; }
        public string CentroCosto { get; set; }
        public string FechaInicio { get; set; }
    }
}
