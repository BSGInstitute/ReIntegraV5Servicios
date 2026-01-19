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
    }
}
