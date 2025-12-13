using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMontoPagoLogRepository : IGenericRepository<TMontoPagoLog>
    {
        #region Metodos Base
        TMontoPagoLog Add(MontoPagoLog entidad);
        TMontoPagoLog Update(MontoPagoLog entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMontoPagoLog> Add(IEnumerable<MontoPagoLog> listadoEntidad);
        IEnumerable<TMontoPagoLog> Update(IEnumerable<MontoPagoLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        MontoPagoLog? ObtenerPorIdMontoPago(int IdMontoPago);
        MontoPagoLog? ObtenerPorId(int id);
        IEnumerable<MontoPagoLogDTO> ObtenerReporteMontoPagoHistorico(FiltroMontoPagoHistoricoDTO filtro);
    }
}
