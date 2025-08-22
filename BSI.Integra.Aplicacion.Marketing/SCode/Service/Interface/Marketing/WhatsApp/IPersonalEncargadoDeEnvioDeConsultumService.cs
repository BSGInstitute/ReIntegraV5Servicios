using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp
{
    public interface IPersonalEncargadoDeEnvioDeConsultumService
    {
        #region Metodos Base
        TPersonalEncargadoDeEnvioDeConsultum Add(PersonalEncargadoDeEnvioDeConsultumDTO entidad, string usuario);
        TPersonalEncargadoDeEnvioDeConsultum Update(PersonalEncargadoDeEnvioDeConsultumDTO entidad, string usuario);
        bool Delete(int id, string usuario);

        List<TPersonalEncargadoDeEnvioDeConsultum> Add(List<PersonalEncargadoDeEnvioDeConsultumDTO> listadoEntidad, string usuario);
        List<TPersonalEncargadoDeEnvioDeConsultum> Update(List<PersonalEncargadoDeEnvioDeConsultum> listadoEntidad, string usuario);
        List<TPersonalEncargadoDeEnvioDeConsultum> AddDelete(List<PersonalEncargadoDeEnvioDeConsultumDTO> listadoEntidad1, int IdConfiguracionDeEnvioParaWhatsApp, string usuario);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
