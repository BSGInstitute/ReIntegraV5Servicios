using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IRespuestaPreguntaProgramaCapacitacionRepository : IGenericRepository<TRespuestaPreguntaProgramaCapacitacion>
    {
        #region Metodos Base
        TRespuestaPreguntaProgramaCapacitacion Add(RespuestaPreguntaProgramaCapacitacion entidad);
        TRespuestaPreguntaProgramaCapacitacion Update(RespuestaPreguntaProgramaCapacitacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TRespuestaPreguntaProgramaCapacitacion> Add(IEnumerable<RespuestaPreguntaProgramaCapacitacion> listadoEntidad);
        IEnumerable<TRespuestaPreguntaProgramaCapacitacion> Update(IEnumerable<RespuestaPreguntaProgramaCapacitacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<RespuestaPreguntaProgramaCapacitacion> ObtenerPorIdPreguntaProgramaCapacitacion(int idPreguntaProgramaCapacitacion);
        RespuestaPreguntaProgramaCapacitacion ObtenerPorId(int id);
    }
}
