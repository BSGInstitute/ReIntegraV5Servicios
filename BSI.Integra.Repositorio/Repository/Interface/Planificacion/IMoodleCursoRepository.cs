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
    public interface IMoodleCursoRepository : IGenericRepository<TMoodleCurso>
    {
        #region Metodos Base
        TMoodleCurso Add(MoodleCurso entidad);
        TMoodleCurso Update(MoodleCurso entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMoodleCurso> Add(IEnumerable<MoodleCurso> listadoEntidad);
        IEnumerable<TMoodleCurso> Update(IEnumerable<MoodleCurso> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        bool Exist(int id);
        #endregion
        List<MoodleCursoDTO> ObtenerCursosMoodleRegistradas();
        MoodleCurso ObtenerPorId(int id);
        bool ExisteCursoMoodle(int idCursoMoodle);
        MoodleCursoDTO ObtenerCursoPorId(int id);

    }
}
