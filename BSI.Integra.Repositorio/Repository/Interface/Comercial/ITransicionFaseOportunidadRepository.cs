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
    public interface ITransicionFaseOportunidadRepository : IGenericRepository<TTransicionFaseOportunidad>
    {
        #region Metodos Base
        TTransicionFaseOportunidad Add(TransicionFaseOportunidad entidad);
        TTransicionFaseOportunidad Update(TransicionFaseOportunidad entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TTransicionFaseOportunidad> Add(IEnumerable<TransicionFaseOportunidad> listadoEntidad);
        IEnumerable<TTransicionFaseOportunidad> Update(IEnumerable<TransicionFaseOportunidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        List<TransicionCalificacionFaseDTO> ObtenerTransicionesCalificacionFase();
        TransicionFaseOportunidad ObtenerPorId(int id);
    }
}