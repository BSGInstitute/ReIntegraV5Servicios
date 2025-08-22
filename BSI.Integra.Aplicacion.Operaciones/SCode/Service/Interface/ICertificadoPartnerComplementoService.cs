using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Operacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System.Web.Mvc;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface ICertificadoPartnerComplementoService
    {

        IEnumerable<CertificadoPartnerComplementoDTO> ObtenerTodo();

        CertificadoPartnerComplementoDTO Insertar(CertificadoPartnerComplementoDTO dto, string usuario);
        CertificadoPartnerComplementoDTO Actualizar(CertificadoPartnerComplementoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        CentroCostoCertificadoDTO Asignar(int idCertificadoPartnerComplemento, int idCentroCosto, string usuario);
        List<CentroCostoAsignadoCertificadoPartnerComplementoDTO> ObtenerCentroCostoAsociadoPorId(int id);




    }
}
