using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Operacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface ICertificadoPartnerComplementoRepository : IGenericRepository<TCertificadoPartnerComplemento>
    {
        #region Metodos Base
        TCertificadoPartnerComplemento Add(CertificadoPartnerComplemento entidad);
        TCertificadoPartnerComplemento Update(CertificadoPartnerComplemento entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCertificadoPartnerComplemento> Add(IEnumerable<CertificadoPartnerComplemento> listadoEntidad);
        IEnumerable<TCertificadoPartnerComplemento> Update(IEnumerable<CertificadoPartnerComplemento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CertificadoPartnerComplementoDTO> ObtenerTodo();
        CertificadoPartnerComplemento? ObtenerPorId(int id);
        List<CentroCostoAsignadoCertificadoPartnerComplementoDTO> ObtenerCentroCostoAsociadoPorId(int id);
        CertificadoPartnerComplementoDTO ObtenerPorCentroCosto(int IdCentroCosto);
            
    }
}
