using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    public interface IPGeneralModalidadRepository : IGenericRepository<TPgeneralModalidad>
    {
        #region Metodos Base
        TPgeneralModalidad Add(PgeneralModalidad entidad);
        TPgeneralModalidad Update(PgeneralModalidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPgeneralModalidad> Add(IEnumerable<PgeneralModalidad> listadoEntidad);
        IEnumerable<TPgeneralModalidad> Update(IEnumerable<PgeneralModalidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PGeneralModalidadDTO> ListarModalidadesCurso(int idPGeneral);
        IEnumerable<PGeneralModalidadDTO> ObtenerPGeneralModalidadPorIdPGeneral(int idPGeneral);
        PgeneralModalidad? ObtenerPorIdPGeneralYIdModalidadCurso(int idPGeneral, int idModalidadCurso);
    }
}
