using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface IFaseCalificacionService
    {
        #region Metodos Base
        FaseCalificacion Add(FaseCalificacion entidad);
        FaseCalificacion Update(FaseCalificacion entidad);
        bool Delete(int id, string usuario);
        List<FaseCalificacion> Add(List<FaseCalificacion> listadoEntidad);
        List<FaseCalificacion> Update(List<FaseCalificacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        FaseCalificacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<FaseCalificacion> ObtenerFasesPorArea(int idPersonalAreaTrabajo);
    }
}
