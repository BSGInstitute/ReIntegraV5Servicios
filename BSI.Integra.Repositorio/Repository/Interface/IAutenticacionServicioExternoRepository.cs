using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAutenticacionServicioExternoRepository : IGenericRepository<TAutenticacionServicioExterno>
    {
        #region Metodos Base
        TAutenticacionServicioExterno Add(AutenticacionServicioExterno entidad);
        TAutenticacionServicioExterno Update(AutenticacionServicioExterno entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAutenticacionServicioExterno> Add(IEnumerable<AutenticacionServicioExterno> listadoEntidad);
        IEnumerable<TAutenticacionServicioExterno> Update(IEnumerable<AutenticacionServicioExterno> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion

        public TokenFacebookDTO ObtenerTokenpoId(int id);


    }
}
