using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IRemarketingEmbudoHistoricoService
    {
        #region Metodos Base
        RemarketingEmbudoHistorico Add(RemarketingEmbudoHistorico entidad);
        RemarketingEmbudoHistorico Update(RemarketingEmbudoHistorico entidad);
        bool Delete(int id, string usuario);

        List<RemarketingEmbudoHistorico> Add(List<RemarketingEmbudoHistorico> listadoEntidad);
        List<RemarketingEmbudoHistorico> Update(List<RemarketingEmbudoHistorico> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public Task<bool> EvaluarEmbudoRemarketing(DateTime? FechaCorte);
        public List<RemarketingEmbudoEsquemaNivelDTO> ObtenerNivelEsquemaEmbudoRemarketing();
    }
}
