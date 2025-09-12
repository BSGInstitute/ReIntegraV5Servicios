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
        //bool InsertarCriterio(CriterioCalificacionFaseDTO criterioCalificacionFaseDTO, string usuario);
        //bool ActualizarCriterio(CriterioCalificacionFaseDTO criterioCalificacionFaseDTO, string usuario);
        CriterioCalificacionFase ActualizarCriterio(CriterioCalificacionFase entidad);
        bool EliminarCriterio(int id, string usuario);
        List<CriterioCalificacionFaseDTO> ObtenerCriteriosCalificacionFase();
        List<CriterioCalificacionFaseDTO> ObtenerCriteriosPorTransicion(int idTransicionCalificacionFase);
        CriterioCalificacionFase ObtenerCriterioCalificacionFasePorId(int idCriterioCalificacionFase);
        //Task<Dictionary<string, List<ComboDTO>>> ObtenerCriticidad();
        IEnumerable<ComboDTO> ListaCriterios();
    }
}