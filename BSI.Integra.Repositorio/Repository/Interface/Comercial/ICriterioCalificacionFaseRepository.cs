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
        #endregion
        CriterioCalificacionFase ObtenerPorId(int id);
        List<CriterioCalificacionFaseDTO> Obtener();

    }
}