using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFormularioSolicitudTextoBotonRepository : IGenericRepository<TFormularioSolicitudTextoBoton>
    {
        #region Metodos Base
        TFormularioSolicitudTextoBoton Add(FormularioSolicitudTextoBoton entidad);
        TFormularioSolicitudTextoBoton Update(FormularioSolicitudTextoBoton entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFormularioSolicitudTextoBoton> Add(IEnumerable<FormularioSolicitudTextoBoton> listadoEntidad);
        IEnumerable<TFormularioSolicitudTextoBoton> Update(IEnumerable<FormularioSolicitudTextoBoton> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<FormularioSolicitudTextoBotonDTO> ObtenerFormularioSolicitudTextoBoton();
        IEnumerable<FormularioSolicitudTextoBotonFiltroDTO> ObtenerFiltroFormularioSolicitudTextoBoton();

    }
}
