using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB; 
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroSegmentoFolder;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IFiltroSegmentoService
    {
        #region Metodos Base
        FiltroSegmento Add(FiltroSegmento entidad);
        FiltroSegmento Update(FiltroSegmento entidad);
        bool Delete(int id, string usuario);

        List<FiltroSegmento> Add(List<FiltroSegmento> listadoEntidad);
        List<FiltroSegmento> Update(List<FiltroSegmento> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        bool EjecutarFiltro(int Id, string UsuarioModificacion);


        List<FiltroDTO> ObtenerTodoFiltro();
        List<DTO.ComboDTO> ObtenerCombo();
        List<DTO.ComboDTO> ObtenerSubArea(string idAreas);
        List<DTO.ComboDTO> ObtenerProgramaEspecifico(string IdProgramaGeneral);
        bool EnvioCorreo(string displayname, string subject, string mensaje, List<AddresseeDTO> listaReceptores);
        FiltroSegmentoDTO ObtenerDetalleFiltroSegmento(int IdFiltroSegmento);
        List<FacebookAudienciaHistorialDTO> ObtenerHistorialPorIdFiltroSegmento(int idFiltroSegmento);
        void LlenarHijoParaInsertar(FiltroSegmentoDTO filtro);
        bool InsertarFiltro(FiltroSegmentoDTO obj, String UsuarioModificacion);
        List<FiltroDTO> ObtenerTodoFiltroFiltroSegmento();
        bool EliminarFiltro(int IdFiltroSegmento, String UsuarioModificacion);
        List<FiltroSegmentoCompuestoDTO> ObtenerResultadoCompuesto(int id, int idFiltroSegmentoTipoContacto);
        List<FiltroSegmentoCompuestoDTO> ObtenerResultadosFiltro(int Id);
        bool Duplicar(int IdFiltroSegmento, string UsuarioModificacion);
        bool ActualizarFiltroEliminacionLogica(int IdFiltroSegmento, String UsuarioModificacion);
        void LlenarHijoParaActualizar(FiltroSegmentoDTO filtro, string UsuarioModificacion);
        bool ActualizarFiltro(FiltroSegmentoDTO Json, string UsuarioModificacion);
        bool InsertarAudiencia(FacebookAudienciaDTO Json, string UsuarioCreacion);

        string FacebookNewAudiencie(FacebookAudienciaDTO Json);
        public RespuestaOportunidadFiltroSegmentoDTO CrearOportunidadPorFiltroSegmento(OportunidadFiltroSegmentoDTO OportunidadFiltroSegmento);
        public bool ActualizarAudiencia(FacebookAudienciaDTO Json);

    }
}
