using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICampoFormularioRepository : IGenericRepository<TCampoFormulario>
    {
        #region Metodos Base
        TCampoFormulario Add(CampoFormulario entidad);
        TCampoFormulario Update(CampoFormulario entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCampoFormulario> Add(IEnumerable<CampoFormulario> listadoEntidad);
        IEnumerable<TCampoFormulario> Update(IEnumerable<CampoFormulario> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();

        IEnumerable<CampoFormularioDTO> ObtenerCampoFormulario();

        IEnumerable<CampoFormularioSeleccionadoDTO> ObtenerCampoFormularioPorIdFormularioSolicitud(int idFormularioSolicitud);

    }
}
