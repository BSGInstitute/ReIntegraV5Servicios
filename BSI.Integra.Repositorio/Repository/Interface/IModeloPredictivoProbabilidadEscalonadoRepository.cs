using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// Interface: IModeloPredictivoProbabilidadEscalonadoRepository
    /// Autor: Jose Vega
    /// Fecha: 22/04/2026
    /// Version: 1.0
    /// <summary>
    /// Contrato de acceso a datos de mkt.T_ModeloPredictivoProbabilidadEscalonado.
    /// </summary>
    public interface IModeloPredictivoProbabilidadEscalonadoRepository : IGenericRepository<TModeloPredictivoProbabilidadEscalonado>
    {
        #region Metodos Base
        TModeloPredictivoProbabilidadEscalonado Add(ModeloPredictivoProbabilidadEscalonado entidad);
        TModeloPredictivoProbabilidadEscalonado Update(ModeloPredictivoProbabilidadEscalonado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivoProbabilidadEscalonado> Add(IEnumerable<ModeloPredictivoProbabilidadEscalonado> listadoEntidad);
        IEnumerable<TModeloPredictivoProbabilidadEscalonado> Update(IEnumerable<ModeloPredictivoProbabilidadEscalonado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        ScorePrimeraOportunidadDTO ObtenerP0PorIdOportunidad(int idOportunidad);
    }
}
