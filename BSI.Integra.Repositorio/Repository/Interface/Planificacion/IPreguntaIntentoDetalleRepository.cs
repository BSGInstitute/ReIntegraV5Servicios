using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPreguntaIntentoDetalleRepository : IGenericRepository<TPreguntaIntentoDetalle>
    {
        #region Metodos Base
        TPreguntaIntentoDetalle Add(PreguntaIntentoDetalle entidad);
        TPreguntaIntentoDetalle Update(PreguntaIntentoDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPreguntaIntentoDetalle> Add(IEnumerable<PreguntaIntentoDetalle> listadoEntidad);
        IEnumerable<TPreguntaIntentoDetalle> Update(IEnumerable<PreguntaIntentoDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Task<IEnumerable<PreguntaIntentoDetalleOrdenDTO>> ObtenerListadoPreguntaIntentoDetallado();
        IEnumerable<PreguntaIntentoDetalle> ObtenerPorIdPreguntaIntento(int idPreguntaIntento);
        PreguntaIntentoDetalle ObtenerPorId(int id);
    }
}
