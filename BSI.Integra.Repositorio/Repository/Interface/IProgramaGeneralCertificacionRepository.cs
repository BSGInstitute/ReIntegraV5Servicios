using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IProgramaGeneralCertificacionRepository : IGenericRepository<TProgramaGeneralCertificacion>
    {
        #region Metodos Base
        TProgramaGeneralCertificacion Add(ProgramaGeneralCertificacion entidad);
        TProgramaGeneralCertificacion Update(ProgramaGeneralCertificacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TProgramaGeneralCertificacion> Add(IEnumerable<ProgramaGeneralCertificacion> listadoEntidad);
        IEnumerable<TProgramaGeneralCertificacion> Update(IEnumerable<ProgramaGeneralCertificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ProgramaGeneralCertificacionAgendaDTO> ObtenerCertificacionesParaAgendaPorIdOportunidad(int idOportunidad);
        ProgramaGeneralCertificacion? ObtenerPorId(int id);
        List<CompuestoCertificacionModalidadDTO> ObteneCertificacionesPorModalidades(int idPGeneral);
    }
}