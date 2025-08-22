using BSI.Integra.Aplicacion.DTO;
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
    public interface IMoodleCategoriaRepository : IGenericRepository<TMoodleCategorium>
    {
        #region Metodos Base
        TMoodleCategorium Add(MoodleCategoria entidad);
        TMoodleCategorium Update(MoodleCategoria entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMoodleCategorium> Add(IEnumerable<MoodleCategoria> listadoEntidad);
        IEnumerable<TMoodleCategorium> Update(IEnumerable<MoodleCategoria> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        bool Exist(int id);
        #endregion
        List<MoodleCategoriaDetalle> Obtener();
        MoodleCategoria? ObtenerPorId(int id);
        bool ExisteCategoriaMoodle(int idCategoriaMoodle);
    }
}
