using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMatriculaCabeceraDatosCertificadoMensajeRepository : IGenericRepository<TMatriculaCabeceraDatosCertificadoMensaje>
    {
        #region Metodos Base
        TMatriculaCabeceraDatosCertificadoMensaje Add(MatriculaCabeceraDatosCertificadoMensaje entidad);
        TMatriculaCabeceraDatosCertificadoMensaje Update(MatriculaCabeceraDatosCertificadoMensaje entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMatriculaCabeceraDatosCertificadoMensaje> Add(IEnumerable<MatriculaCabeceraDatosCertificadoMensaje> listadoEntidad);
        IEnumerable<TMatriculaCabeceraDatosCertificadoMensaje> Update(IEnumerable<MatriculaCabeceraDatosCertificadoMensaje> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MatriculaCabeceraDatosCertificadoMensajeDTO> ObtenerMatriculaCabeceraDatosCertificadoMensaje();
        IEnumerable<MatriculaCabeceraDatosCertificadoMensajeComboDTO> ObtenerCombo();
        ValorIntDTO ObtenerCantidadMensajesPorUsername(string userName);
        int ObtenerCambiosPendientes(int idMatriculaCabecera);
        List<MatriculaCabeceraDatosCertificado> obtenerListado(int idMatriculaCabecera);
        List<MatriculaCabeceraDatosCertificadoDTO> ObtenerDatosCertificadoPorMatricula(int IdMatriculaCabecera);
        Personal obtenerIntegraAspNetUser(int Usuario);
        MatriculaCabeceraDTO obtenerMatricula(int idMatriculaCabecera);
        DetalleOportunidadOperacionesDTO ObtenerDetalleMatricula(int idMatriculaCabecera);
        MatriculaCabeceraDTO obtenerAlumno(int IdAlumno);
        DetalleOportunidadOperacionesDTO obtenerOportunidad(int IdOportunidad);
        public bool ExisteNuevaAulaVirtual(int idPEspecifico);
        string ObtenerFechaInicioCapacitacion(int IdMatriculaCabecera);
        string ObtenerFechaFinCapacitacion(int IdMatriculaCabecera);
        string ObtenerFechaInicioCapacitacionPortalWeb(int IdMatriculaCabecera);
        string ObtenerFechaFinCapacitacionPortalWeb(int IdMatriculaCabecera);
        DatosOportunidadDocumentosCompuestoDTO ObtenerDatosCompuestosPorIdOportunidad(int idOportunidad);
        List<MatriculaCabeceraDatosCertificadoMensajesDTO> ObtenerMensajesPendientes(int idPersonal);
        List<MatriculaCabeceraDatosCertificadoMensajesDTO> ObtenerMensajesLeidos(int idPersonal);
    }
}