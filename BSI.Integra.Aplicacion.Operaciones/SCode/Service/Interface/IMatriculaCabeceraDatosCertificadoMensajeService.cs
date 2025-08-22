using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IMatriculaCabeceraDatosCertificadoMensajeService
    {
        #region Metodos Base
        MatriculaCabeceraDatosCertificadoMensaje Add(MatriculaCabeceraDatosCertificadoMensaje entidad);
        MatriculaCabeceraDatosCertificadoMensaje Update(MatriculaCabeceraDatosCertificadoMensaje entidad);
        bool Delete(int id, string usuario);

        List<MatriculaCabeceraDatosCertificadoMensaje> Add(List<MatriculaCabeceraDatosCertificadoMensaje> listadoEntidad);
        List<MatriculaCabeceraDatosCertificadoMensaje> Update(List<MatriculaCabeceraDatosCertificadoMensaje> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<MatriculaCabeceraDatosCertificadoMensajeDTO> ObtenerMatriculaCabeceraDatosCertificadoMensaje();
        IEnumerable<MatriculaCabeceraDatosCertificadoMensajeComboDTO> ObtenerCombo();
        ValorIntDTO ObtenerCantidadMensajesPorUsername(string userName);
        List<MatriculaCabeceraDatosCertificadoMensajesDTO> ObtenerMensajesPendientes(int idPersonal);
        List<MatriculaCabeceraDatosCertificadoMensajesDTO> ObtenerMensajesLeidos(int idPersonal);
        Boolean ModificarCertificadoMensaje(MatriculaCabeceraDatosCertificadoMensajesDTO ObjetoDTO);
    }
}
