using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface ICriterioCalificacionLlamadaRepository : IGenericRepository<TCriterioCalificacionLlamadum>
    {
        #region Metodos Base
        TCriterioCalificacionLlamadum Add(CriterioCalificacionLlamada entidad);
        TCriterioCalificacionLlamadum Update(CriterioCalificacionLlamada entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCriterioCalificacionLlamadum> Add(IEnumerable<CriterioCalificacionLlamada> listadoEntidad);
        IEnumerable<TCriterioCalificacionLlamadum> Update(IEnumerable<CriterioCalificacionLlamada> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        CriterioCalificacionLlamada ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<CriterioCalificacionLlamada> ObtenerCriterios();

    }
}
