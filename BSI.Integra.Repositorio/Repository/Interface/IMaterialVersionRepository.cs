using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMaterialVersionRepository : IGenericRepository<TMaterialVersion>
    {
        #region Metodos Base
        TMaterialVersion Add(MaterialVersion entidad);
        TMaterialVersion Update(MaterialVersion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMaterialVersion> Add(IEnumerable<MaterialVersion> listadoEntidad);
        IEnumerable<TMaterialVersion> Update(IEnumerable<MaterialVersion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialVersionDTO> ObtenerMaterialVersion();
        IEnumerable<GrabacionCelularCorporativoDTO> ObtenerGrabacionesCelularCorporativo();
        bool InsertarGrabacionesCelularCorporativo(GrabacionCelularCorporativoDTO datos,string usuario);
        IEnumerable<ComboDTO> ObtenerCombo();
        MaterialVersion ObtenerPorId(int id);
        List<MaterialVersion> ObtenerPorIds(List<int> id);
        string SubirModeloCertificadoRepositorio(byte[] archivo, string tipo, string nombreArchivo);
    }
}