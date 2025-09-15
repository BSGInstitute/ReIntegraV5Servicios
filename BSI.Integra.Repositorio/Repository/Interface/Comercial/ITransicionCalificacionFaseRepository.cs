using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ITransicionCalificacionFaseRepository : IGenericRepository<TTransicionFaseOportunidad>
    {
        #region Metodos Base
        TTransicionFaseOportunidad Add(TransicionCalificacionFase entidad);
        TTransicionFaseOportunidad Update(TransicionCalificacionFase entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTransicionFaseOportunidad> Add(IEnumerable<TransicionCalificacionFase> listadoEntidad);
        IEnumerable<TTransicionFaseOportunidad> Update(IEnumerable<TransicionCalificacionFase> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<TransicionCalificacionFaseDTO> ObtenerTransicionesCalificacionFase();
        TransicionCalificacionFase ObtenerTransicionCalificacionFasePorId(int idTransicionCalificacionFase);
        TransicionCalificacionFase ObtenerPorId(int id);
    }
}