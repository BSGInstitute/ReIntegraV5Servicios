using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Sendingblue
{
    public interface ISendinBlueEventoWebHookRepository
    {
        public TSendinBlueEventoWebHook Add(SendinBlueEventoWebHook entidad);
        TSendinBlueEventoWebHook Update(SendinBlueEventoWebHook entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TSendinBlueEventoWebHook> Add(IEnumerable<SendinBlueEventoWebHook> listadoEntidad);
        IEnumerable<TSendinBlueEventoWebHook> Update(IEnumerable<SendinBlueEventoWebHook> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        List<SendinBlueEventoWebHookDTO> ObtenerTodaLaData();
    }
}
