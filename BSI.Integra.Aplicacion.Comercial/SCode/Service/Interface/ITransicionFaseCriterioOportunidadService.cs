using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Comercial.SCode.Service.Interface
{
    public interface ITransicionFaseCriterioOportunidadService
    {
        #region Metodos Base
        TransicionFaseCriterioOportunidad Add(TransicionFaseCriterioOportunidad entidad);
        TransicionFaseCriterioOportunidad Update(TransicionFaseCriterioOportunidad entidad);
        bool Delete(int id, string usuario);
        #endregion
        List<TransicionFaseCriterioOportunidadDTO> Obtener();
        TransicionFaseCriterioOportunidad ObtenerPorId(int id);

    }
}
