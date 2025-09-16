using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface ILineamientoCalificacionFaseRepository
    {
        #region
        TLineamientoCalificacionFase Add(LineamientoCalificacionFase entidad);
        TLineamientoCalificacionFase Update(LineamientoCalificacionFase entidad);
        bool Delete(int id, string usuario);
        #endregion
        LineamientoCalificacionFase ObtenerPorId(int id);
        List<LineamientoCalificacionFaseDTO> Obtener();
        
    }
}
