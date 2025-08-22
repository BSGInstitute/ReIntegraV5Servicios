using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface INivelEstudioRepository : IGenericRepository<TNivelEstudio>
    {
        #region Metodos Base
        TNivelEstudio Add(NivelEstudio entidad);
        TNivelEstudio Update(NivelEstudio entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TNivelEstudio> Add(IEnumerable<NivelEstudio> listadoEntidad);
        IEnumerable<TNivelEstudio> Update(IEnumerable<NivelEstudio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<NivelEstudioComboDTO> ObtenerCombo();

        IEnumerable<NivelEstudioDTO> Obtener();
        NivelEstudio? ObtenerPorId(int id);
    }
}
