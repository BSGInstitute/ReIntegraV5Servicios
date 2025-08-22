using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICampaniaFacebookRepository : IGenericRepository<TCampaniaFacebook>
    {
        #region Metodos Base
        TCampaniaFacebook Add(CampaniaFacebook entidad);
        TCampaniaFacebook Update(CampaniaFacebook entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCampaniaFacebook> Add(IEnumerable<CampaniaFacebook> listadoEntidad);
        IEnumerable<TCampaniaFacebook> Update(IEnumerable<CampaniaFacebook> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
     
    }
}
