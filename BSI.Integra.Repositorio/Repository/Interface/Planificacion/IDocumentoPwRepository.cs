using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IDocumentoPwRepository : IGenericRepository<TDocumentoPw>
    {
        #region Metodos Base
        TDocumentoPw Add(DocumentoPw entidad);
        TDocumentoPw Update(DocumentoPw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDocumentoPw> Add(IEnumerable<DocumentoPw> listadoEntidad);
        IEnumerable<TDocumentoPw> Update(IEnumerable<DocumentoPw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<PlantillaDocumentoAsociadoDTO> ObtenerDocumentosAsociados(int idPGeneral);
        List<PlantillaDocumentoNoAsociadoDTO> ObtenerDocumentosNoAsociados(int idPGeneral);
        DocumentoPw ObtenerPorId(int id);
        IEnumerable<DocumentoPwDTO> Obtener();
        void RegularizaIntroduccionPrerrequisito(int IdDocumentoPw, string usuario);
        void InsertarVersionesBeneficiosDocumentosPw(DocumentoPwVersionesDTO entidad, string usuario);
        void ActualizarIntroduccionBeneficioDocumentoPw(string introduccion,int idDocumentoPw, int version , string usuario);

        IntDTO ValidarSiTieneRegistro(int IdDocumentoPw);
        StringDTO ValidarSiTieneIntroduccionLaVersion(int IdDocumentoPw, int version);
        IEnumerable<DocumentoPwVersionesDTO> ObtenerIntroduccionVersionDocumento(int IdDocumentoPw);
        List<CursoHijoDuracionPdusDTO> ObtenerProgramasEstrucuraCurricularPdus(int idPGeneral);
        List<EstructuraCursoDTO> ObtenerEstructuraCurso(int idPGeneral);

        IEnumerable<ModalidadPortalDTO> ObtenerModalidadPortal();
        IEnumerable<ComboDTO> ObtenerModoFechaInicio();
        IEnumerable<ComboDTO> ObtenerNotasTipo();

        void InsertarDocumentoPwModalidad(SeccionModalidadHorarioDTO? dto, int id, string usuario);
        IEnumerable<DocumentoPWModalidadRowVM> ObtenerDocumentoPWModalidadRows(int idDocumentoPW);
        void InsertarDocumentoPwDuracion(SeccionDuracionDTO? dto, int idDocumentoPw, string usuario);
        IEnumerable<DocumentoPWDuracionRowVM> ObtenerDocumentoPWDuracionRows(int idDocumentoPW);
        void InsertarDocumentoPwFechaInicio(SeccionFechaInicioDTO? dto, int idDocumentoPw, string usuario);

        List<DocumentoPWFechaInicioRowDTO> ObtenerDocumentoPWFechaInicioRows(int idDocumentoPw);

        void InsertarDocumentoPwNotas(SeccionNotasDTO dto, int idDocumentoPw, string usuario);

        List<DocumentoPWNotasRowDTO> ObtenerDocumentoPWNotasRows(int idDocumentoPW);
        string ObtenerDocumentoPWModalidadRowsSP(int idDocumentoPw);


        string SP_TDocumentoPWModalidadIntroduccion_Insertar(string? introduccion, string usuario);
        string SP_TDocumentoPWModalidadIntroduccion_Actualizar(int id, string? introduccion, string usuario);
        string SP_TDocumentoPWModalidad_Insertar(int idModalidadPortal, string? subTitulo, string? descripcion, string usuario);
        string SP_TDocumentoPWModalidad_Actualizar(int id, int idModalidadPortal, string? subTitulo, string? descripcion, string usuario);
        string SP_TDocumentoPWModalidadDetalle_Insertar(int idDocumentoPWModalidad, int orden, string? tipo, string? beneficio, int? idPais, string usuario);
        string SP_TDocumentoPWModalidadDetalle_Actualizar(int id, int idDocumentoPWModalidad, int orden, string? tipo,string? beneficio, int? idPais, string usuario);
        string SP_DocumentoPWModalidadConfiguracion_RegistrarCambios(int idDocumentoPw, int idIntroduccion, int idDocumentoPWModalidad, string usuario);
        string SP_TDocumentoPWModalidad_Desactivar(int idDocumentoPWModalidad, string usuario);
        string SP_TDocumentoPWModalidadDetalle_Desactivar(int idDocumentoPWModalidadDetalle, string usuario);
        string SP_TDocumentoPWModalidadConfiguracion_DesactivarPorModalidad(int idDocumentoPw, int idDocumentoPWModalidad, string usuario);
        string ObtenerDocumentoPWDuracionRowsSP(int idDocumentoPw);
        string SP_TDocumentoPWDuracion_Insertar(string? titulo, string? introduccion, string? pieDePagina, string usuario);
        string SP_TDocumentoPWDuracionDetalle_Insertar(int idDocumentoPWDuracion, int idVersionPrograma, string? detalleMes, string? detalleHora, string usuario);
        string SP_TDocumentoPWDuracionConfiguracion_Insertar(int idDocumentoPw, int idDocumentoPWDuracion, string usuario);
        string SP_TDocumentoPWDuracion_Actualizar(int id, string? titulo, string? introduccion, string? pieDePagina, string usuario);
        string SP_TDocumentoPWDuracionDetalle_Actualizar(int id, int idDocumentoPWDuracion, int idVersionPrograma, string? detalleMes, string? detalleHora, string usuario);
        string SP_TDocumentoPWDuracionDetalle_Desactivar(int id, string usuario);
        string SP_DocumentoPWDuracionConfiguracion_RegistrarCambios(int idDocumentoPw, int idDocumentoPWDuracion, string usuario);
        string SP_TDocumentoPWFechaInicio_ObtenerRows(int idDocumentoPw);

        string SP_TDocumentoPWFechaInicioCabecera_Insertar(string? titulo, string? subTitulo, bool mostrarEnLaWeb, string usuario);
        string SP_TDocumentoPWFechaInicioCabecera_Actualizar(int id, string? titulo, string? subTitulo, bool mostrarEnLaWeb, string usuario);

        string SP_TDocumentoPWFechaInicio_Insertar(int? idPais, string usuario);
        string SP_TDocumentoPWFechaInicio_Actualizar(int id, int? idPais, string usuario);

        string SP_TDocumentoPWFechaInicioDetalle_Insertar(int idDocumentoPWFechaInicio, int? idModo, DateTime? fecha, string? horario, string usuario);
        string SP_TDocumentoPWFechaInicioDetalle_Actualizar(int id, int idDocumentoPWFechaInicio, int? idModo, DateTime? fecha, string? horario, string usuario);

        string SP_DocumentoPWFechaInicioConfiguracion_RegistrarCambios(int idDocumentoPWFechaInicioCabecera, int idDocumentoPWFechaInicio, int idDocumentoPw, string usuario);

        string SP_TDocumentoPWFechaInicioDetalle_Desactivar(int id, string usuario);
        string SP_TDocumentoPWFechaInicioDetalle_DesactivarPorFechaInicio(int idDocumentoPWFechaInicio, string usuario);

        string SP_TDocumentoPWFechaInicio_Desactivar(int idDocumentoPWFechaInicio, string usuario);
        string SP_TDocumentoPWFechaInicioConfiguracion_DesactivarPorFechaInicio(int idDocumentoPw, int idDocumentoPWFechaInicio, string usuario);

        string SP_TDocumentoPWNota_ObtenerRows(int idDocumentoPw);

        string SP_TDocumentoPWNota_Insertar(int idDocumentoPWNotaTipo, int? idPGeneral, string? descripcion, string usuario);
        string SP_TDocumentoPWNota_Actualizar(int id, int idDocumentoPWNotaTipo, int? idPGeneral, string? descripcion, string usuario);
        string SP_TDocumentoPWNota_Desactivar(int id, string usuario);

        string SP_TDocumentoPWNotaDetalle_Insertar(int idDocumentoPWNota, int orden, string? informacionExtra, int? idPais, string usuario);
        string SP_TDocumentoPWNotaDetalle_Actualizar(int id, int idDocumentoPWNota, int orden, string? informacionExtra, int? idPais, string usuario);
        string SP_TDocumentoPWNotaDetalle_Desactivar(int id, string usuario);
        string SP_TDocumentoPWNotaDetalle_DesactivarPorNota(int idDocumentoPWNota, string usuario);

        string SP_DocumentoPWNotaConfiguracion_RegistrarCambios(int idDocumentoPw, int idDocumentoPWNota, bool mostrarWeb, string usuario);
        string SP_TDocumentoPWNotaConfiguracion_DesactivarPorNota(int idDocumentoPw, int idDocumentoPWNota, string usuario);

    }
}
