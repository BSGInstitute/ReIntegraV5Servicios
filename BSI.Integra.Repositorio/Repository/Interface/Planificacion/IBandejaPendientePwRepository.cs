using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IBandejaPendientePwRepository : IGenericRepository<TBandejaPendientePw>
    {
        #region Metodos Base
        TBandejaPendientePw Add(BandejaPendientePw entidad);
        TBandejaPendientePw Update(BandejaPendientePw entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TBandejaPendientePw> Add(IEnumerable<BandejaPendientePw> listadoEntidad);
        IEnumerable<TBandejaPendientePw> Update(IEnumerable<BandejaPendientePw> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<BandejaPendientePw> ObtenerPorIdDocumento(int idDocumentoPw);
        BandejaPendientePw? ObtenerPorIdDocumentoIdRevisionNivel(int idDocumentoPw, int idRevisionPw);
    }
}
