using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IOportunidadErradoService
    {
        #region Metodos Base
        OportunidadErrado Add(OportunidadErrado entidad);
        OportunidadErrado Update(OportunidadErrado entidad);
        bool Delete(int id, string usuario);

        List<OportunidadErrado> Add(List<OportunidadErrado> listadoEntidad);
        List<OportunidadErrado> Update(List<OportunidadErrado> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public object CorregirDatoOportunidad(AsignacionAutomaticaCompuestoDTO objs, string usuario);
    }
}
