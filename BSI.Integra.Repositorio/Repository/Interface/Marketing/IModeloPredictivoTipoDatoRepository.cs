using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing
{
    public interface IModeloPredictivoTipoDatoRepository : IGenericRepository<TModeloPredictivoTipoDato>
    {
        #region Metodos Base
        TModeloPredictivoTipoDato Add(ModeloPredictivoTipoDato entidad);
        TModeloPredictivoTipoDato Update(ModeloPredictivoTipoDato entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TModeloPredictivoTipoDato> Add(IEnumerable<ModeloPredictivoTipoDato> listadoEntidad);
        IEnumerable<TModeloPredictivoTipoDato> Update(IEnumerable<ModeloPredictivoTipoDato> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ModeloPredictivoTipoDato? ObtenerPorId(int id);
        IEnumerable<ModeloPredictivoTipoDato> ObtenerPorIdPGeneral(int idPGeneral);
        List<ModeloPredictivoTipoDatoDTO> ObtenerTipoDatoPorPrograma(int idPGeneral);
    }
}
