using BSI.Integra.Aplicacion.DTO;
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
    public interface ISuscripcionProgramaGeneralRepository : IGenericRepository<TSuscripcionProgramaGeneral>
    {
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
        IEnumerable<SuscripcionProgramaGeneralDTO> ObtenerSuscripcionProgramaGeneralPorIdPGeneral(int idPGeneral);
        IEnumerable<ComboDTO> ObtenerComboPorIdPgeneral(int idPGeneral);
        IEnumerable<SuscripcionProgramaGeneral> ObtenerPorIdPGeneral(int idPGeneral);
        SuscripcionProgramaGeneral? ObtenerPorId(int id);
    }
}
