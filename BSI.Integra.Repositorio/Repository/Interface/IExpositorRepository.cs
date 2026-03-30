using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IExpositorRepository : IGenericRepository<TExpositor>
    {
        #region Metodos Base
        TExpositor Add(Expositor entidad);
        TExpositor Update(Expositor entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TExpositor> Add(IEnumerable<Expositor> listadoEntidad);
        IEnumerable<TExpositor> Update(IEnumerable<Expositor> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        List<AgendaExpositorDTO> ObtenerExpositoresPorProgramaGeneral(int idPGeneral);
        Expositor? ObtenerPorId(int idExpositor);
        bool ExisteExpositorPorEmail(string email1);
        int? ObtenerExpositorEliminadoEmailRepetido(string email);
        bool EliminarFisicaExpositor(string tablaV3, string tablaV4, int idV4, Guid? idv3, int? id_v3);
        IEnumerable<ExpositorDTO> Obtener();
        IEnumerable<ExpositorDTO> BuscarPorContacto(string? email, string? celular, string? nroDocumento);
    }
}