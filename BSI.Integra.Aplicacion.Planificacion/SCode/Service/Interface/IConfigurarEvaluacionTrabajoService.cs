using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IConfigurarEvaluacionTrabajoService
    {
        bool Insertar(ConfigurarEvaluacionTrabajoDTO configurarEvaluacionTrabajoDTO, string usuario);
        bool Actualizar(ConfigurarEvaluacionTrabajoDTO configurarEvaluacionTrabajoDTO, string usuario);
        bool Eliminar(int id, string usuario);

        public List<PreguntaEvaluacionTrabajoDTO> ObtenerPorConfiguracion(int idConfigurarEvaluacionTrabajo);
    }
}
