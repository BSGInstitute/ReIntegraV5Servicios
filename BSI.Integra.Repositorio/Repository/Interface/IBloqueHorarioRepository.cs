using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IBloqueHorarioRepository : IGenericRepository<TBloqueHorario>
    {
        IEnumerable<BloqueHorarioProcesarBicDTO> ObtenerCombo();
        BloqueHorarioProcesaOportunidad ObtenerConfiguracion(string dia);
        List<BloqueHorarioProcesarBicDTO> ObtenerPorIdConfiguracionBic(int idConfiguracionBic);
    }
}
