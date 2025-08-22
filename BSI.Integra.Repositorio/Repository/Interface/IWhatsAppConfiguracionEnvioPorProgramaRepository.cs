using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppConfiguracionEnvioPorProgramaRepository
    {
        #region Metodos Base
        TWhatsAppConfiguracionEnvioPorPrograma Add(WhatsAppConfiguracionEnvioPorProgramaDTO entidad);
        TWhatsAppConfiguracionEnvioPorPrograma Update(WhatsAppConfiguracionEnvioPorProgramaDTO entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TWhatsAppConfiguracionEnvioPorPrograma> Add(IEnumerable<WhatsAppConfiguracionEnvioPorProgramaDTO> listadoEntidad);
        IEnumerable<TWhatsAppConfiguracionEnvioPorPrograma> Update(IEnumerable<WhatsAppConfiguracionEnvioPorProgramaDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<WhatsAppConfiguracionEnvioPorProgramaDTO> GetBy(int id);
    }
}
