using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IMaestroEvaluacionRepository : IGenericRepository<TPuestoTrabajoRemuneracion>
    {
        List<ExamenTestDTO> ObtenerExamenTest();
        List<EvaluacionAgrupadaComponenteDTO> ObtenerEvaluacionAgrupado(int IdEvaluacion);
    }
}
