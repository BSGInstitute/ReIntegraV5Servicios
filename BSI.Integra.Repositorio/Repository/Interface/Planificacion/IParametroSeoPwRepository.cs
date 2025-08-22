using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IParametroSeoPwRepository : IGenericRepository<TParametroSeoPw>
    {
        IEnumerable<ParametroSeoPwDTO> ObtenerCombo();
        Task<IEnumerable<ParametroSeoPwDTO>> ObtenerComboAsync();

    }
}
