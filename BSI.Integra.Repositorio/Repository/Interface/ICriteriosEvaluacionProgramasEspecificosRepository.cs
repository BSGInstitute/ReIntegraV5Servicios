using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICriteriosEvaluacionProgramasEspecificosRepository : IGenericRepository<TPespecificoEsquema>
    {
        #region Metodos Base
        TPespecificoEsquema Add(PEspecificoEsquema entidad);
        TPespecificoEsquema Update(PEspecificoEsquema entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPespecificoEsquema> Add(IEnumerable<PEspecificoEsquema> listadoEntidad);
        IEnumerable<TPespecificoEsquema> Update(IEnumerable<PEspecificoEsquema> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public ValorDTO ObtenerEsquemaPorIdPEspecifico(int IdPEspecifico);

        public IEnumerable<DatosListaPespecificoEsquemaDTO> ObtenerProgramasEspecificoEsquemasFiltrosPadreIndividual(FiltroProgramaEspecificoEsquemaFiltroCompuestoDTO filtro);

    }
}
