using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProveedorPEspecificoRepository : IGenericRepository<TProveedorPespecifico>
    {
        #region Metodos Base
        TProveedorPespecifico Add(ProveedorPespecifico entidad);
        TProveedorPespecifico Update(ProveedorPespecifico entidad);
        bool Delete(int id, string usuario);
        #endregion

        IEnumerable<ProveedorActivoPEspecificoDTO> ObtenerActivoPEspecifico();
        IEnumerable<ProveedorPEspecificoGridDTO> ObtenerPorIdProveedor(int idProveedor);
        bool ExistePorProveedorYPespecifico(int idProveedor, int idPespecifico);
    }
}
