using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
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
        // TTransicionFaseOportunidad Add(TransicionFaseOportunidad entidad);
        TTransicionFaseOportunidad Add(TransicionFaseOportunidad entidad);
        TTransicionFaseOportunidad Update(TransicionFaseOportunidad entidad);
        bool Delete(int id, string usuario);
        bool DeleteCriterios(int id, string usuario);
        #endregion
        List<TransicionFaseCriterioOportunidad> ObtenerPorIdTransicion(int idTransicionFaseOportunidad);
        TransicionFaseOportunidad ObtenerPorId(int id);
        List<TransicionFaseOportunidadPlanoDto> Obtener();

    }
}