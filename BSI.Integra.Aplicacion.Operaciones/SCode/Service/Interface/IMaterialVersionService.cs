using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IMaterialVersionService
    {
        #region Metodos Base
        MaterialVersionDTO Insertar(MaterialVersionDTO dto, string usuario);
        MaterialVersionDTO Actualizar(MaterialVersionDTO dto, string usuario);
        bool Eliminar(int id, string usuario);

        List<MaterialVersion> InsertarLista(List<MaterialVersion> dtos, string usuario);
        List<MaterialVersion> ActualizarLista(List<MaterialVersion> dtos, string usuario);
        bool EliminarLista(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<MaterialVersionDTO> Obtener();
        IEnumerable<GrabacionCelularCorporativoDTO> ObtenerGrabacionesCelularCorporativo();
        bool InsertarGrabacionesCelularCorporativo(GrabacionCelularCorporativoDTO item,string usuario);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
