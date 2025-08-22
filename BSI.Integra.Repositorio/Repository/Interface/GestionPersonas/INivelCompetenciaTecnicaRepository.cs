using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface INivelCompetenciaTecnicaRepository : IGenericRepository<TNivelCompetenciaTecnica>
    {
        #region Metodos Base
        TNivelCompetenciaTecnica Add(NivelCompetenciaTecnica entidad);
        TNivelCompetenciaTecnica Update(NivelCompetenciaTecnica entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TNivelCompetenciaTecnica> Add(IEnumerable<NivelCompetenciaTecnica> listadoEntidad);
        IEnumerable<TNivelCompetenciaTecnica> Update(IEnumerable<NivelCompetenciaTecnica> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion


        IEnumerable<NivelCompetenciaTecnicaDTO> Obtener();
        NivelCompetenciaTecnica? ObtenerPorId(int idNivelCompetenciaTecnica);
    }
}
