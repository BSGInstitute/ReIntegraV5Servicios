using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IMatriculaCabeceraDatosCertificadoService
    {
        #region Metodos Base
        MatriculaCabeceraDatosCertificado Add(MatriculaCabeceraDatosCertificado entidad);
        MatriculaCabeceraDatosCertificado Update(MatriculaCabeceraDatosCertificado entidad);
        bool Delete(int id, string usuario);

        List<MatriculaCabeceraDatosCertificado> Add(List<MatriculaCabeceraDatosCertificado> listadoEntidad);
        List<MatriculaCabeceraDatosCertificado> Update(List<MatriculaCabeceraDatosCertificado> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        DateTime TransformarCadenaEnFecha(string fecha);
        string TransformarFechaEnCadena(DateTime fecha);
        MatriculaCabeceraDatosCertificado ObtenerTotal(int idMatriculaCabecera);
    }
}
