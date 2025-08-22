using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface ISentinelService
    {
        #region Metodos Base
        Sentinel Add(Sentinel entidad);
        Sentinel Update(Sentinel entidad);
        bool Delete(int id, string usuario);

        List<Sentinel> Add(List<Sentinel> listadoEntidad);
        List<Sentinel> Update(List<Sentinel> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelDTO> ObtenerSentinel();
        IEnumerable<SentinelComboDTO> ObtenerCombo();
        SentinelDatosAlumnoDetalleAgendaDTO ObtenerDatosSentinelDetallePorIdAlumno(int idAlumno);
        ValorIntDTO ObtenerIdSentinelPorDni(string dni);
        SentinelDTO ObtenerSentinelPorDni(string dni);
        SentinelDatosContactoDTO ObtenerDatosAlumnoSentinel(int idAlumno);
        SentinelDatosCabeceraDTO ObtenerCabeceraSentinel(int idSentinel);
        SentinelRespuestaDTO ActualizarSentinelAlumno(string dni, int idContacto, string usuario);
        SueldosDescripcionDTO ObtenerPromedioSueldo(int? idEmpresa, string dni, int? idCargo, int? idIndustria);
        SueldoPromedioDTO ObtenerSueldoPromedio(SueldoPromedioArgumentosDTO argumentos);
        ActualizarSentinelResultadoDTO ActualizarSentinelAlumno(string dni, string usuario);
        Sentinel MapeoEntidadDesdeDTO(SentinelDTO sentinelDTO);

    }
}
