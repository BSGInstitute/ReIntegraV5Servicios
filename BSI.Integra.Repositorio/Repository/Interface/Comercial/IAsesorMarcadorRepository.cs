using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface IAsesorMarcadorRepository : IGenericRepository<TAsesorMarcador>
    {
        #region Metodos Base
        TAsesorMarcador Add(AsesorMarcador entidad);
        TAsesorMarcador Update(AsesorMarcador entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TAsesorMarcador> Add(IEnumerable<AsesorMarcador> listadoEntidad);
        IEnumerable<TAsesorMarcador> Update(IEnumerable<AsesorMarcador> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<AsesorMarcadorDTO> Obtener();
        AsesorMarcador? ObtenerPorId(int id);
        List<ReporteAsesorMarcadorDTO> ObtenerReporteMarcador(FiltroReporteAsesorMarcadorDTO filtro);
        List<ReporteFinalPromedioDTO> ObtenerReporteMarcadorTiemposPromedio(FiltroReporteAsesorMarcadorDTO filtro);
    }
}
