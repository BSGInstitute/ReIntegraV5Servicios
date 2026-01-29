using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
  
    public interface ICriticidadCalificacionService
    {
        #region Metodos Base
        CriticidadCalificacion Add(CriticidadCalificacion entidad);
        CriticidadCalificacion Update(CriticidadCalificacion entidad);
        bool Delete(int id, string usuario);
        List<CriticidadCalificacion> Add(List<CriticidadCalificacion> listadoEntidad);
        List<CriticidadCalificacion> Update(List<CriticidadCalificacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        CriticidadCalificacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
