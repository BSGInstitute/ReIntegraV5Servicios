using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IProgramaAreaRelacionadaRepository : IGenericRepository<TProgramaAreaRelacionadum>
    {
        IEnumerable<ProgramaAreaRelacionadaDTO> ObtenerProgramaAreaRelacionadaPorIdPGeneral(int idPGeneral);
        IEnumerable<ProgramaAreaRelacionadum> ObtenerPorIdPGeneral(int idPGeneral);
        ProgramaAreaRelacionadum? ObtenerPorId(int id);
        ProgramaAreaRelacionadum? ObtenerPorIdPgeneralIdAreaCapacitacion(int idPgeneral, int idAreaCapacitacion);
    }
}
