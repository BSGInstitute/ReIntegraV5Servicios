using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPGeneralRelacionadoRepository : IGenericRepository<TPgeneralRelacionado>
    {
        #region Metodos Base
        TPgeneralRelacionado Add(PgeneralRelacionado entidad);
        TPgeneralRelacionado Update(PgeneralRelacionado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralRelacionado> Add(IEnumerable<PgeneralRelacionado> listadoEntidad);
        IEnumerable<TPgeneralRelacionado> Update(IEnumerable<PgeneralRelacionado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PgeneralRelacionado? ObtenerPorId(int id);
        IEnumerable<PgeneralRelacionado> ObtenerPorIdPGeneral(int idPGeneral);
        IEnumerable<PGeneralProgramaRelacionadoDTO> ObtenerCursosRelacionadosPorPrograma(int idPGeneral);
        IEnumerable<ComboDTO> ObtenerCursosNoRelacionadosPorPrograma(int idPGeneral);
    }
}
