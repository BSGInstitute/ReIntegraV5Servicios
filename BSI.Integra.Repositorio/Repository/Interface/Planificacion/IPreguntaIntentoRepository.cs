using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IPreguntaIntentoRepository : IGenericRepository<TPreguntaIntento>
    {
        #region Metodos Base
        TPreguntaIntento Add(PreguntaIntento entidad);
        TPreguntaIntento Update(PreguntaIntento entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPreguntaIntento> Add(IEnumerable<PreguntaIntento> listadoEntidad);
        IEnumerable<TPreguntaIntento> Update(IEnumerable<PreguntaIntento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PreguntaIntento ObtenerPorId(int id);
    }
}
