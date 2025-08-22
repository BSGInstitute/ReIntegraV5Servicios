using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ITipoEstudioRepository : IGenericRepository<TTipoEstudio>
    {
        #region Metodos Base
        TTipoEstudio Add(TipoEstudio entidad);
        TTipoEstudio Update(TipoEstudio entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTipoEstudio> Add(IEnumerable<TipoEstudio> listadoEntidad);
        IEnumerable<TTipoEstudio> Update(IEnumerable<TipoEstudio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        TipoEstudio? ObtenerPorId(int id);

        IEnumerable<TipoEstudioDTO> ObtenerListaTipoEstudioCombo();
        IEnumerable<TipoEstudioDTO> Obtener();
    }
}
