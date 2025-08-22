using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPGeneralDescripcionRepository : IGenericRepository<TPgeneralDescripcion>
    {
        IEnumerable<PGeneralDescripcionDTO> ObtenerPGeneralDescripcionPorIdPGeneral(int idPGeneral);
    }
}
