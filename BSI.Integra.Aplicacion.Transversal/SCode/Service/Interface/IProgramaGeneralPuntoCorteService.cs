using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IProgramaGeneralPuntoCorteService
    {
        PGeneralPuntoCorteComboDTO ObtenerComboModulo();
        List<ProgramaGeneralPuntoCorteConfiguracionDTO> ObtenerConfiguracionPuntoCorte();
        List<ProgramaGeneralPuntoCorteDTO> ObtenerPuntoCortePorPrograma(int idProgramaGeneral);
        List<ProgramaGeneralPuntoCorteDetalleDTO> ObtenerDetallePuntoCortePorIdPuntoCorte(PuntoCorteDetalleFiltroDTO filtro);
        ProgramaGeneralPuntoCorteDTO? ObtenerPuntoCortePorProgramaPais(int idProgramaGeneral, int idPais);
        bool ActualizarProgramaGeneralPuntoCorte(ProgramaGeneralPuntoCorteDTO dto, string usuario);
        bool ActualizarProgramaGeneralPuntoCortePaises(List<ProgramaGeneralPuntoCorteDTO> listaDto, string usuario);
        List<ProgramaGeneralPuntoCorteAreaSubAreaDTO> ObtenerListaProgramaGeneralPuntoCorte(ProgramaGeneralPuntoCorteFiltroDTO filtroProgramaGeneralPuntoCorte);
        bool ActualizarProgramaGeneralPuntoCorteMasivo(ProgramaGeneralPuntoCorteMasivoDTO dto, string usuario);
        bool ActualizarProgramaGeneralPuntoCorteConfiguracion(List<ProgramaGeneralPuntoCorteConfiguracionDTO> dtoConfiguracion, string usuario);
        bool Eliminar(List<int> idPaises, int idProgramaGeneral, string usuario);
    }
}
