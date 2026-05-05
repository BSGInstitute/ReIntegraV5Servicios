using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.ExperianSentinel;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ISentinelRepository : IGenericRepository<TSentinel>
    {
        #region Metodos Base
        TSentinel Add(Sentinel entidad);
        TSentinel Update(Sentinel entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSentinel> Add(IEnumerable<Sentinel> listadoEntidad);
        IEnumerable<TSentinel> Update(IEnumerable<Sentinel> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<SentinelDTO> ObtenerSentinel();
        IEnumerable<SentinelComboDTO> ObtenerCombo();
        SentinelDatosAlumnoAgendaDTO ObtenerSentinelTipoDocumentoDNIPorIdAlumno(int idAlumno);
        SentinelDatosAlumnoAgendaDTO ObtenerSentinelPorIdAlumno(int idAlumno);
        ValorIntDTO ObtenerIdSentinelPorDni(string dni);
        SentinelDTO ObtenerSentinelPorDni(string dni);
        SentinelDatosContactoDTO ObtenerDatosAlumnoSentinel(int idAlumno);
        SentinelDatosCabeceraDTO ObtenerCabeceraSentinel(int idSentinel);
        SentinelCredencialDTO ObtenerCredencial();

        /// <summary>
        /// Obtiene las credenciales para el servicio REST de Experian Sentinel.
        /// Lee desde la vista fin.V_ObtenerSentinelCredencialRest (creada por DBA).
        /// </summary>
        SentinelCredencialRestDTO ObtenerCredencialRest();
    }
}