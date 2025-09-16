using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Servicios.Interface
{
    public interface ICriterioCalificacionFaseService
    {
        #region
        CriterioCalificacionFase Add(CriterioCalificacionFase entidad);
        CriterioCalificacionFase Update(CriterioCalificacionFase entidad);
        bool Delete(int id, string usuario);
        #endregion
        List<CriterioCalificacionFaseDTO> Obtener();
        CriterioCalificacionFase ObtenerPorId(int id);
    }
}