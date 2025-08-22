using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IPrestacionRegistroService
    {
        #region Metodos Base
        PrestacionRegistro Add(PrestacionRegistro entidad);
        PrestacionRegistro Update(PrestacionRegistro entidad);
        bool Delete(int id, string usuario);

        List<PrestacionRegistro> Add(List<PrestacionRegistro> listadoEntidad);
        List<PrestacionRegistro> Update(List<PrestacionRegistro> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<PrestacionRegistroComboDTO> ObtenerCombo();

    }
}
