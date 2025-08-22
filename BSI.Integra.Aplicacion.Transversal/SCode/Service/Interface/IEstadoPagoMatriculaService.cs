using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IEstadoPagoMatriculaService
    {
        #region Metodos Base
        EstadoPagoMatricula Add(EstadoPagoMatricula entidad);
        EstadoPagoMatricula Update(EstadoPagoMatricula entidad);
        bool Delete(int id, string usuario);

        List<EstadoPagoMatricula> Add(List<EstadoPagoMatricula> listadoEntidad);
        List<EstadoPagoMatricula> Update(List<EstadoPagoMatricula> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion



        List<DTO.ComboDTO> ObtenerTodoFiltro();
        }
}
