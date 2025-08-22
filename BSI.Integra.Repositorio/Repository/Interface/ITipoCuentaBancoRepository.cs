using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoCuentaBancoRepository : IGenericRepository<TTipoCuentaBanco>
    {
        #region Metodos Base
        TTipoCuentaBanco Add(TipoCuentaBanco entidad);
        TTipoCuentaBanco Update(TipoCuentaBanco entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoCuentaBanco> Add(IEnumerable<TipoCuentaBanco> listadoEntidad);
        IEnumerable<TTipoCuentaBanco> Update(IEnumerable<TipoCuentaBanco> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoCuentaBancoComboDTO> ObtenerCombo();
        IEnumerable<TipoCuentaBancoDTO> ObtenerTipoCuentaBanco();
    }
}
