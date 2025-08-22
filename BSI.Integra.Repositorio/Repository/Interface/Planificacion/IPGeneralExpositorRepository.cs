using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPGeneralExpositorRepository : IGenericRepository<TPgeneralExpositor>
    {
        IEnumerable<PGeneralExpositorDTO> ObtenerPGeneralExpositorPorIdPGeneral(int idPGeneral);
        IEnumerable<PGeneralExpositor> ObtenerPorIdPGeneral(int idPGeneral);
        PGeneralExpositor? ObtenerPorId(int id);
        PGeneralExpositor? ObtenerPorIdPgeneralIdExpositor(int idPgeneral, int idExpositor);
        PGeneralExpositor? ObtenerPorIdPgeneralIdExpositorModalidad(int idPgeneral, int idExpositor, int idModalidadCurso);
    }
}
