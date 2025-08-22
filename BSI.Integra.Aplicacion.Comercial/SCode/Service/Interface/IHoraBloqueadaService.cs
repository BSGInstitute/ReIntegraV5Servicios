using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IHoraBloqueadaService
    {
        #region Metodos Base
        HoraBloqueada Add(HoraBloqueada entidad);
        HoraBloqueada Update(HoraBloqueada entidad);
        bool Delete(int id, string usuario);

        List<HoraBloqueada> Add(List<HoraBloqueada> listadoEntidad);
        List<HoraBloqueada> Update(List<HoraBloqueada> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<HoraBloqueadaDTO> ObtenerHoraBloqueada();
        List<HoraBloqueadaRADTO> ObtenerHorasBloquedasReprogramacionPorAsesor(int idPersonal, DateTime fecha);
    }
}
