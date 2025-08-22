using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppNumeroValidadoRepository : IGenericRepository<TWhatsAppNumeroValidado>
    {
        #region Metodos Base
        TWhatsAppNumeroValidado Add(WhatsAppNumeroValidado entidad);
        TWhatsAppNumeroValidado Update(WhatsAppNumeroValidado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TWhatsAppNumeroValidado> Add(IEnumerable<WhatsAppNumeroValidado> listadoEntidad);
        IEnumerable<TWhatsAppNumeroValidado> Update(IEnumerable<WhatsAppNumeroValidado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        bool VerificarNumeroValidado(string numero);
    }
}
