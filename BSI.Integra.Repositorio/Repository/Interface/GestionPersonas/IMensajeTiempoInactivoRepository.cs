using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IMensajeTiempoInactivoRepository : IGenericRepository<TMensajeTiempoInactivo>
    {
        #region Metodos Base
        TMensajeTiempoInactivo Add(MensajeTiempoInactivo entidad);
        TMensajeTiempoInactivo Update(MensajeTiempoInactivo entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMensajeTiempoInactivo> Add(IEnumerable<MensajeTiempoInactivo> listadoEntidad);
        IEnumerable<TMensajeTiempoInactivo> Update(IEnumerable<MensajeTiempoInactivo> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MensajeTiempoInactivoDTO> Obtener();
        MensajeTiempoInactivo? ObtenerPorId(int id);
    }
}
