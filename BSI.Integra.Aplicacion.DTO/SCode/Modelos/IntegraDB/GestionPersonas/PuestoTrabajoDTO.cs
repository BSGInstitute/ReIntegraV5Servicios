
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int? IdPersonalAreaTrabajo { get; set; }
        public List<GrupoComparacionProcesoSeleccionDTO> GrupoComparacionProcesoSeleccions { get; set; }
    }
    public class PuestoTrabajoPorFechaDTO
    {
        public int Id { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public string Nombre { get; set; }
        public int? IdPerfilPuestoTrabajo { get; set; }
        public string Objetivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? PuestoTrabajoFechaModificacion { get; set; }
        public DateTime? PerfilPuestoTrabajoFechaModificacion { get; set; }
        public DateTime? PersonalAreaFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoCaracteristicaPersonalFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoCursoComplementarioFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoExperienciaFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoFormacionAcademicaFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoFuncionFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoRelacionFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoRelacionDetalleFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoReporteFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoPuntajeCalificacionFechaModificacion { get; set; }
        public DateTime? ModuloSistemaPuestoTrabajoFechaModificacion { get; set; }

        public string PuestoTrabajoUsuarioModificacion { get; set; }
        public string PerfilPuestoTrabajoUsuarioModificacion { get; set; }
        public string PersonalAreaUsuarioModificacion { get; set; }
        public string PuestoTrabajoCaracteristicaPersonalUsuarioModificacion { get; set; }
        public string PuestoTrabajoCursoComplementarioUsuarioModificacion { get; set; }
        public string PuestoTrabajoExperienciaUsuarioModificacion { get; set; }
        public string PuestoTrabajoFormacionAcademicaUsuarioModificacion { get; set; }
        public string PuestoTrabajoFuncionUsuarioModificacion { get; set; }
        public string PuestoTrabajoRelacionUsuarioModificacion { get; set; }
        public string PuestoTrabajoRelacionDetalleUsuarioModificacion { get; set; }
        public string PuestoTrabajoReporteUsuarioModificacion { get; set; }
        public string PuestoTrabajoPuntajeCalificacionUsuarioModificacion { get; set; }
        public string ModuloSistemaPuestoTrabajoUsuarioModificacion { get; set; }
    }

    public class PuestoTrabajoEnviarDTO
    {
        public int Id { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public string Nombre { get; set; }
        public int? IdPerfilPuestoTrabajo { get; set; }
        public string Objetivo { get; set; }
        public string Descripcion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string FechaModificacion { get; set; }
    }

    public class PuestoFechaDTO
    {
        public int Id { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
        public string Nombre { get; set; }
        public int? IdPerfilPuestoTrabajo { get; set; }
        public string Objetivo { get; set; }
        public string Descripcion { get; set; }
        public List<FechaModificacionDTO> ListaFechaModificacion { get; set; }
    }
    public class FechaModificacionDTO
    {
        public DateTime? PuestoTrabajoFechaModificacion { get; set; }
        public DateTime? PerfilPuestoTrabajoFechaModificacion { get; set; }
        public DateTime? PersonalAreaFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoCaracteristicaPersonalFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoCursoComplementarioFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoExperienciaFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoFormacionAcademicaFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoFuncionFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoRelacionFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoRelacionDetalleFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoReporteFechaModificacion { get; set; }
        public DateTime? PuestoTrabajoPuntajeCalificacionFechaModificacion { get; set; }
        public DateTime? ModuloSistemaPuestoTrabajoFechaModificacion { get; set; }


        public string PuestoTrabajoUsuarioModificacion { get; set; }
        public string PerfilPuestoTrabajoUsuarioModificacion { get; set; }
        public string PersonalAreaUsuarioModificacion { get; set; }
        public string PuestoTrabajoCaracteristicaPersonalUsuarioModificacion { get; set; }
        public string PuestoTrabajoCursoComplementarioUsuarioModificacion { get; set; }
        public string PuestoTrabajoExperienciaUsuarioModificacion { get; set; }
        public string PuestoTrabajoFormacionAcademicaUsuarioModificacion { get; set; }
        public string PuestoTrabajoFuncionUsuarioModificacion { get; set; }
        public string PuestoTrabajoRelacionUsuarioModificacion { get; set; }
        public string PuestoTrabajoRelacionDetalleUsuarioModificacion { get; set; }
        public string PuestoTrabajoReporteUsuarioModificacion { get; set; }
        public string PuestoTrabajoPuntajeCalificacionUsuarioModificacion { get; set; }
        public string ModuloSistemaPuestoTrabajoUsuarioModificacion { get; set; }
    }
    public class FechaUsuarioDTO
    {
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
    }


    public class PuestoTrabajoModuloSistemaDTO
    {
        public int Id { get; set; }
        public int IdModuloSistema { get; set; }
        public string ModuloSistema { get; set; }
        public string IdModuloSistemaGrupo { get; set; }
        public string ModuloSistemaGrupo { get; set; }
        public bool Estado { get; set; }
        public string Url { get; set; }
        public int? IdTipo { get; set; }
        public string NombreTipo { get; set; }
    }

    public class ModuloSistemaModuloGrupoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IdModuloSistemaGrupo { get; set; }
        public string ModuloSistemaGrupo { get; set; }
        public string Url { get; set; }
        public int? IdTipo { get; set; }
        public string NombreTipo { get; set; }
    }

    public class ValidarAsignacionDTO
    {
        public int Id { get; set; }
        public int IdModuloSistema { get; set; }
        public string ModuloSistema { get; set; }
        public string IdModuloSistemaGrupo { get; set; }
        public string ModuloSistemaGrupo { get; set; }
        public bool Estado { get; set; }
        public bool Modificacion { get; set; }
        public string Url { get; set; }
    }
    public class AsignarInterfazDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public List<ValidarAsignacionDTO> ListaAsignacion { get; set; }
    }



    public class ComboPuestoTrabajo
    {
        public IEnumerable<ComboDTO> listaPersonalAreaTrabajo { get; set; }
        public IEnumerable<ComboDTO> listaPuestoTrabajo { get; set; }
        public IEnumerable<PersonalTipoFuncionDTO> listaTipoFuncion { get; set; }
        public IEnumerable<FrecuenciaPuestoTrabajoDTO> listaFrecuenciaPuestoTrabajo { get; set; }
        public IEnumerable<TipoCursoComplementarioDTO> listaTipoCompetenciaTecnica { get; set; }
        public IEnumerable<CursoComplementarioDTO> listaCompetenciaTecnica { get; set; }
        public IEnumerable<NivelCompetenciaTecnicaDTO>  listaNivelCompetenciaTecnica { get; set; }
        public IEnumerable<ExperienciaComboDTO> listaExperiencia { get; set; }
        public IEnumerable<TipoExperienciaDTO> listaTipoExperiencia { get; set; }
        public IEnumerable<ComboDTO>  listaSexo { get; set; }
        public IEnumerable<TipoFormacionDTO> listaTipoFormacion { get; set; }
        public IEnumerable<NivelEstudioComboDTO> listaNivelEstudio { get; set; }
        public IEnumerable<ComboDTO> listaAreaFormacion { get; set; }
        public IEnumerable<CentroEstudioDTO> listaCentroEstudio { get; set; }
        public IEnumerable<GradoEstudioDTO> listaGradoEstudio { get; set; }
        public IEnumerable<ProcesoSeleccionRangoDTO> listaRango { get; set; }
        public IEnumerable<EstadoCivilDTO> listaEstadoCivil { get; set; }
    }
    public class PuestoTrabajoInsertDTO
    {
        public int Id { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public string? PersonalAreaTrabajo { get; set; }
        public string? Nombre { get; set; }
        public int? IdPerfilPuestoTrabajo { get; set; }
        public string? Objetivo { get; set; }
        public string? Descripcion { get; set; }
    }

    public class PerfilPuestoTrabajoInsertarActualizarDTO
    {
        public int IdPuestoTrabajo { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public string Descripcion { get; set; }
        public string Objetivo { get; set; }

        public bool EstadoPuestoTrabajoCaracteristicaPersonal { get; set; }
        public bool EstadoPuestoTrabajoCursoComplementario { get; set; }
        public bool EstadoPuestoTrabajoExperiencia { get; set; }
        public bool EstadoPuestoTrabajoFormacionAcademica { get; set; }
        public bool EstadoPuestoTrabajoFuncion { get; set; }
        public bool EstadoPuestoTrabajoRelacion { get; set; }
        public bool EstadoPuestoTrabajoReporte { get; set; }

        public List<PuestoTrabajoCaracteristicaPersonalDTO>? PuestoTrabajoCaracteristicaPersonal { get; set; }
        public List<PuestoTrabajoCursoComplementarioDTO>? PuestoTrabajoCursoComplementario { get; set; }
        public List<PuestoTrabajoExperienciaPuestoTrabajo>? PuestoTrabajoExperiencia { get; set; }
        public List<PuestoTrabajoFormacionAcademicaDTO>? PuestoTrabajoFormacion { get; set; }
        public List<PuestoTrabajoFuncionDTO>? PuestoTrabajoFuncion { get; set; }
        public List<PuestoTrabajoRelacionCompuestoDTO>? PuestoTrabajoRelacion { get; set; }
        public List<PuestoTrabajoReporteDTO>? PuestoTrabajoReporte { get; set; }
        public PuestoTrabajoPuntajeEvaluacionAgrupadaComponenteDTO? Puntaje { get; set; }
        public bool CrearNuevaVersion { get; set; }
        public string? Usuario { get; set; }
        public bool EsUsuarioAprobacion { get; set; }
        public int IdPersonal { get; set; }
    }
    public class PuestoTrabajoPuntajeEvaluacionAgrupadaComponenteDTO
    {
        public List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> ListaPuntaje { get; set; }
    }


    public class AprobacionRechazoPerfilPuestoTrabajoDTO
    {
        public int IdPerfilPuestoTrabajo { get; set; }
        public bool TipoBoton { get; set; }
        public int? IdPersonal { get; set; }
        public string Observacion { get; set; }
        
    }
}
