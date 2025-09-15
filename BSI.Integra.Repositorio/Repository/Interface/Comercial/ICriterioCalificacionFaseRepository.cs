using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICriterioCalificacionFaseRepository : IGenericRepository<TCriterioCalificacionFaseOportunidad>
    {
        #region Metodos Base
        TCriterioCalificacionFaseOportunidad Add(CriterioCalificacionFase entidad);
        TCriterioCalificacionFaseOportunidad Update(CriterioCalificacionFase entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCriterioCalificacionFaseOportunidad> Add(IEnumerable<CriterioCalificacionFase> listadoEntidad);
        IEnumerable<TCriterioCalificacionFaseOportunidad> Update(IEnumerable<CriterioCalificacionFase> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<CriterioCalificacionFaseDTO> ObtenerCriteriosCalificacionFase();
        CriterioCalificacionFase ObtenerPorId(int id);
    }
}