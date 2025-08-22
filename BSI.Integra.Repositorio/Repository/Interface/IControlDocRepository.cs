using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IControlDocRepository : IGenericRepository<TControlDoc>
    {
        #region Metodos Base
        TControlDoc Add(ControlDoc entidad);
        TControlDoc Update(ControlDoc entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TControlDoc> Add(IEnumerable<ControlDoc> listadoEntidad);
        IEnumerable<TControlDoc> Update(IEnumerable<ControlDoc> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<DocumentoMatriculaDTO> ObtenerDocumentosPorMatriculaCabecera(string CodigoMatricula);
        List<ControlDocumentoMatriculaDTO> ObtenerDocumentosPorMatriculaCabeceraControl(int idMatriculaCabecera);
    }
}
