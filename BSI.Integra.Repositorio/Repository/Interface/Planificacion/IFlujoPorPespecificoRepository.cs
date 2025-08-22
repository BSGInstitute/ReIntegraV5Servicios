using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IFlujoPorPespecificoRepository : IGenericRepository<TFlujoPorPespecifico>
    {
        #region Metodos Base
        TFlujoPorPespecifico Add(FlujoPorPespecifico entidad);
        TFlujoPorPespecifico Update(FlujoPorPespecifico entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TFlujoPorPespecifico> Add(IEnumerable<FlujoPorPespecifico> listadoEntidad);
        IEnumerable<TFlujoPorPespecifico> Update(IEnumerable<FlujoPorPespecifico> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        FlujoPorPespecifico? ObtenerPorId(int id);
    }
}
