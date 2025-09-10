using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
     public interface IPuntosGeneralesRepository
    {
        #region Metodos Base
        TCalificacionPuntoGeneral Add(PuntosGeneralesCalificacion entidad);
        TCalificacionPuntoGeneral Update(PuntosGeneralesCalificacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCalificacionPuntoGeneral> Add(IEnumerable<PuntosGeneralesCalificacion> listadoEntidad);
        IEnumerable<TCalificacionPuntoGeneral> Update(IEnumerable<PuntosGeneralesCalificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PuntosGeneralesCalificacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<PuntosGeneralesCalificacion> ObtenerPuntosGenerales();
    }
}
