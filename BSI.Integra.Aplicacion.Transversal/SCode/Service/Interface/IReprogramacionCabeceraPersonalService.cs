using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IReprogramacionCabeceraPersonalService
    {
        #region Metodos Base
        ReprogramacionCabeceraPersonal Add(ReprogramacionCabeceraPersonal entidad);
        ReprogramacionCabeceraPersonal Update(ReprogramacionCabeceraPersonal entidad);
        bool Delete(int id, string usuario);

        List<ReprogramacionCabeceraPersonal> Add(List<ReprogramacionCabeceraPersonal> listadoEntidad);
        List<ReprogramacionCabeceraPersonal> Update(List<ReprogramacionCabeceraPersonal> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ReprogramacionCabeceraPersonalDTO> ObtenerReprogramacionCabeceraPersonal();
        ReprogramacionCabeceraPersonal ObtenerReprogramacionCabeceraPersonalAutomatica(int idActividadCabecera, int idCategoriaOrigen, int idPersonal);
        ReprogramacionCabeceraPersonal MapeoEntidadDesdeDTO(ReprogramacionCabeceraPersonalDTO dto);
    }
}
