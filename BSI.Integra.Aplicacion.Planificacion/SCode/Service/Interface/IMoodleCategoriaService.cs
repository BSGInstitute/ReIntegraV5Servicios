using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IMoodleCategoriaService
    {
        MoodleCategoriaDTO Insertar(MoodleCategoriaDTO moodleCategoriaDTO, string usuario);
        MoodleCategoriaDTO Actualizar(MoodleCategoriaDTO moodleCategoriaDTO, string usuario);
        bool EliminarMoodleCategoria(int idMoodleCategoria, string usuario);
    }
}
