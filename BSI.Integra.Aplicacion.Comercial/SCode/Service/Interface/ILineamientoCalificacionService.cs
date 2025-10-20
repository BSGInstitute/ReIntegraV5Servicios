using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface ILineamientoCalificacionService
    {
        #region Metodos Base
        LineamientoCalificacion Add(LineamientoCalificacion entidad);
        LineamientoCalificacion Update(LineamientoCalificacion entidad);
        bool Delete(int id, string usuario);
        List<LineamientoCalificacion> Add(List<LineamientoCalificacion> listadoEntidad);
        List<LineamientoCalificacion> Update(List<LineamientoCalificacion> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        LineamientoCalificacion ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<string?> GenerarCuerpoCalificacionv2(int idLlamada);
    }
}
