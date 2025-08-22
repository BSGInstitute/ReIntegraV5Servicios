using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{

    public interface ITipoEncuestumRepository : IGenericRepository<TTipoEncuestum>
    {
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ComboDTO> ObtenerComboTipoModalidad();

    }
}
