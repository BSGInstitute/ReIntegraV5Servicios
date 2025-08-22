using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IFormularioProgresivoService
    {
        #region Metodos Base
        List<FormularioProgresivo> Add(FormularioProgresivo entidad);
        List<FormularioProgresivo> Update(FormularioProgresivo entidad);
        bool Delete(int id, string usuario);
        #endregion
        FormularioProgresivo ObtenerPorId(int id);
        IEnumerable<FormularioProgresivo> ObtenerRegistros();
        IEnumerable<FormularioProgresivoInicialDTO> ObtenerFormulariosIniciales();
        IEnumerable<FormularioProgresivoInicialDTO> ObtenerFormulariosInicialesSinFormularioRespuesta();
    }
}
