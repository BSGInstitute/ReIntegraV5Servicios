using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadMaximaPorCategoriaRepository : IGenericRepository<TOportunidadMaximaPorCategorium>
    {
        #region Metodos Base
        TOportunidadMaximaPorCategorium Add(OportunidadMaximaPorCategoria entidad);
        TOportunidadMaximaPorCategorium Update(OportunidadMaximaPorCategoria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadMaximaPorCategorium> Add(IEnumerable<OportunidadMaximaPorCategoria> listadoEntidad);
        IEnumerable<TOportunidadMaximaPorCategorium> Update(IEnumerable<OportunidadMaximaPorCategoria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<OportunidadMaximaPorCategoriaDTO> ObtenerOportunidadMaximaPorCategoria();
        IEnumerable<OportunidadMaximaPorCategoriaComboDTO> ObtenerCombo();
        SeguimientoAsesorDTO ObtenerSeguimientoAsesor(int idPersonal, int idCategoriaOrigen, int estadoPantalla);
        void ActualizarDatosEstaticosPantalla2(int idAsesor, int idCategoriaOrigen, int estadoISOM);
        Task ActualizarDatosEstaticosPantalla2Async(int idAsesor, int idCategoriaOrigen, int estadoISOM);
    }
}