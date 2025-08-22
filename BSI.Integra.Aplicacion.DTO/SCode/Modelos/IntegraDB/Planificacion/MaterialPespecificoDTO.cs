using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class MaterialPespecificoDTO
    {
        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public int IdMaterialTipo { get; set; }
        public int GrupoEdicion { get; set; }
        public int GrupoEdicionOrden { get; set; }
        public int? IdFur { get; set; }
    }
    public class MaterialPespecificoCombosDTO
    {
        public IEnumerable<AreaCapacitacionFiltroDTO> ListaArea { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> ListaSubArea { get; set; }
        public IEnumerable<PGeneralSubAreaFiltroDTO> ListaProgramaGeneral { get; set; }
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ListaProgramaEspecifico { get; set; }
        public IEnumerable<PEspecificoGrupoDTO>? ListaPEspecificoCurso { get; set; }
        public IEnumerable<ComboDTO>? ListaGrupo { get; set; }
        public IEnumerable<ComboDTO> ListaEstadoPEspecifico { get; set; }
        public IEnumerable<ComboDTO> ListaCiudadBS { get; set; }
        public IEnumerable<ComboDTO> ListaModalidad { get; set; }
        public IEnumerable<ComboDTO> ListaMaterialEstado { get; set; }
        public IEnumerable<ComboDTO> ListaMaterialTipo { get; set; }
        public IEnumerable<ComboDTO> ListaMaterialVersion { get; set; }


    }
    public class DocumentosOportunidadDTO
    {
        public int IdAlumno { get; set; }
        public int IdOportunidad { get; set; }
        public int IdClasificacionPersona { get; set; }
        public string? ComentarioSubida { get; set; }
        public string NombreUsuario { get; set; }
        public int Tipo { get; set; }
        public IList<IFormFile>? Files { get; set; }
    }
    public class FiltroMaterialDTO
    {
        public int[]? IdsAreas { get; set; }
        public int[]? IdsSubAreas { get; set; }
        public int[]? IdsProgramasGenerales { get; set; }
        public int[]? IdsProgramasEspecificoPadreIndividual { get; set; }
        public int[]? IdsProgramasEspecificoCurso { get; set; }
        public int[]? IdsGrupos { get; set; }
        public int[]? IdsEstadosPEspecifico { get; set; }
        public int[]? IdsCiudades { get; set; }
        public int[]? IdsModalidades { get; set; }
        public int[]? IdsEstadosMateriales { get; set; }
    }
    public class ResultadoMaterialPEspecificoDetalleDTO
    {
        public int IdMaterialPEspecifico { get; set; }
        public int IdMaterialPEspecificoDetalle { get; set; }
        public int IdPEspecificoPadreIndividual { get; set; }
        public string NombrePEspecificoPadreIndividual { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public DateTime? FechaInicioCurso { get; set; }
        public int Grupo { get; set; }
        public int GrupoEdicion { get; set; }
        public int GrupoEdicionOrden { get; set; }
        public int IdMaterialTipo { get; set; }
        public string NombreMaterialTipo { get; set; }
        public int IdMaterialVersion { get; set; }
        public string NombreMaterialVersion { get; set; }
        public int IdMaterialEstado { get; set; }
        public string NombreMaterialEstado { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlArchivo { get; set; }
        public string UsuarioSubida { get; set; }
        public DateTime? FechaSubida { get; set; }
        public string ComentarioSubida { get; set; }
        public string UsuarioAprobacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public List<ComboDTO> ListaMaterialAccion { get; set; }
        public bool EsPrimerMaterial { get; set; }
        public bool GrupoEdicionTieneFurAsociado { get; set; }
        public bool EnviadoAProveedorImpresion { get; set; }
        public bool DebeEnviarAProveedorImpresion { get; set; }
        public bool TodasVersionesMaterialGrupoEdicionAprobadas { get; set; }
        public bool TieneFurAsociado { get; set; }
        public bool EsMaterialEnviado { get; set; }
        public bool DebeEnviarAAlumnos { get; set; }
        public string UsuarioEnvio { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public ResultadoMaterialPEspecificoDetalleDTO()
        {
            ListaMaterialAccion = new List<ComboDTO>();
        }
    }
    public class ComboMaterialPespecificoDTO
    {
        public List<ComboDTO> ObtenerCiudadBs { get; set; }
        public List<ComboDTO> ObtenerComboEstado { get; set; }
        public List<ComboDTO> ObtenerComboModalidad { get; set; }
        public List<AreaCapacitacionFiltroDTO> ObtenerFiltroArea { get; set; }
        public List<SubAreaCapacitacionFiltroDTO> ObtenerFiltroSubArea { get; set; }
        public List<ProgramaGeneralSubAreaFiltroDTO> ObtenerProgramaGeneralPadre { get; set; }
        public List<PEspecificoProgramaGeneralFiltroDTO> ObtenerProgramasEspecificosPadres { get; set; }
        public List<CentroCostoProgramaEspecificoFiltroDTO> ObtenerCentroCostoPadres { get; set; }
        public List<ComboDTO> ObtenerComboMaterial { get; set; }
    }

    public class MaterialPEspecificoDetalleEnvioProveedorDTO
    {
        public string NombreProveedor { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string DireccionEntrega { get; set; }
        public decimal Cantidad { get; set; }
        public string NombreMaterialTipo { get; set; }
        public string UrlArchivo { get; set; }
        public int Grupo { get; set; }
        public string NombrePEspecifico { get; set; }
        public string NombreCentroCosto { get; set; }
    }

    public class MaterialPEspecificoEntregaDTO
    {
        public int IdMaterialPEspecifico { get; set; }
        public int Grupo { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public int IdMaterialTipo { get; set; }
        public string NombreMaterialTipo { get; set; }
        public int IdMaterialAccion { get; set; }
        public string MaterialAccion { get; set; }
        public int IdMaterialPEspecificoDetalle { get; set; }
        public int IdMaterialVersion { get; set; }
        public string MaterialVersion { get; set; }
        public List<MaterialDetalleCriterioVerificacionDTO> CriteriosVerificacion { get; set; }
        public int IdMaterialEstado { get; set; }
        public int? IdEstadoRegistroMaterial { get; set; }
        public string EstadoRegistroMaterial { get; set; }
        public int? IdFur { get; set; }
    }


    public class MaterialDetalleCriterioVerificacionDTO
    {
        public int Id { get; set; }
        public int IdMaterialPEspecificoDetalle { get; set; }
        public int IdMaterialCriterioVerificacion { get; set; }
        public string MaterialCriterioVerificacion { get; set; }
        public bool EsAprobado { get; set; }
    }

    public class CriterioVerificacionColumnasDTO
    {
        public int IdMaterialCriterioVerificacion { get; set; }
        public string MaterialCriterioVerificacion { get; set; }
        public bool EsAprobado { get; set; }
    }

    public class EntregaMaterialDTO
    {
        public List<MaterialPEspecificoEntregaDTO> listaMateriales { get; set; }
        public List<CriterioVerificacionColumnasDTO> columnas { get; set; }
    }


    public class NotificarListaMaterialVersionDTO
    {
        public List<int> ListaIdMaterialPEspecificoDetalle { get; set; }
    }
}
