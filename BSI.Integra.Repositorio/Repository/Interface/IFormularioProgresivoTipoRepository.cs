using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormularioProgresivoTipoRepository : IGenericRepository<TFormularioProgresivoTipo>
    {
        #region Metodos Base
        List<TFormularioProgresivoTipo> Add(FormularioProgresivoTipo entidad);
        List<TFormularioProgresivoTipo> Update(FormularioProgresivoTipo entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivoTipo ObtenerPorId(int id);
        IEnumerable<FormularioProgresivoTipo> ObtenerRegistros();
    }
}
