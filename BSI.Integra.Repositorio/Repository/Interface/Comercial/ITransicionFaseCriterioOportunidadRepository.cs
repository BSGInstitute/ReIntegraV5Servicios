using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Comercial
{
    public interface ITransicionFaseCriterioOportunidadRepository
    {
        #region Metodos Base
        TTransicionFaseCriterioOportunidad Add(TransicionFaseCriterioOportunidad entidad);
        TTransicionFaseCriterioOportunidad Update(TransicionFaseCriterioOportunidad entidad);
        bool Delete(int id, string usuario);
        #endregion

        List<TransicionFaseCriterioOportunidadDTO> Obtener();
        TransicionFaseCriterioOportunidad ObtenerPorId(int id);
    }
}
