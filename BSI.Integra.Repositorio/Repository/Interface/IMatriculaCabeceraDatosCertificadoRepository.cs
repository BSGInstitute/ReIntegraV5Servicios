using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMatriculaCabeceraDatosCertificadoRepository : IGenericRepository<TMatriculaCabeceraDatosCertificado>
    {
        #region Metodos Base
        TMatriculaCabeceraDatosCertificado Add(MatriculaCabeceraDatosCertificado entidad);
        TMatriculaCabeceraDatosCertificado Update(MatriculaCabeceraDatosCertificado entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TMatriculaCabeceraDatosCertificado> Add(IEnumerable<MatriculaCabeceraDatosCertificado> listadoEntidad);
        IEnumerable<TMatriculaCabeceraDatosCertificado> Update(IEnumerable<MatriculaCabeceraDatosCertificado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        MatriculaCabeceraDatosCertificado ObtenerTotal(int idMatriculaCabecera);
        List<MatriculaCabeceraDatosCertificadoDTO> ObtenerDatosCertificadoPorMatricula(int IdMatriculaCabecera);
        MatriculaCabeceraDatosCertificado ObtenerMatriculaCabceraDatosCertificado (int IdMatriculaCabecera);
        DateTime TranformarCadenaEnFecha(string fecha);
        string TranformarFechaEnCadena(DateTime fecha);
    }
}
