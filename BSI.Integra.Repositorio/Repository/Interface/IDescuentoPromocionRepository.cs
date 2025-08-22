using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDescuentoPromocionRepository
    {
        #region Metodos Base
        TDescuentoPromocion Add(DescuentoPromocion entidad);
        TDescuentoPromocion Update(DescuentoPromocion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TDescuentoPromocion> Add(IEnumerable<DescuentoPromocion> listadoEntidad);
        IEnumerable<TDescuentoPromocion> Update(IEnumerable<DescuentoPromocion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<DescuentoPromocionDTO> Obtener();
        IEnumerable<DescuentoPromocion> ObtenerPorIdTipoDescuento(int idTipoDescuento);
    }
}
