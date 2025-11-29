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
    public interface IPGeneralVersionProgramaRepository : IGenericRepository<TPgeneralVersionPrograma>
    {
        #region Metodos Base
        TPgeneralVersionPrograma Add(PgeneralVersionPrograma entidad);
        TPgeneralVersionPrograma Update(PgeneralVersionPrograma entidad);
        #endregion
        Task<IEnumerable<ComboDTO>> ObtenerVersionesProgramaPorPGeneralAsync(int idPGeneral);
        IEnumerable<PGeneralVersionProgramaDetalleDTO> ObtenerPGeneralVersionProgramaDetallePorIdPGeneral(int idPGeneral);
        IEnumerable<PgeneralVersionPrograma> ObtenerPorIdPGeneral(int idPGeneral);
        PgeneralVersionPrograma? ObtenerPorId(int id);
        PgeneralVersionPrograma? ObtenerPorIdPgeneralIdVersionPrograma(int idPgeneral, int idVersionPrograma);
    }
}
