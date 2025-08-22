using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IFichaAlumnoService
    {
        ActividadAgendaDTO CrearOportunidadFicha(OportunidadFichaDTO dto);
        AlumnoInformacionDTO ObtenerInformacionAlumnoPorIdOportunidadRN2(int idOportunidadRN2);
        (IEnumerable<ComboDTO> programas, IEnumerable<FaseOportunidadComboDTO> fasesOportunidad) ObtenerCombos();
    }
}
