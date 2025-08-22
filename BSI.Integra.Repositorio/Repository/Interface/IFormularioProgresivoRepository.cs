using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormularioProgresivoRepository : IGenericRepository<TFormularioProgresivo>
    {
        #region Metodos Base
        List<TFormularioProgresivo> Add(FormularioProgresivo entidad);
        List<TFormularioProgresivo> Update(FormularioProgresivo entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivo ObtenerPorId(int id);
        IEnumerable<FormularioProgresivo> ObtenerRegistros();
        IEnumerable<FormularioProgresivoInicialDTO> ObtenerFormulariosIniciales();
        IEnumerable<FormularioProgresivoInicialDTO> ObtenerFormulariosInicialesSinFormularioRespuesta();
    }
}
