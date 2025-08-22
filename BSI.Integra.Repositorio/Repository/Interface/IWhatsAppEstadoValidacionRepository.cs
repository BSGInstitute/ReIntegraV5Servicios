using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppEstadoValidacionRepository
    {
        #region Metodos Base
        TWhatsAppEstadoValidacion Add(WhatsAppEstadoValidacionDTO entidad);
        TWhatsAppEstadoValidacion Update(WhatsAppEstadoValidacionDTO entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TWhatsAppEstadoValidacion> Add(IEnumerable<WhatsAppEstadoValidacionDTO> listadoEntidad);
        IEnumerable<TWhatsAppEstadoValidacion> Update(IEnumerable<WhatsAppEstadoValidacionDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<WhatsAppEstadoValidacionDTO> ObtenerListaEstadosValidacionNumeroWhatsApp();

    }
}
