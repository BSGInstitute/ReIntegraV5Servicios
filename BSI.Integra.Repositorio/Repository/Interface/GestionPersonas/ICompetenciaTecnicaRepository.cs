using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ICompetenciaTecnicaRepository
    {
        #region Metodos Base
        TCompetenciaTecnica Add(CompetenciaTecnica entidad);
        TCompetenciaTecnica Update(CompetenciaTecnica entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCompetenciaTecnica> Add(IEnumerable<CompetenciaTecnica> listadoEntidad);
        IEnumerable<TCompetenciaTecnica> Update(IEnumerable<CompetenciaTecnica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CursoComplementarioDTO> Obtener();
        CompetenciaTecnica? ObtenerPorIdCursoComplementario(int idCursoComplementario);
        CompetenciaTecnica? ObtenerPorId(int id);
    }
}
