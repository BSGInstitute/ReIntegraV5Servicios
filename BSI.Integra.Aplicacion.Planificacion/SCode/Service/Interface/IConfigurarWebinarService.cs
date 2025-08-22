using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface.Planificacion
{
    public interface IConfigurarWebinarService
    {
        IEnumerable<ConfigurarWebinarDTO> ObtenerPorIdPespecificoPadre(int idPEspecificoPadre);
    }
}
