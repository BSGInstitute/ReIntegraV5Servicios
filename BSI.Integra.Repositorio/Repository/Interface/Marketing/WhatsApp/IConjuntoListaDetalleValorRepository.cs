using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IConjuntoListaDetalleValorRepository : IGenericRepository<TConjuntoListaDetalleValor>
    {


        public List<FiltroSegmentoValorTipoDTO> ObtenerConjuntoListaDetalleValor(int idConjuntoListaDetalle);

    }
}