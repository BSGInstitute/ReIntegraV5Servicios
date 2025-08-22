using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IModalidadCursoService
    {
        IEnumerable<ComboDTO> ObtenerCombo();
        IEnumerable<ModalidadCursoDTO> Obtener();
    }
}
