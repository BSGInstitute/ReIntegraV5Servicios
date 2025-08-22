using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITipoContratoRepository : IGenericRepository<TTipoContrato>
    {
        #region Metodos Base
        TTipoContrato Add(TipoContrato entidad);
        TTipoContrato Update(TipoContrato entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoContrato> Add(IEnumerable<TipoContrato> listadoEntidad);
        IEnumerable<TTipoContrato> Update(IEnumerable<TipoContrato> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<TipoContratoDTO>  ObtenerTipoContrato();
        IEnumerable<TipoContratoComboDTO> ObtenerCombo();
    }
}
