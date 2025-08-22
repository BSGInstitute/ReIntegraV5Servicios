using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppNumeroValidadoService
    {
        #region Metodos Base
        WhatsAppNumeroValidado Add(WhatsAppNumeroValidado entidad);
        WhatsAppNumeroValidado Update(WhatsAppNumeroValidado entidad);
        bool Delete(int id, string usuario);

        List<WhatsAppNumeroValidado> Add(List<WhatsAppNumeroValidado> listadoEntidad);
        List<WhatsAppNumeroValidado> Update(List<WhatsAppNumeroValidado> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        bool VerificarNumeroValidado(string numero);
    }
}
