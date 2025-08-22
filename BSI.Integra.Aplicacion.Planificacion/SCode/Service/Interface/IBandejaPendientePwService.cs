
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IBandejaPendientePwService
    {
        void EliminacionBandejaPendienteLogicoPorIdDocumento(int idDocumento, string usuario, List<RevisionNivelPwFiltroIdPlantillaDTO> nuevos);
    }
}
