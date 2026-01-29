using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface ILineamientoCalificacionFaseService
    {
        #region
        LineamientoCalificacionFase Add(LineamientoCalificacionFase entidad);  
        LineamientoCalificacionFase Update(LineamientoCalificacionFase entidad);
        bool Delete(int id, string usuario);
        #endregion
        LineamientoCalificacionFase ObtenerPorId(int id);
        List<LineamientoCalificacionFaseDTO> Obtener();


    }
}
