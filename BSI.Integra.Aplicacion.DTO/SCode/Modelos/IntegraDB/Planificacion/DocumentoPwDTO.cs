using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class DocumentoPwDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPlantillaPw { get; set; }
        public int EstadoFlujo { get; set; }
        public bool Asignado { get; set; }
    }
    public class DocumentoAsociadoProgramaDTO
    {
        public List<PGeneralDocumentoPwDTO> PGeneralDocumentoPws { get; set; }
        public int IdPGeneral { get; set; }
    }
    public class CompuestoDocumentoPwDTO
    {
        public DocumentoPwDTO ObjetoDocumento { get; set; }
        public List<DocumentoSeccionPwFiltroDTO> Lista { get; set; }
        public List<DocumentoPwVersionesDTO>? ListaIntroduccionBeneficios { get; set; }
        public SeccionModalidadHorarioDTO? SeccionModalidadHorario { get; set; }
        public SeccionDuracionDTO? SeccionDuracion { get; set; }
        public SeccionFechaInicioDTO? SeccionFechaInicio { get; set; }
        public SeccionNotasDTO? SeccionNotas { get; set; }
        //public List<RevisionNivelPwFiltroIdPlantillaDTO> ListaRevision { get; set; }
        //public List<PGeneralCriterioEvaluacionDTO> ListaCriterioEvaluacionPresencial { get; set; }
        //public List<PGeneralCriterioEvaluacionDTO> ListaCriterioEvaluacionOnline { get; set; }
        //public List<PGeneralCriterioEvaluacionDTO> ListaCriterioEvaluacionAOnline { get; set; }

    }
    public class SeccionModalidadHorarioDTO
    {
        public int IdDocumentoPw { get; set; }
        public string? Introduccion { get; set; }

        public List<ModalidadHorarioDTO> Modalidades { get; set; } = new();

        public List<int> ModalidadesEliminadas { get; set; } = new();
        public List<int> DetallesEliminados { get; set; } = new();
    }

    public class ModalidadHorarioDTO
    {
        public int Id { get; set; }

        public int? IdModalidad { get; set; }

        public string? SubTitulo { get; set; }
        public string? Descripcion { get; set; }

        public List<ModalidadHorarioDetalleDTO> Detalles { get; set; } = new();
    }

    public class ModalidadHorarioDetalleDTO
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public string? Tipo { get; set; } // "HORA" | "BENEFICIO"

        // det.valor en tu UI es el id del comboHoras => int
        public int? IdPais { get; set; }

        // det.valorTexto en tu UI
        public string? Beneficio { get; set; }

        // Si quieres enviar etiqueta, agrégala; si no, ignórala
        public string? Etiqueta { get; set; }
    }
    public class SeccionDuracionDTO
    {
        public int IdDocumentoPw { get; set; }
        public string? Titulo { get; set; }
        public string? Introduccion { get; set; }
        public string? PieDePagina { get; set; }
        public List<DuracionDetalleDTO> Detalles { get; set; } = new();
        public List<int> DetallesEliminados { get; set; } = new();
    }

    public class DuracionDetalleDTO
    {
        public int Id { get; set; }
        public int? IdVersionPrograma { get; set; }
        public string? Meses { get; set; }
        public string? Horas { get; set; }
    }

    public class SeccionFechaInicioDTO
    {
        public int IdDocumentoPw { get; set; }
        public bool MostrarEnLaWeb { get; set; }
        public string? Titulo { get; set; }
        public string? SubTitulo { get; set; }
        public List<FechaInicioPaisDTO> Paises { get; set; } = new();
        public List<int> PaisesEliminados { get; set; } = new();
        public List<int> DetallesEliminados { get; set; } = new();
    }

    public class FechaInicioPaisDTO
    {
        public int Id { get; set; }
        public int? IdPais { get; set; }
        public List<FechaInicioDetalleDTO> Detalles { get; set; } = new();
    }

    public class FechaInicioDetalleDTO
    {
        public int Id { get; set; }
        public int? IdModo { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Horario { get; set; }
    }

    public class CompuestoDocumentoDTO
    {
        public DocumentoPwDTO ObjetoDocumento { get; set; }
        public List<SeccionPwFiltroPlantillaPwDTO> Lista { get; set; }

        public List<VersionDocumentoBeneficioDTO>? ListaIntroduccionBeneficios { get; set; }

        public SeccionModalidadHorarioDTO? SeccionModalidadHorario { get; set; }
        public SeccionDuracionDTO? SeccionDuracion { get; set; }
        public SeccionFechaInicioDTO? SeccionFechaInicio { get; set; }
        public SeccionNotasDTO? SeccionNotas { get; set; }
    }
    public class VersionDocumentoBeneficioDTO
    {
        public int IdVersionPrograma { get; set; }
        public string? Introduccion { get; set; }
    }
    public class SeccionNotasDTO
    {
        public int IdDocumentoPw { get; set; }
        public bool MostrarEnLaWeb { get; set; }
        public List<NotaDTOV2> Notas { get; set; } = new();
        public List<int> NotasEliminadas { get; set; } = new();
        public List<int> DetallesEliminados { get; set; } = new();
    }
    public class NotaDTOV2
    {
        public int Id { get; set; }
        public int? IdNotaTipo { get; set; }
        public int? IdPGeneral { get; set; }
        public string? Descripcion { get; set; }
        public List<NotaDetalleDTO> Detalles { get; set; } = new();
    }

    public class NotaDetalleDTO
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public string? InformacionExtra { get; set; }
        public int? IdPais { get; set; }
    }


    public class RevisionNivelPwFiltroIdPlantillaDTO
    {
        public int Id { get; set; }
        public int IdPlantillaPw { get; set; }
        public int IdRevisionNivelPw { get; set; }
        public int Identificador { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public int IdTipoRevisionPw { get; set; }
        public int IdDocumento { get; set; }
        public int Cambio { get; set; }
    }


    public class CursoHijoDuracionPdusDTO
    {
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public string CodigoPartner { get; set; }
        public int? CantidadPdus { get; set; }
    }


    public class ModalidadPortalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Propiedad { get; set; }

    }

    public class DocumentoPWModalidadVM
    {
        public int IdDocumentoPW { get; set; }
        public int IdDocumentoPWModalidadIntroduccion { get; set; }
        public string Introduccion { get; set; }

        public int IdDocumentoPWModalidad { get; set; }
        public int IdModalidadPortal { get; set; }
        public string SubTitulo { get; set; }
        public string Descripcion { get; set; }

        public int? IdDocumentoPWModalidadDetalle { get; set; }
        public int? Orden { get; set; }
        public string Tipo { get; set; }
        public int? IdPais { get; set; }
        public string Beneficio { get; set; }
    }

    public class SeccionModalidadHorarioResponseDTO
    {
        public int IdDocumentoPw { get; set; }
        public string? Introduccion { get; set; }
        public List<ModalidadHorarioResponseDTO> Modalidades { get; set; } = new();
        public List<int> ModalidadesEliminadas { get; set; } = new();
        public List<int> DetallesEliminados { get; set; } = new();
    }

    public class ModalidadHorarioResponseDTO
    {
        public int Id { get; set; }
        public int? IdModalidad { get; set; }
        public string? SubTitulo { get; set; }
        public string? Descripcion { get; set; }
        public List<ModalidadHorarioDetalleResponseDTO> Detalles { get; set; } = new();
    }

    public class ModalidadHorarioDetalleResponseDTO
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public string? Tipo { get; set; }
        public int? IdPais { get; set; }
        public string? Beneficio { get; set; }
    }
    public class DocumentoPWModalidadRowVM
    {
        public int IdDocumento_PW { get; set; }
        public int IdDocumentoPWModalidadIntroduccion { get; set; }
        public string? Introduccion { get; set; }

        public int IdDocumentoPWModalidad { get; set; }
        public int? IdModalidadPortal { get; set; }
        public string? SubTitulo { get; set; }
        public string? Descripcion { get; set; }

        public int? IdDocumentoPWModalidadDetalle { get; set; }
        public int? Orden { get; set; }
        public string? Tipo { get; set; }
        public int? IdPais { get; set; }
        public string? Beneficio { get; set; }
    }


    public class DocumentoPWDuracionRowVM
    {
        public int IdDocumentoPW { get; set; }

        public int IdDocumentoPWDuracion { get; set; }
        public string? Titulo { get; set; }
        public string? Introduccion { get; set; }
        public string? PieDePagina { get; set; }

        public int? IdDocumentoPWDuracionDetalle { get; set; }
        public int? IdVersionPrograma { get; set; }
        public string? DetalleMes { get; set; }
        public string? DetalleHora { get; set; }
    }

    public class DocumentoPWFechaInicioRowDTO
    {
        public bool MostrarEnLaWeb { get; set; }
        public string? Titulo { get; set; }
        public string? SubTitulo { get; set; }

        public int IdDocumentoPWFechaInicio { get; set; }
        public int? IdPais { get; set; }

        public int? IdDetalle { get; set; }
        public int? IdModo { get; set; }

        public DateTime? Fecha { get; set; }
        public string? Horario { get; set; }
    }

    public class DocumentoPWNotasRowDTO
    {
        public int IdDocumentoPw { get; set; }
        public bool MostrarWeb { get; set; }

        public int IdDocumentoPWNota { get; set; }
        public int? IdDocumentoPWNotaTipo { get; set; }
        public int? IdPGeneral { get; set; }
        public string? Descripcion { get; set; }

        public int? IdDocumentoPWNotaDetalle { get; set; }
        public int? Orden { get; set; }
        public string? InformacionExtra { get; set; }
        public int? IdPais { get; set; }
    }
}
