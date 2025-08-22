using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IMaestroCursoMoodleService
    {
        List<MoodleCategoriaDetalle> ObtenerComboMoodleCategoria();
        List<MoodleCursoDTO> ObtenerCursosMoodleRegistradas();
        bool ExisteCursoMoodle(int idCursoMoodle);
        MoodleCursoDTO InsertarMoodleCurso(MoodleCursoDTO moodleCursoDTO, string usuario);
        MoodleCursoDTO ActualizarMoodleCurso(MoodleCursoDTO moodleCursoDTO, string usuario);
        bool EliminarMoodleCurso(int id, string usuario);
    }
}
