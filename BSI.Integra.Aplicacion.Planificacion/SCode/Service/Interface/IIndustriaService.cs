using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IIndustriaService
    {
        #region Metodos Base
        Industria Add(Industria entidad);
        Industria Update(Industria entidad);
        bool Delete(int id, string usuario);

        List<Industria> Add(List<Industria> listadoEntidad);
        List<Industria> Update(List<Industria> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion 
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
