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
    public interface ITipoCompetenciaTecnicaRepository : IGenericRepository<TTipoCompetenciaTecnica>
    {
        #region Metodos Base
        TTipoCompetenciaTecnica Add(TipoCompetenciaTecnica entidad);
        TTipoCompetenciaTecnica Update(TipoCompetenciaTecnica entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TTipoCompetenciaTecnica> Add(IEnumerable<TipoCompetenciaTecnica> listadoEntidad);
        IEnumerable<TTipoCompetenciaTecnica> Update(IEnumerable<TipoCompetenciaTecnica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        IEnumerable<TipoCursoComplementarioDTO> ObtenerCombos();
    }
}
