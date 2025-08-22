using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System.Linq.Expressions;
using WhatsAppResultadoConjuntoListaDTO = BSI.Integra.Aplicacion.DTO.Modelos.WhatsAppResultadoConjuntoListaDTO;


namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConjuntoListaResultadoRepository : IGenericRepository<TConjuntoListaResultado>
    {

        #region Metodos Base
        TConjuntoListaResultado Add(ConjuntoListaResultadoDTO entidad);
        TConjuntoListaResultado Update(ConjuntoListaResultadoDTO entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TConjuntoListaResultado> Add(IEnumerable<ConjuntoListaResultadoDTO> listadoEntidad);
        IEnumerable<TConjuntoListaResultado> Update(IEnumerable<ConjuntoListaResultadoDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        bool Eliminar(int idConjuntoListaDetalle); 
        bool EliminarPorConjuntoLista(int idConjuntoLista, string nombreUsuario);
        List<WhatsAppResultadoConjuntoListaDTO> ObtenerConjuntoListaResultado(int IdConjuntoListaDetalle);
        List<PreWhatsAppResultadoConjuntoListaDTO> ObtenerListaPreparadaProcesamiento(int idConjuntoListaDetalle);
        List<PreWhatsAppResultadoConjuntoListaDTO> PreObtenerConjuntoListaResultado(int IdConjuntoListaDetalle);
        List<WhatsAppResultadoConjuntoListaDTO> ObtenerConjuntoListaResultadoWhatsAppMasivoOperaciones(int idConjuntoListaDetalle);
        List<WhatsAppResultadoConjuntoListaDTO> ObtenerOportunidadesReasignadasOperaciones();
        List<RegularizarMensajeWhatsAppEnvioDTO> ObtenerEnvioSinMensaje();  
        List<FacebookAudienciaDatosAlumnoDTO> ObtenerConjuntoListaResultadoFacebook(int IdConjuntoListaDetalle);
        List<TConjuntoListaResultado> ObtenerPorConjuntoListaDetalle(int idConjuntoListaDetalle);
        List<ConjuntoListaResultado> ObtenerPorConjuntoListaDetalleRedireccion(int idConjuntoListaDetalle);
        List<ConjuntoListaResultado> ObtenerPorConjuntoListaDetalleDapper(int idConjuntoListaDetalle);
		FiltroFasesOportunidadAlumnoDTO ObtenerHoraMinimaFasesCadena(FiltroHoraMinimaFasesCadenaDTO filtros);
		List<AlumnoOportunidadFiltroDTO> ObtenerAlumnoOportunidadEnvioAutomatico(AlumnoOportunidadEnvioAutomaticoDTO filtros);
        string[][] ObtenerMessengerUsuarioPorConjuntoListaResultado(int IdConjuntoListaDetalle, int idFacebookPagina);
        bool ExisteConjuntoListaResultado(int idConjuntoListaResultado);
        ConjuntoListaResultado BuscaConjuntoListaResultado(int idConjuntoListaResultado);
    }
}