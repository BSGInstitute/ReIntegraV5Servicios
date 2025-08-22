using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IContadorBicService
    {
        int CalcularDiasParaBIC();
        List<int> EjecutarBIC(int idPaisSede);
        List<int> EjecutarBICManualmente();
        List<ContadorBic> CalcularDiasParaBICPorIdOportunidad(int idOportunidad);
        List<ContadorBic> CalcularDiasParaBICPorIdOportunidadWhatsapp(int idOportunidad);
        StringDTO CalcularDiasParaBICAlterno();
    }
}
