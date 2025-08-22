using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IReporteEncuestasRepository
    {
        IEnumerable<ReporteEncuestasDTO> ObtenerDatosEncuestas(ReporteEncuestasFiltroDTO dto, int idExamenEncuesta);
        IEnumerable<ObtenerPreguntasExamenDTO> ObtenerPreguntasExamen(int idExamenEncuesta);
        int ObtenerIdExamenEncuesta(int idTipoEncuesta, int version);
        List<VersionEncuestaDTO> ObtenerVersionEncuesta(int idTipoEncuesta);

    }
}
