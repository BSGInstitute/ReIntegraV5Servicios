using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IPlantillaPwService
    {
        IEnumerable<PlantillaPwDTO> Obtener();
        IEnumerable<ComboDTO> ObtenerCombo();
        string ObtenerFechaInicioPrograma(int idProgramaGeneral, int idCentroCosto);
        string ObtenerValorEtiquetaListas(int idOportunidad, int idAreaEtiqueta);
        IEnumerable<PlantillaPwComboWhatsappDTO> ObtenerComboWhatsapp();
        PlantillaPwComboModuloDTO ObtenerCombosModulo();
        List<SeccionPwPlantillaPwAgrupadoDTO> ObtenerSeccionesPorIdPlantillaPW(int idPlantillaPw);
        List<PlantillaPaisFiltroDTO> ObtenerPaisesPorIdPlantillaPw(int idPlantillaPw);
        IEnumerable<PlantillaPwDTO> Insertar(PlantillaPwParametrosDTO plantillaPwParametros, string usuario);
        IEnumerable<PlantillaPwDTO> Actualizar(PlantillaPwParametrosDTO plantillaPwParametros, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
