using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface ICriteriosEvaluacionProgramasEspecificosService
    {
        #region Metodos Base
        PEspecificoEsquema Add(PEspecificoEsquema entidad);
        PEspecificoEsquema Update(PEspecificoEsquema entidad);
        bool Delete(int id, string usuario);

        List<PEspecificoEsquema> Add(List<PEspecificoEsquema> listadoEntidad);
        List<PEspecificoEsquema> Update(List<PEspecificoEsquema> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        public ValorDTO ObtenerEsquemaPorIdPEspecifico(int IdPEspecifico);
        IEnumerable<DatosListaPespecificoEsquemaDTO> ObtenerProgramasEspecificoEsquemasFiltrosPadreIndividual(FiltroProgramaEspecificoEsquemaFiltroCompuestoDTO paginador);
    }
}
