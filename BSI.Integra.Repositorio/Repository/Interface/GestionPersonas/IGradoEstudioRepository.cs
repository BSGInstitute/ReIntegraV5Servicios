using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IGradoEstudioRepository : IGenericRepository<TGradoEstudio>
    {

        #region Metodos Base
        TGradoEstudio Add(GradoEstudio entidad);
        TGradoEstudio Update(GradoEstudio entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TGradoEstudio> Add(IEnumerable<GradoEstudio> listadoEntidad);
        IEnumerable<TGradoEstudio> Update(IEnumerable<GradoEstudio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<GradoEstudioDTO> Obtener();
        GradoEstudio? ObtenerPorId(int idGradoEstudio);
        IEnumerable<GradoEstudioDTO> ObtenerEstadoEstudio();
    }
}
